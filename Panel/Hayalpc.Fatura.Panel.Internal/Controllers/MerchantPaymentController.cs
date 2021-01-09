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
    public class MerchantPaymentController : ControllerBase
    {
        private readonly IMerchantPaymentService service;

        public MerchantPaymentController(IMerchantPaymentService service)
        {
            this.service = service;
        }

        [HttpGet("{id}")]
        public IDataResult<MerchantPayment> Get(long Id)
        {
            return service.Get(Id);
        }

        [HttpPost]
        public IResult Calculate(long[] merchantIds)
        {
            return service.Calculate(merchantIds);
        }

        [HttpPut]
        public IDataResult<MerchantPayment> Update(MerchantPayment model)
        {
            return service.Update(model);
        }

        [HttpPost]
        public IDataResult<IEnumerable<MerchantPayment>> Search(DataSourceLoadOptionsBase req)
        {
            return service.Search(req);
        }

    }
}
