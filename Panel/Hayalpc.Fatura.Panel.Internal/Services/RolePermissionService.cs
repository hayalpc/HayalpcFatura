using Hayalpc.Library.Common.Results;
using Hayalpc.Library.Log;
using Hayalpc.Library.Repository.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using NLog;
using Hayalpc.Fatura.Data;
using Hayalpc.Fatura.Data.Models;
using Hayalpc.Fatura.Panel.Internal.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hayalpc.Fatura.Panel.Internal.Services
{
    public class RolePermissionService : BaseService<RolePermission>, IRolePermissionService
    {
        public RolePermissionService(IRepository<RolePermission> repository, IHpUnitOfWork<HpDbContext> unitOfWork, IMemoryCache memoryCache,IHpLogger logger)
            :base(repository,logger,unitOfWork,memoryCache)
        {
        }

        public List<RolePermission> GetAll() => memoryCache.GetOrCreate("RolePermissionGetAll", entry =>
        {
            entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(60);
            return repository.GetQuery(t => t.Status == Hayalpc.Library.Common.Enums.Status.Active).ToList();
        });

        public List<RolePermission> GetByRoleId(long roleId) => memoryCache.GetOrCreate("RolePermissionGetByRoleId-" + roleId, entry =>
        {
            entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(60);
            return repository.GetQuery(t => t.Status == Hayalpc.Library.Common.Enums.Status.Active && t.RoleId == roleId).ToList();
        });

        public override RolePermission AfterAdd(RolePermission model)
        {
            ClearCache(model.RoleId.ToString());

            return base.AfterAdd(model);
        }

        public override RolePermission AfterUpdate(RolePermission model)
        {
            ClearCache(model.RoleId.ToString());

            return base.AfterUpdate(model);
        }

        public IResult Delete(long id)
        {
            try
            {
                var entity = repository.GetById(id);

                entity.Status = Hayalpc.Library.Common.Enums.Status.Passive;

                return base.Update(entity);
            }
            catch (Exception exp)
            {
                logger.Error(exp.ToString());
                return new ErrorResult("InternalError");
            }
        }

        private void ClearCache(string kk = "")
        {
            memoryCache.Remove("RolePermissionGetByRoleId-" + kk);
            memoryCache.Remove("RolePermissionGetAll");
        }
       
    }
}
