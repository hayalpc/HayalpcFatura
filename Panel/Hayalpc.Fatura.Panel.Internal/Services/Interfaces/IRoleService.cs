using Hayalpc.Library.Common.DataTables;
using Hayalpc.Library.Common.Results;
using Hayalpc.Fatura.Data.Models;
using System.Collections.Generic;

namespace Hayalpc.Fatura.Panel.Internal.Services.Interfaces
{
    public interface IRoleService:IBaseService<Role>
    {
        List<Role> GetAll();
    }
}
