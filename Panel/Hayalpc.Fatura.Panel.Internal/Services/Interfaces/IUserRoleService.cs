using Hayalpc.Library.Common.Results;
using Hayalpc.Fatura.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hayalpc.Fatura.Panel.Internal.Services.Interfaces
{
    public interface IUserRoleService
    {
        List<UserRole> GetByRoleId(long roleId);
        List<UserRole> GetByUserId(long userId);
        IResult DeleteByUserId(long userId);
        IDataResult<UserRole> Add(UserRole userRole);
        IResult AddRange(List<UserRole> userRole);
    }
}
