using Hayalpc.Library.Common.Results;
using Microsoft.AspNetCore.Mvc;
using Hayalpc.Fatura.Data.Models;
using Hayalpc.Fatura.Panel.Internal.Services.Interfaces;
using System.Collections.Generic;
using DevExtreme.AspNet.Data;

namespace Hayalpc.Fatura.Panel.Internal.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class InvoicePaymentController : ControllerBase
    {
        private readonly IInvoicePaymentService service;

        public InvoicePaymentController(IInvoicePaymentService service)
        {
            this.service = service;
        }

        [HttpGet("{id}")]
        public IDataResult<InvoicePayment> Get(long Id)
        {
            return service.Get(Id);
        }

        [HttpPost]
        public IDataResult<IEnumerable<InvoicePayment>> Search(DataSourceLoadOptionsBase req)
        {
            return service.Search(req);
        }

    }
}
