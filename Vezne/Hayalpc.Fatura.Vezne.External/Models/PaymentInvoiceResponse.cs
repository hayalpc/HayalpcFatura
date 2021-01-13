using System.Collections.Generic;

namespace Hayalpc.Fatura.Vezne.External.Models
{
    public class PaymentInvoiceResponse
    {
        public int Status { get; set; }
        public string Message { get; set; }
        public List<PaymentMethod> PaymentMethods { get; set; }
    }
}
