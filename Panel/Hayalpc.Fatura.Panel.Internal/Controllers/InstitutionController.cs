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
    public class InstitutionController : ControllerBase
    {
        private readonly IInstitutionService service;

        public InstitutionController(IInstitutionService service)
        {
            this.service = service;
        }

        [HttpGet("{id}")]
        public IDataResult<Institution> Get(long Id)
        {
            return service.Get(Id);
        }

        [HttpPost]
        public IDataResult<Institution> Add(Institution model)
        {
            return service.Add(model);
        }

        [HttpPut]
        public IDataResult<Institution> Update(Institution model)
        {
            return service.Update(model);
        }

        [HttpPost]
        public IDataResult<IEnumerable<Institution>> Search(DataSourceLoadOptionsBase req)
        {
            return service.Search(req);
        }

    }
}
