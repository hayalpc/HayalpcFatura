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
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService service;

        public CategoryController(ICategoryService service)
        {
            this.service = service;
        }

        [HttpGet("{id}")]
        public IDataResult<Category> Get(long Id)
        {
            return service.Get(Id);
        }

        [HttpPost]
        public IDataResult<Category> Add(Category model)
        {
            return service.Add(model);
        }

        [HttpPut]
        public IDataResult<Category> Update(Category model)
        {
            return service.Update(model);
        }

        [HttpPost]
        public IDataResult<IEnumerable<Category>> Search(DataSourceLoadOptionsBase req)
        {
            return service.Search(req);
        }

    }
}
