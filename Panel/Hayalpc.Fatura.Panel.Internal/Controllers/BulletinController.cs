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
    public class BulletinController : ControllerBase
    {
        private readonly IUserBulletinService service;

        public BulletinController(IUserBulletinService service)
        {
            this.service = service;
        }

        [HttpGet("{id}")]
        public IDataResult<UserBulletin> Get(long Id)
        {
            service.Read(Id);
            return service.Get(Id);
        }

        [HttpGet("{id}")]
        public IResult Read(long Id)
        {
            return service.Read(Id);
        }

        [HttpGet]
        public IResult ReadAll()
        {
            return service.ReadAll();
        }

        [HttpPost]
        public IDataResult<UserBulletin> Add(UserBulletin model)
        {
            return service.Add(model);
        }

        [HttpPut]
        public IDataResult<UserBulletin> Update(UserBulletin model)
        {
            return service.Update(model);
        }

        [HttpPost]
        public IDataResult<IEnumerable<UserBulletin>> Search(DataSourceLoadOptionsBase req)
        {
            return service.Search(req);
        }

    }
}
