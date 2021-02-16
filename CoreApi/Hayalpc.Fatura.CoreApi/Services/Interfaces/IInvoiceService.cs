using Hayalpc.Fatura.Data.Models;
using Hayalpc.Library.Common.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hayalpc.Fatura.CoreApi.Services.Interfaces
{
    public interface IInvoiceService
    {
        IDataResult<Invoice> Add(Invoice invoice);
        IDataResult<Invoice> Get(long invoiceId);
        IDataResult<Invoice> Update(Invoice invoice);


    }
}
