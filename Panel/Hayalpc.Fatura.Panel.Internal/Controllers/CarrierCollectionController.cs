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
    public class CarrierCollectionController : ControllerBase
    {
        private readonly ICarrierCollectionService service;

        public CarrierCollectionController(ICarrierCollectionService service)
        {
            this.service = service;
        }

        [HttpGet("{id}")]
        public IDataResult<CarrierCollection> Get(long Id)
        {
            return service.Get(Id);
        }

        [HttpPut]
        public IDataResult<CarrierCollection> Update(CarrierCollection model)
        {
            return service.Update(model);
        }

        [HttpPost]
        public IDataResult<CarrierCollection> Add(CarrierCollection model)
        {
            return service.Add(model);
        }

        [HttpPost]
        public IDataResult<IEnumerable<CarrierCollection>> Search(DataSourceLoadOptionsBase req)
        {
            return service.Search(req);
        }

    }
}
