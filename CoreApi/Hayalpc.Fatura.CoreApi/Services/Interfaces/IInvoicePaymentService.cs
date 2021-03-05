using Hayalpc.Fatura.Common.Dtos;
using Hayalpc.Fatura.Data.Models;
using Hayalpc.Library.Common.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hayalpc.Fatura.CoreApi.Services.Interfaces
{
    public interface IInvoicePaymentService
    {
        IDataResult<InvoicePayment> GetByToken(Guid token);
        IDataResult<InvoicePayment> Add(InvoicePayment invoice);
        IDataResult<InvoicePayment> Update(InvoicePayment invoice);

        IDataResult<ReceiptDto> Receipt(long id, Guid token);
    }
}
