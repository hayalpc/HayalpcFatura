using Hayalpc.Fatura.Common.Dtos;
using System.Collections.Generic;

namespace Hayalpc.Fatura.Vezne.External.Models
{
    public class SearchInvoiceResponse
    {
        public bool IsSuccess { get => ResultCode == 0; }
        public int ResultCode { get; set; }
        public string ResultDescription { get; set; }
        public List<InvoiceDto> Invoices { get; set; } = new List<InvoiceDto>();
        public int InvoiceCount { get => Invoices.Count; }
    }
}
