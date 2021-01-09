using Hayalpc.Fatura.Data;
using Hayalpc.Fatura.Data.Models;
using Hayalpc.Fatura.Panel.Internal.Services.Interfaces;
using Hayalpc.Library.Common.Enums;
using Hayalpc.Library.Common.Helpers;
using Hayalpc.Library.Common.Results;
using Hayalpc.Library.Log;
using Hayalpc.Library.Repository.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using System;
using System.Linq;

namespace Hayalpc.Fatura.Panel.Internal.Services
{
    public class TableHistoryService : BaseService<TableHistory>, ITableHistoryService
    {
        private readonly ITableDefinitionService tableDefinitionService;
        private readonly IUserBulletinService userBulletinService;
        private readonly IUserRoleService userRoleService;

        public TableHistoryService(IRepository<TableHistory> repository, IHpLogger logger, IHpUnitOfWork<HpDbContext> unitOfWork, IMemoryCache memoryCache, ITableDefinitionService tableDefinitionService, IUserBulletinService userBulletinService, IUserRoleService userRoleService)
            : base(repository, logger, unitOfWork, memoryCache)
        {
            this.tableDefinitionService = tableDefinitionService;
            this.userBulletinService = userBulletinService;
            this.userRoleService = userRoleService;
        }

        public override IDataResult<TableHistory> Add(TableHistory model) => throw new NotImplementedException();

        public Result Approve(long Id)
        {
            Result approveResponse = null;
            var modelRes = Get(Id);
            if (modelRes.IsSuccess)
            {
                var model = modelRes.Data;
                if (model.CreateUserId != RequestHelper.UserId)
                {
                    var tableDefinition = tableDefinitionService.Get(model.TableDefinitionId).Data;
                    if (model.Status == TableHistoryStatus.New && RequestHelper.User.UserRoles.Where(x => x.RoleId == model.RoleId1).Any())
                    {
                        model.Status = tableDefinition.RoleId2 > 0 ? TableHistoryStatus.Step1 : TableHistoryStatus.Approved;
                        if(tableDefinition.RoleId2 > 0)
                        {
                            var str = "Onayınızı bekleyen talep bulunmaktadır.";
                            var userBulletin = userRoleService.GetByRoleId(tableDefinition.RoleId2 ?? 0).Select(x => new UserBulletin {
                                MerchantId = 0,
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
                            userBulletinService.AddRange(userBulletin);
                        }
                    }
                    else if (model.UpdateUserId == RequestHelper.UserId)
                    {
                        return new ErrorResult("FirstApproverCannotSecondApprove");
                    }
                    else if (model.Status == TableHistoryStatus.Step1 && RequestHelper.User.UserRoles.Where(x => x.RoleId == model.RoleId2).Any())
                    {
                        model.Status = TableHistoryStatus.Approved;
                    }
                    else
                    {
                        return new ErrorResult("InvalidApprove");
                    }

                    var fullAssembly = tableDefinition.Namespace + "." + tableDefinition.ModelName + ", " + tableDefinition.Assembly;
                    Type modelType = Type.GetType(fullAssembly);
                    if (modelType != null)
                    {
                        var result = Update(model, "Status");
                        if (result.Success)
                        {
                            if (model.Status == TableHistoryStatus.Approved)
                            {
                                try
                                {
                                    IDataResult<IHpModel> modelCusRes = null;
                                    if (tableDefinition.ActionType == ActionType.Insert)
                                    {
                                        var insertData = (IHpModel)JsonConvert.DeserializeObject(model.NewData, modelType);
                                        modelCusRes = base.AddCustom(insertData);
                                    }
                                    else if (tableDefinition.ActionType == ActionType.Update)
                                    {
                                        var updateData = (IHpModel)JsonConvert.DeserializeObject(model.NewData, modelType);
                                        modelCusRes = base.UpdateCustom(updateData);
                                    }
                                    else if (tableDefinition.ActionType == ActionType.Delete)
                                    {
                                    }
                                    else if (tableDefinition.ActionType == ActionType.Approve)
                                    {
                                    }
                                    else
                                        approveResponse = new ErrorResult("InvalidActionType");

                                    if (modelCusRes != null && modelCusRes.IsSuccess)
                                    {
                                        approveResponse = new SuccessResult("ApproveSuccess");
                                        model.DataId = modelCusRes.Data.Id;
                                        Update(model, "DataId");
                                    }
                                    else
                                    {
                                        logger.Error(modelCusRes);
                                        approveResponse = new ErrorResult("CouldNotApprove");
                                    }
                                }
                                catch (Exception exp)
                                {
                                    logger.Error(exp.ToString());
                                    approveResponse = new ErrorResult("InternalError");
                                }
                            }
                            else
                                approveResponse = new SuccessResult("WaitingNextApprove");
                        }
                        else
                        {
                            logger.Error(result);
                            approveResponse = new ErrorResult("InternalError");
                        }
                    }
                    else
                        approveResponse = new ErrorResult("InvalidModel");
                }
                else
                    approveResponse = new ErrorResult("CreatedUserCannotApprove");
            }
            else
                approveResponse = new ErrorResult("NotFound");

            return approveResponse;
        }

        public Result Reject(long Id)
        {
            var modelRes = Get(Id);
            if (modelRes.IsSuccess)
            {
                var model = modelRes.Data;
                var tableDefinition = tableDefinitionService.Get(model.TableDefinitionId).Data;
                if (model.CreateUserId == RequestHelper.UserId || RequestHelper.User.UserRoles.Where(x => x.RoleId == model.RoleId1 || x.RoleId == model.RoleId2).Any())
                {
                    model.Status = TableHistoryStatus.Rejected;
                    Update(model, "Status");
                    return new SuccessResult("RejectSuccess");
                }
                else
                {
                    return new ErrorResult("InvalidPermission");
                }
            }
            else
            {
                return new ErrorResult("NotFound");
            }
        }

        public override IQueryable<TableHistory> BeforeSearch(IQueryable<TableHistory> req)
        {
            if (RequestHelper.MerchantId > 0)
            {
                req = req.Where(x => x.MerchantId == RequestHelper.MerchantId);
            }
            return base.BeforeSearch(req);
        }
    }
}
