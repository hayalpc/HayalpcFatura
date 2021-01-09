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
    public class TariffController : ControllerBase
    {
        private readonly ITariffService service;

        public TariffController(ITariffService service)
        {
            this.service = service;
        }

        [HttpGet("{id}")]
        public IDataResult<Tariff> Get(long Id)
        {
            return service.Get(Id);
        }

        [HttpPost]
        public IDataResult<Tariff> Add(Tariff model)
        {
            return service.Add(model);
        }

        [HttpPut]
        public IDataResult<Tariff> Update(Tariff model)
        {
            return service.Update(model);
        }

        [HttpPost]
        public IDataResult<IEnumerable<Tariff>> Search(DataSourceLoadOptionsBase req)
        {
            return service.Search(req);
        }

    }
}
