using Hayalpc.Fatura.Common.Dtos;
using System.Collections.Generic;

namespace Hayalpc.Fatura.Vezne.External.Models
{
    public class ChoosePaymentMethod
    {
        public List<InvoiceDto> Invoices { get; set; }
    }
}
