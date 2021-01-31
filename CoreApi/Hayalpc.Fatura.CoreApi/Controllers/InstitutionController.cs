using Hayalpc.Fatura.Common.Dtos;
using Hayalpc.Fatura.CoreApi.Services.Interfaces;
using Hayalpc.Library.Common.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hayalpc.Fatura.CoreApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InstitutionController : ControllerBase
    {
        private readonly IInstitutionService service;

        public InstitutionController(IInstitutionService service)
        {
            this.service = service;
        }

        [HttpGet]
        public IDataResult<List<InstitutionDto>> Get()
        {
            return service.Get();
        }

    }
}
