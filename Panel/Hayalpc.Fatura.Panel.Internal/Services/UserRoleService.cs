using Hayalpc.Library.Common.Results;
using Hayalpc.Library.Log;
using Hayalpc.Library.Repository.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using Hayalpc.Fatura.Data;
using Hayalpc.Fatura.Data.Models;
using Hayalpc.Fatura.Panel.Internal.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using Hayalpc.Library.Common.Helpers;

namespace Hayalpc.Fatura.Panel.Internal.Services
{
    public class UserRoleService : IUserRoleService
    {
        private readonly IHpLogger logger;
        private readonly IRepository<UserRole> repository;
        private readonly IMemoryCache memoryCache;
        private readonly IHpUnitOfWork<HpDbContext> unitOfWork;

        public UserRoleService(IHpLogger logger, IRepository<UserRole> repository, IMemoryCache memoryCache, IHpUnitOfWork<HpDbContext> unitOfWork)
        {
            this.logger = logger;
            this.repository = repository;
            this.memoryCache = memoryCache;
            this.unitOfWork = unitOfWork;
        }

        public List<UserRole> GetAll() => memoryCache.GetOrCreate("UserRoleGetAll", entry =>
        {
            entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(60);
            return repository.GetQuery().ToList();
        });

        public List<UserRole> GetByRoleId(long roleId) => repository.GetQuery(u => u.RoleId == roleId).Select(t => new UserRole
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

        public List<UserRole> GetByUserId(long userId) => memoryCache.GetOrCreate("UserRoleGetByUserId-" + userId, entry =>
        {
            entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(60);
            return repository.GetQuery(u => u.UserId == userId).Select(t => new UserRole
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
        });

        public IResult DeleteByUserId(long userId)
        {
            var roles = repository.GetQuery(x => x.UserId == userId);
            foreach (var role in roles)
            {
                repository.Delete(role);
            }
            unitOfWork.SaveChanges();

            ClearCache(userId.ToString());
            return new SuccessResult();
        }

        public IDataResult<UserRole> Add(UserRole model)
        {
            try
            {
                model.CreateTime = DateTime.Now;
                model.CreateUserId = RequestHelper.UserId;

                repository.Insert(model);
                unitOfWork.SaveChanges();

                ClearCache();
                return new SuccessDataResult<UserRole>(model);
            }
            catch (Exception exp)
            {
                logger.Error(exp.ToString());
                return new ErrorDataResult<UserRole>(500, exp.Message);
            }
        }

        public IResult AddRange(List<UserRole> model)
        {
            try
            {
                repository.InsertRange(model);
                unitOfWork.SaveChanges();

                ClearCache();
                return new SuccessResult();
            }
            catch (Exception exp)
            {
                logger.Error(exp.ToString());
                return new ErrorResult(500, exp.Message);
            }
        }

        private void ClearCache(string kk = "")
        {
            memoryCache.Remove("UserRoleGetByUserId-" + kk);
            memoryCache.Remove("UserRoleGetAll");
        }

    }
}
