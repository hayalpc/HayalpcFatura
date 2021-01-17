using Hayalpc.Library.Common.Helpers.Interfaces;
using Hayalpc.Library.Log;
using Hayalpc.Fatura.Panel.External.Services.Interfaces;
using Hayalpc.Fatura.Panel.External.Models;

namespace Hayalpc.Fatura.Panel.External.Services
{
    public class InvoicePaymentService : BaseService<InvoicePaymentVM>, IInvoicePaymentService
    {
        public InvoicePaymentService(IHttpClientHelper clientHelper, IHpLogger logger) : base(clientHelper, logger,"invoicePayment")
        {
        }
      
    }
}
