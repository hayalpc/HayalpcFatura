using Hayalpc.Library.Common.Helpers.Interfaces;
using Hayalpc.Library.Log;
using Hayalpc.Fatura.Panel.External.Services.Interfaces;
using Hayalpc.Fatura.Panel.External.Models;

namespace Hayalpc.Fatura.Panel.External.Services
{
    public class InvoiceService : BaseService<InvoiceVM>, IInvoiceService
    {
        public InvoiceService(IHttpClientHelper clientHelper, IHpLogger logger) : base(clientHelper, logger,"invoice")
        {
        }
      
    }
}
