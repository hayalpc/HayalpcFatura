using Hayalpc.Library.Common.Results;
using Hayalpc.Fatura.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hayalpc.Library.Common.DataTables;

namespace Hayalpc.Fatura.Panel.Internal.Services.Interfaces
{
    public interface IServiceService : IBaseService<Service>
    {
        IDataResult<List<Service>> GetByMerchant(long Id);
    }
}
