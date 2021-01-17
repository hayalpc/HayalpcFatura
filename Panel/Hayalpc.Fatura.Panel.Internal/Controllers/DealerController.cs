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
    public class DealerController : ControllerBase
    {
        private readonly IDealerService service;

        public DealerController(IDealerService service)
        {
            this.service = service;
        }

        [HttpGet("{id}")]
        public IDataResult<Dealer> Get(long Id)
        {
            return service.Get(Id);
        }

        [HttpPost]
        public IDataResult<Dealer> Add(Dealer model)
        {
            return service.Add(model);
        }

        [HttpPut]
        public IDataResult<Dealer> Update(Dealer model)
        {
            return service.Update(model);
        }

        [HttpPost]
        public IDataResult<IEnumerable<Dealer>> Search(DataSourceLoadOptionsBase req)
        {
            return service.Search(req);
        }

    }
}
