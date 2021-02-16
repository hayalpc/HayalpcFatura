using Hayalpc.Fatura.Common.Dtos;
using System.Collections.Generic;

namespace Hayalpc.Fatura.Common.ReqRes
{
    public class ChoosePaymentMethod
    {
        public List<InvoiceDto> Invoices { get; set; }
        public long InstitutionId { get; set; }
        public string SubscriberNo { get; set; }
        public string Channel { get; set; }
        public string UserIp { get; set; }
        public decimal Amount { get; set; }
        public decimal DelayAmount { get; set; }
        public decimal Fee { get; set; }
    }
}
