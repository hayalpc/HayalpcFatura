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
    public class ServiceController : ControllerBase
    {
        private readonly IServiceService service;

        public ServiceController(IServiceService service)
        {
            this.service = service;
        }

        [HttpGet("{id}")]
        public IDataResult<Service> Get(long Id)
        {
            return service.Get(Id);
        }

        [HttpGet("{id}")]
        public IDataResult<List<Service>> GetByMerchant(long Id)
        {
            return service.GetByMerchant(Id);
        }

        [HttpPost]
        public IDataResult<Service> Add(Service model)
        {
            return service.Add(model);
        }

        [HttpPut]
        public IDataResult<Service> Update(Service model)
        {
            return service.Update(model);
        }

        [HttpPost]
        public IDataResult<IEnumerable<Service>> Search(DataSourceLoadOptionsBase req)
        {
            return service.Search(req);
        }

    }
}
