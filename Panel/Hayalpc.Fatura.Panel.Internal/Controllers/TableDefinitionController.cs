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
    public class TableDefinitionController : ControllerBase
    {
        private readonly ITableDefinitionService service;

        public TableDefinitionController(ITableDefinitionService service)
        {
            this.service = service;
        }

        [HttpGet("{id}")]
        public IDataResult<TableDefinition> Get(long Id)
        {
            return service.Get(Id);
        }

        [HttpPost]
        public IDataResult<TableDefinition> Add(TableDefinition model)
        {
            return service.Add(model);
        }

        [HttpPut]
        public IDataResult<TableDefinition> Update(TableDefinition model)
        {
            return service.Update(model);
        }

        [HttpPost]
        public IDataResult<IEnumerable<TableDefinition>> Search(DataSourceLoadOptionsBase req)
        {
            return service.Search(req);
        }

    }
}
