using DevExtreme.AspNet.Data;
using Hayalpc.Fatura.Common.Enums;
using Hayalpc.Fatura.Data;
using Hayalpc.Fatura.Data.Models;
using Hayalpc.Fatura.Panel.Internal.Services.Interfaces;
using Hayalpc.Library.Common.Enums;
using Hayalpc.Library.Common.Extensions;
using Hayalpc.Library.Common.Helpers;
using Hayalpc.Library.Common.Results;
using Hayalpc.Library.Log;
using Hayalpc.Library.Repository;
using Hayalpc.Library.Repository.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Hayalpc.Fatura.Panel.Internal.Services
{
    public abstract class BaseService<TEntity> : IBaseService<TEntity> where TEntity : class, IHpModel
    {
        protected readonly IRepository<TEntity> repository;
        protected readonly IHpLogger logger;
        protected readonly IHpUnitOfWork<HpDbContext> unitOfWork;
        protected readonly IMemoryCache memoryCache;

        public BaseService(IRepository<TEntity> repository, IHpLogger logger, IHpUnitOfWork<HpDbContext> unitOfWork, IMemoryCache memoryCache)
        {
            this.repository = repository;
            this.logger = logger;
            this.unitOfWork = unitOfWork;
            this.memoryCache = memoryCache;
        }

        public virtual IDataResult<TEntity> Get(long Id)
        {
            var model = repository.GetById(Id);
            if (model != null)
            {
                model = BeforeGet(model);
                return new SuccessDataResult<TEntity>(model);
            }
            return new ErrorDataResult<TEntity>("RecordNotFound");
        }

        public virtual TEntity BeforeGet(TEntity model)
        {
            return model;
        }

        public virtual TEntity BeforeAdd(TEntity model)
        {
            model.CreateTime = DateTime.Now;
            model.CreateUserId = RequestHelper.UserId;

            return model;
        }

        private TableDefinition GetTableDefinitionFromCache(string modelName, ActionType actionType)
        {
            var cacheKey = $"GetTableDefinitionFromCache-{modelName}-{actionType}";
            return memoryCache.GetOrCreate(cacheKey, (entry) =>
                {
                    entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(3);
                    return repository.GetQuery<TableDefinition>(x =>
                    x.ModelName == modelName &&
                    x.Status == Status.Active &&
                    x.ActionType == actionType).FirstOrDefault();
                });
        }

        private bool HasTableHistory(string modelName, ActionType actionType, long dataId = 0)
        {
            return repository.GetQuery<TableHistory>(x =>
                    x.ModelName == modelName &&
                    (x.Status == TableHistoryStatus.New || x.Status == TableHistoryStatus.Step1 || x.Status == TableHistoryStatus.Step2) &&
                    x.ActionType == actionType &&
                    x.DataId == dataId &&
                    x.RoleId1 > 0).Any();
        }

        public virtual IDataResult<TOEntity> AddCustom<TOEntity>(TOEntity model) where TOEntity : class, IHpModel
        {
            try
            {
                model.CreateTime = DateTime.Now;

                model.UpdateUserId = RequestHelper.UserId;
                model.UpdateTime = DateTime.Now;

                repository.Insert(model);
                unitOfWork.SaveChanges();
                return new SuccessDataResult<TOEntity>(model);
            }
            catch (Exception exp)
            {
                logger.Error(exp.ToString());
                return new ErrorDataResult<TOEntity>(500, exp.Message);
            }
        }

        public virtual IResult AddRange<TOEntity>(IEnumerable<TOEntity> models) where TOEntity : class, IHpModel
        {
            try
            {
                unitOfWork.BeginTransaction();

                repository.InsertRange(models);
                unitOfWork.SaveChanges();

                unitOfWork.CommitTransaction();
                return new SuccessResult();
            }
            catch (Exception exp)
            {
                unitOfWork.RollBackTransaction();
                logger.Error(exp.ToString());
                return new ErrorResult(500, exp.Message);
            }
        }

        private List<UserRole> GetByRoleId(long roleId) => repository.GetQuery<UserRole>(u => u.RoleId == roleId).Select(t => new UserRole
        {
            Id = t.Id,
            CreateTime = t.CreateTime,
            CreateUserId = t.CreateUserId,
            UpdateTime = t.UpdateTime,
            UpdateUserId = t.UpdateUserId,
            UserId = t.UserId,
            RoleId = t.RoleId,
            Role = t.Role
        }).ToList();

        public virtual IDataResult<TEntity> Add(TEntity model)
        {
            try
            {
                IDataResult<TEntity> response = null;

                model = BeforeAdd(model);
                var modelName = model.GetType().Name;

                var tableDefinition = GetTableDefinitionFromCache(modelName, ActionType.Insert);
                if (tableDefinition != null)
                {
                    if (!HasTableHistory(modelName, ActionType.Insert))
                    {
                        var tableHistory = new TableHistory
                        {
                            DealerId = 0,
                            TableDefinitionId = tableDefinition.Id,
                            Status = tableDefinition.RoleId1 > 0 ? TableHistoryStatus.New : TableHistoryStatus.Log,
                            ActionType = ActionType.Insert,
                            ActionDetail = "Insert",
                            ModelName = tableDefinition.ModelName,
                            DataId = 0,
                            NewData = JsonConvert.SerializeObject(model),
                            RoleId1 = tableDefinition.RoleId1,
                            RoleId2 = tableDefinition.RoleId2,
                            CreateTime = DateTime.Now,
                            UpdateTime = DateTime.Now,
                            CreateUserId = model.CreateUserId,
                            UpdateUserId = model.UpdateUserId,
                        };
                        repository.Insert(tableHistory);
                        unitOfWork.SaveChanges();

                        if (tableDefinition.RoleId2 > 0)
                        {
                            try
                            {
                                var str = "Onayınızı bekleyen talep bulunmaktadır.";
                                var userBulletin = GetByRoleId(tableDefinition.RoleId2 ?? 0).Select(x => new UserBulletin
                                {
                                    DealerId = 0,
                                    RoleGroupId = tableDefinition.RoleId2 ?? 0,
                                    UserId = 0,
                                    ActionType = "approveRequest",
                                    Title = "Talep Onay",
                                    Message = str,
                                    Type = UserBulletinType.Warning,
                                    Status = UserBulletinStatus.Unread,
                                    StartDate = DateTime.Now,
                                    ExpireDate = DateTime.Now.AddDays(3),
                                    CreateTime = DateTime.Now,
                                    CreateUserId = -1,
                                }).ToList();
                                repository.InsertRange(userBulletin);
                            }
                            catch(Exception exp)
                            {
                                logger.Error(exp.ToString());
                            }
                        }
                        response = new SuccessDataResult<TEntity>(null, "AddRequestSuccess");
                    }
                    else
                    {
                        response = new ErrorDataResult<TEntity>(400, "HasWaitingApprove");
                    }
                }
                if (tableDefinition == null || tableDefinition.RoleId1 == 0)
                {
                    repository.Insert(model);
                    try
                    {
                        var tableHistory = new TableHistory
                        {
                            DealerId = 0,
                            TableDefinitionId = 0,
                            Status = TableHistoryStatus.Log,
                            ActionType = ActionType.Insert,
                            ActionDetail = "Insert Log",
                            ModelName = modelName,
                            DataId = model.Id,
                            NewData = JsonConvert.SerializeObject(model),
                            RoleId1 = 0,
                            RoleId2 = 0,
                            CreateTime = DateTime.Now,
                            UpdateTime = DateTime.Now,
                            CreateUserId = model.CreateUserId,
                            UpdateUserId = model.UpdateUserId,
                        };
                        repository.Insert(tableHistory);
                    }
                    catch (Exception exp)
                    {
                        logger.Error(exp.ToString());
                    }

                    unitOfWork.SaveChanges();
                    model = AfterAdd(model);
                    response = new SuccessDataResult<TEntity>(model);
                }
                return response;
            }
            catch (Exception exp)
            {
                logger.Error(exp.ToString());
                return new ErrorDataResult<TEntity>(500, exp.Message);
            }
        }

        public virtual TEntity AfterAdd(TEntity model)
        {
            return model;
        }

        public virtual TEntity BeforeUpdate(TEntity oldModel)
        {
            var data = Get(oldModel.Id).Data;

            oldModel.UpdateTime = DateTime.Now;
            oldModel.UpdateUserId = RequestHelper.UserId;

            data = BeforeUpdate(oldModel, data);

            return data;
        }

        public virtual TEntity BeforeUpdate(TEntity oldModel, TEntity data)
        {
            foreach (var propertyInfo in oldModel.GetType().GetProperties())
            {
                if (propertyInfo.GetCustomAttribute(typeof(UpdatableAttribute)) != null)
                {
                    if (data.GetType().GetProperty(propertyInfo.Name) != null && propertyInfo.GetValue(oldModel) != data.GetType().GetProperty(propertyInfo.Name).GetValue(data))
                    {
                        data.GetType().GetProperty(propertyInfo.Name).SetValue(data, oldModel.GetType().GetProperty(propertyInfo.Name).GetValue(oldModel));
                    }
                }
            }
            return data;
        }

        public virtual IDataResult<TEntity> Update(TEntity model)
        {
            try
            {
                IDataResult<TEntity> response = null;

                var oldData = JsonConvert.SerializeObject(Get(model.Id).Data);
                var updatedModel = BeforeUpdate(model);
                var modelName = model.GetType().Name;

                var tableDefinition = GetTableDefinitionFromCache(modelName, ActionType.Update);
                if (tableDefinition != null)
                {
                    if (!HasTableHistory(modelName, ActionType.Update, model.Id))
                    {
                        var tableHistory = new TableHistory
                        {
                            DealerId = Fatura.Common.Helpers.RequestHelper.DealerId,
                            TableDefinitionId = tableDefinition.Id,
                            ActionType = ActionType.Update,
                            Status = tableDefinition.RoleId1 > 0 ? TableHistoryStatus.New : TableHistoryStatus.Log,
                            ActionDetail = "Update",
                            ModelName = tableDefinition.ModelName,
                            DataId = model.Id,
                            OldData = oldData,
                            NewData = JsonConvert.SerializeObject(updatedModel),
                            RoleId1 = tableDefinition.RoleId1,
                            RoleId2 = tableDefinition.RoleId2,
                            CreateTime = DateTime.Now,
                            UpdateTime = DateTime.Now,
                            CreateUserId = RequestHelper.UserId,
                            UpdateUserId = updatedModel.UpdateUserId,
                        };
                        repository.Insert(tableHistory);
                        unitOfWork.SaveChanges();

                        response = new SuccessDataResult<TEntity>(null, "UpdateRequestSuccess");
                    }
                    else
                    {
                        response = new ErrorDataResult<TEntity>(400, "HasWaitingApprove");
                    }
                }
                if (tableDefinition == null || tableDefinition.RoleId1 == 0)
                {
                    repository.Update(updatedModel);
                    try
                    {
                        var tableHistory = new TableHistory
                        {
                            DealerId = Fatura.Common.Helpers.RequestHelper.DealerId,
                            TableDefinitionId = 0,
                            ActionType = ActionType.Update,
                            Status = TableHistoryStatus.Log,
                            ActionDetail = "Update Log",
                            ModelName = modelName,
                            DataId = model.Id,
                            OldData = oldData,
                            NewData = JsonConvert.SerializeObject(updatedModel),
                            RoleId1 = 0,
                            RoleId2 = 0,
                            CreateTime = DateTime.Now,
                            UpdateTime = DateTime.Now,
                            CreateUserId = RequestHelper.UserId,
                            UpdateUserId = updatedModel.UpdateUserId,
                        };
                        repository.Insert(tableHistory);
                    }
                    catch (Exception exp)
                    {
                        logger.Error(exp.ToString());
                    }
                    unitOfWork.SaveChanges();
                    updatedModel = AfterUpdate(updatedModel);
                    response = new SuccessDataResult<TEntity>(model);
                }
                return response;
            }
            catch (Exception exp)
            {
                logger.Error(exp.ToString());
                return new ErrorDataResult<TEntity>(500, exp.Message);
            }
        }

        public virtual IDataResult<TOEntity> UpdateCustom<TOEntity>(TOEntity model) where TOEntity : class, IHpModel
        {
            try
            {
                model.UpdateTime = DateTime.Now;
                repository.Update(model);
                unitOfWork.SaveChanges();
                return new SuccessDataResult<TOEntity>(model);
            }
            catch (Exception exp)
            {
                return new ErrorDataResult<TOEntity>(500, exp.Message);
            }
        }

        public virtual TEntity AfterUpdate(TEntity model)
        {
            return model;
        }

        public virtual IDataResult<TEntity> Update(TEntity model, params string[] fields)
        {
            try
            {
                model.UpdateTime = DateTime.Now;
                model.UpdateUserId = RequestHelper.UserId;

                Array.Resize(ref fields, fields.Length + 2);
                fields[fields.Length - 1] = "UpdateTime";
                fields[fields.Length - 2] = "UpdateUserId";

                repository.Update(model, fields);
                unitOfWork.SaveChanges();

                AfterUpdate(model);

                return new SuccessDataResult<TEntity>(model);
            }
            catch (Exception exp)
            {
                return new ErrorDataResult<TEntity>("InternalError " + exp.Message);
            }
        }

        public virtual IQueryable<TEntity> BeforeSearch(IQueryable<TEntity> req)
        {
            return req;
        }

        public virtual IDataResult<IEnumerable<TEntity>> Search(DataSourceLoadOptionsBase req)
        {
            try
            {
                var query = repository.GetQuery();
                query = BeforeSearch(query);

                var dataLoader = DataSourceLoader.LoadAsync(query, req).Result;
                var data = dataLoader.data.Convert<IEnumerable<TEntity>>();

                var result = new SuccessDataResult<IEnumerable<TEntity>>(data, dataLoader.totalCount);
                return result;
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }

        public virtual IDataResult<IEnumerable<TOEntity>> Search<TOEntity>(DataSourceLoadOptionsBase req) where TOEntity : class, IHpModel
        {
            try
            {
                var query = repository.GetQuery<TOEntity>();

                var dataLoader = DataSourceLoader.Load(query, req);
                var data = dataLoader.data.Convert<IEnumerable<TOEntity>>();

                var result = new SuccessDataResult<IEnumerable<TOEntity>>(data, dataLoader.totalCount);
                return result;
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }
    }
}
