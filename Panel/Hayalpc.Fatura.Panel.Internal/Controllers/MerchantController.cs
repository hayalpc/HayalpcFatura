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
    public class MerchantController : ControllerBase
    {
        private readonly IMerchantService service;

        public MerchantController(IMerchantService service)
        {
            this.service = service;
        }

        [HttpGet("{id}")]
        public IDataResult<Merchant> Get(long Id)
        {
            return service.Get(Id);
        }

        [HttpPost]
        public IDataResult<Merchant> Add(Merchant model)
        {
            return service.Add(model);
        }

        [HttpPut]
        public IDataResult<Merchant> Update(Merchant model)
        {
            return service.Update(model);
        }

        [HttpPost]
        public IDataResult<IEnumerable<Merchant>> Search(DataSourceLoadOptionsBase req)
        {
            return service.Search(req);
        }

    }
}
