using Hayalpc.Fatura.Data.Models;
using Hayalpc.Library.Common.Results;

namespace Hayalpc.Fatura.Panel.Internal.Services.Interfaces
{
    public interface ITableHistoryService : IBaseService<TableHistory>
    {
        Result Approve(long Id);
        Result Reject(long Id);
    }
}
