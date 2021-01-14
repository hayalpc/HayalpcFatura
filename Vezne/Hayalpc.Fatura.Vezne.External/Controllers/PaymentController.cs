using Hayalpc.Fatura.Vezne.External.Models;
using Hayalpc.Library.Common.Helpers.Interfaces;
using Hayalpc.Library.Log;
using Microsoft.AspNetCore.Mvc;
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
                return new CreditCardPaymentResponse { ResultCode = 0, ResultDescription = "Ok" };
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

            //if(choosePaymentMethod.Invoices != null && choosePaymentMethod.Invoices.Count > 0)
            //{
            response.ResultCode = 0;
            response.PaymentId = 1;
            response.ResultDescription = "Ok";
            response.PaymentMethods = new List<PaymentMethod>
                {
                    new PaymentMethod
                    {
                        Id=1,
                        PaymentId=1,
                        Name="Kredi Kartı",
                        Description="Kredi Kartı",
                        Type="creditcard",
                        Logo="/assets/img/methods/creditcard.png",
                    },
                };
            //}
            return View("PaymentMethod", response);
        }
    }
}
