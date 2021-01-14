using Hayalpc.Fatura.Common.Dtos;
using System.Collections.Generic;

namespace Hayalpc.Fatura.Vezne.External.Models
{
    public class SearchInvoiceResponse : BaseResponse
    {
        public List<InvoiceDto> Invoices { get; set; } = new List<InvoiceDto>();
        public int InvoiceCount { get => Invoices.Count; }
    }
}
