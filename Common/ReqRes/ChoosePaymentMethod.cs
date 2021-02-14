using Hayalpc.Fatura.Common.Dtos;
using System.Collections.Generic;

namespace Hayalpc.Fatura.Common.ReqRes
{
    public class ChoosePaymentMethod
    {
        public List<InvoiceDto> Invoices { get; set; }
    }
}
