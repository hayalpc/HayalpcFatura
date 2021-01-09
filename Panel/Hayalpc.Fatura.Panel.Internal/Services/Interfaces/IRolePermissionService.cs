using Hayalpc.Library.Common.Results;
using Hayalpc.Fatura.Data.Models;
using System.Collections.Generic;

namespace Hayalpc.Fatura.Panel.Internal.Services.Interfaces
{
    public interface IRolePermissionService : IBaseService<RolePermission>
    {
        List<RolePermission> GetAll();
        List<RolePermission> GetByRoleId(long roleId);
        IResult Delete(long Id);
    }
}
