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
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService service;

        public CategoryController(ICategoryService service)
        {
            this.service = service;
        }

        [HttpGet]
        public IDataResult<List<CategoryDto>> Get()
        {
            return service.Get();
        }

    }
}
