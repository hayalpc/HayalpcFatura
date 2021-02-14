using Hayalpc.Fatura.Common.Dtos;
using Hayalpc.Fatura.Common.ReqRes;
using Hayalpc.Fatura.Vezne.External.Models;
using Hayalpc.Library.Common.Helpers;
using Hayalpc.Library.Common.Helpers.Interfaces;
using Hayalpc.Library.Common.Results;
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
    public class InvoiceController : Controller
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
        public SearchInvoiceResponse Search([FromForm] SearchInvoice searchInvoice)
        {
            if (session.Get("Authenticated") == "1")
            {
                searchInvoice.UserIp = RequestHelper.RemoteIp;
                searchInvoice.Channel = "faturaode";

                ModelState.Clear();
                TryValidateModel(searchInvoice);

                if (ModelState.IsValid)
                {
                    return clientHelper.Post<SearchInvoice, SearchInvoiceResponse>(AppConfigHelper.ApiUrl, "invoice/search", searchInvoice);
                }
                else
                {
                    return new SearchInvoiceResponse
                    {
                        ResultCode = 400,
                        ResultDescription = "BadRequest"
                    };
                }
            }
            else
            {
                return new SearchInvoiceResponse
                {
                    ResultCode = 401,
                    ResultDescription = "NotAuthenticated"
                };
            }
        }

    }
}
