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
        IDataResult<InvoicePayment> Add(InvoicePayment invoice);
    }
}
