using Hayalpc.Fatura.Common.Dtos;
using Hayalpc.Library.Common.Helpers;
using Hayalpc.Library.Common.Helpers.Interfaces;
using Hayalpc.Library.Common.Results;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hayalpc.Fatura.Vezne.External.Controllers
{
    [Route("[controller]")]
    public class ReceiptController : Controller
    {
        private readonly IHttpClientHelper clientHelper;
        private readonly ISessionHelper session;

        public ReceiptController(IHttpClientHelper clientHelper, ISessionHelper session)
        {
            this.clientHelper = clientHelper;
            this.session = session;
        }

        [HttpGet("{id:long}/{token:guid}")]
        public IActionResult Index(long id,Guid token)
        {
            var invoicePaymentRes = clientHelper.Get<DataResult<ReceiptDto>>(AppConfigHelper.ApiUrl, $"invoicePayment/receipt/{id}/{token}");
            if (invoicePaymentRes != null && invoicePaymentRes.IsSuccess)
            {
                return View(invoicePaymentRes.Data);
            }
            else
            {
                return NotFound();
            }
        }
    }
}
