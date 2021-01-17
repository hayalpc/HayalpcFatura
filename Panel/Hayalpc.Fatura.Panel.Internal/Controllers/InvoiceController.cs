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
    public class InvoiceController : ControllerBase
    {
        private readonly IInvoiceService service;

        public InvoiceController(IInvoiceService service)
        {
            this.service = service;
        }

        [HttpGet("{id}")]
        public IDataResult<Invoice> Get(long Id)
        {
            return service.Get(Id);
        }

        [HttpPost]
        public IDataResult<IEnumerable<Invoice>> Search(DataSourceLoadOptionsBase req)
        {
            return service.Search(req);
        }

    }
}
