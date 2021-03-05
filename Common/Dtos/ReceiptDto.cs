using System;
using System.Collections.Generic;
using System.Text;

namespace Hayalpc.Fatura.Common.Dtos
{
    public class ReceiptDto
    {
        public InvoicePaymentDto InvoicePayment { get; set; }
        public IList<InvoiceDto> Invoices { get; set; }
        public CategoryDto Category { get; set; }
        public InstitutionDto Institution { get; set; }
    }
}
