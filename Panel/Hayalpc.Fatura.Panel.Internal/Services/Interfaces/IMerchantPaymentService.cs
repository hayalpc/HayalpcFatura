using Hayalpc.Fatura.Data.Models;
using Hayalpc.Library.Common.Results;

namespace Hayalpc.Fatura.Panel.Internal.Services.Interfaces
{
    public interface IMerchantPaymentService : IBaseService<MerchantPayment>
    {
        IResult Calculate(long[] merchantIds);
    }
}
