using System.Collections.Generic;
using Hayalpc.Library.Common.Results;
using Microsoft.AspNetCore.Mvc;
using Hayalpc.Fatura.Data.Models;
using Hayalpc.Fatura.Panel.Internal.Services.Interfaces;
using DevExtreme.AspNet.Data;

namespace Hayalpc.Fatura.Panel.Internal.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TableHistoryController : ControllerBase
    {
        private readonly ITableHistoryService service;

        public TableHistoryController(ITableHistoryService service)
        {
            this.service = service;
        }

        [HttpGet("{id}")]
        public IDataResult<TableHistory> Get(long Id)
        {
            return service.Get(Id);
        }

        [HttpPost]
        public IDataResult<IEnumerable<TableHistory>> Search(DataSourceLoadOptionsBase req)
        {
            return service.Search(req);
        }

        [HttpGet("{id}")]
        public Result Approve(long Id)
        {
            return service.Approve(Id);
        }

        [HttpGet("{id}")]
        public Result Reject(long Id)
        {
            return service.Reject(Id);
        }

    }
}
