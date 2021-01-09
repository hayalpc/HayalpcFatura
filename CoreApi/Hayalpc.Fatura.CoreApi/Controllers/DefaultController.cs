using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hayalpc.Fatura.CoreApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DefaultController : ControllerBase
    {

        [HttpGet(Name = nameof(Get))]
        public ActionResult Get()
        {
            return Ok();
        }

        [HttpGet]
        [Route("{id:long}", Name = nameof(Detail))]
        public ActionResult Detail(long id)
        {
            return Ok();
        }

        [HttpPost(Name = nameof(Add))]
        public ActionResult Add()
        {
            return Ok();
        }

        [HttpPut]
        [Route("{id:int}", Name = nameof(Update))]
        public ActionResult Update()
        {
            return Ok();
        }

        [HttpDelete]
        [Route("{id:int}", Name = nameof(Delete))]
        public ActionResult Delete()
        {
            return Ok();
        }

    }
}
