using System.Collections.Generic;

namespace Hayalpc.Fatura.Vezne.External.Models
{
    public class PaymentInvoiceResponse : BaseResponse
    {
        public long PaymentId { get; set; }
        public List<PaymentMethod> PaymentMethods { get; set; }
    }
}
