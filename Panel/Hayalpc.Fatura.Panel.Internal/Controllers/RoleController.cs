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
    public class RoleController : ControllerBase
    {
        private readonly IRoleService service;

        public RoleController(IRoleService service)
        {
            this.service = service;
        }

        [HttpGet("{id}")]
        public IDataResult<Role> Get(long Id)
        {
            return service.Get(Id);
        }

        [HttpGet]
        public List<Role> GetAll()
        {
            return service.GetAll();
        }

        [HttpPost]
        public IDataResult<Role> Add(Role model)
        {
            return service.Add(model);
        }

        [HttpPut]
        public IDataResult<Role> Update(Role model)
        {
            return service.Update(model);
        }

        [HttpPost]
        public IDataResult<IEnumerable<Role>> Search(DataSourceLoadOptionsBase req)
        {
            return service.Search(req);
        }

    }
}
