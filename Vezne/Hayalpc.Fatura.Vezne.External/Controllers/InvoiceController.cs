using Hayalpc.Fatura.Common.Dtos;
using Hayalpc.Fatura.Vezne.External.Models;
using Hayalpc.Library.Common.Helpers.Interfaces;
using Hayalpc.Library.Log;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Hayalpc.Fatura.Vezne.External.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private readonly IHpLogger logger;
        private readonly IHttpClientHelper clientHelper;
        private readonly ISessionHelper session;

        public InvoiceController(IHpLogger logger, IHttpClientHelper clientHelper, ISessionHelper session)
        {
            this.logger = logger;
            this.clientHelper = clientHelper;
            this.session = session;
        }

        [HttpPost]
        public SearchInvoiceResponse Search([FromForm]SearchInvoice searchInvoice)
        {
            Thread.Sleep(3000);
            var response = new SearchInvoiceResponse();
            if (session.Get("Authenticated") == "1")
            {
                if (ModelState.IsValid)
                {
                    response.ResultCode = 0;
                    response.ResultDescription = "Ok";
                    response.Invoices = new List<InvoiceDto>();
                    response.Invoices.Add(new InvoiceDto
                    {
                        InstutionId = searchInvoice.InstituteId,
                        SubscriberNo = searchInvoice.SubscriberNo,
                        InstutionName = "Test",
                        InvoiceNo = "InvoiceNo123",
                        InvoiceDate = "InvoiceDate",
                        InvoiceOwner = "InvoiceOwner",
                        Amount = 10,
                        DelayAmount = 1,
                        Fee = 1,
                        TotalAmount = 12
                    });
                    response.Invoices.Add(new InvoiceDto
                    {
                        InstutionId = searchInvoice.InstituteId,
                        SubscriberNo = searchInvoice.SubscriberNo,
                        InstutionName = "Test",
                        InvoiceNo = "InvoiceNo1234",
                        InvoiceDate = "InvoiceDate",
                        InvoiceOwner = "InvoiceOwner",
                        Amount = 10,
                        DelayAmount = 1,
                        Fee = 1,
                        TotalAmount = 12
                    });
                }
                else
                {
                    response.ResultCode = 400;
                    response.ResultDescription = "BadRequest";
                }
            }
            else
            {
                response.ResultCode = 401;
                response.ResultDescription = "NotAuthenticated";
            }
            return response;
        }
    }
}
