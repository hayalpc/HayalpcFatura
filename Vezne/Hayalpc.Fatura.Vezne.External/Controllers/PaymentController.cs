using Hayalpc.Fatura.Common.Dtos;
using Hayalpc.Fatura.Common.ReqRes;
using Hayalpc.Fatura.Vezne.External.Models;
using Hayalpc.Library.Common.Helpers;
using Hayalpc.Library.Common.Helpers.Interfaces;
using Hayalpc.Library.Common.Results;
using Hayalpc.Library.Log;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace Hayalpc.Fatura.Vezne.External.Controllers
{
    [Route("[controller]/[action]")]
    public class PaymentController : Controller
    {
        private readonly IHpLogger logger;
        private readonly IHttpClientHelper clientHelper;
        private readonly ISessionHelper session;

        public PaymentController(IHpLogger logger, IHttpClientHelper clientHelper, ISessionHelper session)
        {
            this.logger = logger;
            this.clientHelper = clientHelper;
            this.session = session;
        }

        [HttpPost]
        public CreditCardPaymentResponse CreditCard([FromForm] CreditCardPayment creditCardPayment)
        {
            if (ModelState.IsValid)
            {
                if (creditCardPayment.CardNumber == "1111111111111111")
                {
                    var invoicePaymentRes = clientHelper.Post<CreditCardPayment, Result>(AppConfigHelper.ApiUrl, $"invoicePayment/creditCardPayment", creditCardPayment);

                    return new CreditCardPaymentResponse { ResultCode = 0, ResultDescription = "Ok", Url = "/creditCard/demoOtp/" + creditCardPayment.PaymentToken };
                }
                else
                {
                    return new CreditCardPaymentResponse { ResultCode = 400, ResultDescription = "InvalidCardInformation" };
                }
            }
            else
            {
                return new CreditCardPaymentResponse { ResultCode = 400, ResultDescription = "InvalidCardForm" };
            }
        }

        [HttpPost]
        public IActionResult Methods([FromForm] ChoosePaymentMethod choosePaymentMethod)
        {
            var response = new PaymentInvoiceResponse { ResultCode = 500, ResultDescription = "Hata" };
            try
            {
                choosePaymentMethod.Channel = "faturaode";
                choosePaymentMethod.UserIp = RequestHelper.RemoteIp;
                response = clientHelper.Post<ChoosePaymentMethod, PaymentInvoiceResponse>(AppConfigHelper.ApiUrl, "payment/methods", choosePaymentMethod);
            }
            catch (Exception exp)
            {

            }
            return View("PaymentMethod", response);
        }

       

    }
}
