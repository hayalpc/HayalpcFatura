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
    [Route("[controller]/[action]")]
    public class CreditCardController : Controller
    {
        private readonly IHttpClientHelper clientHelper;
        private readonly ISessionHelper session;

        public CreditCardController(IHttpClientHelper clientHelper, ISessionHelper session)
        {
            this.clientHelper = clientHelper;
            this.session = session;
        }

        [HttpGet("{token:guid}")]
        public IActionResult DemoOtp(Guid token)
        {
            var invoicePayment = clientHelper.Get<DataResult<InvoicePaymentDto>>(AppConfigHelper.ApiUrl, $"invoicePayment/{token}");
            if (invoicePayment != null && (!invoicePayment.IsSuccess || invoicePayment.Data.CreateTime < DateTime.Now.AddMinutes(-5)))
                return NotFound();
            return View("Result", invoicePayment.Data);
            return View(invoicePayment);
        }

        [HttpPost("{token:guid}")]
        public IActionResult DemoOtp(Guid token, [FromForm] long otp)
        {
            var invoicePaymentRes = clientHelper.Get<DataResult<InvoicePaymentDto>>(AppConfigHelper.ApiUrl, $"invoicePayment/{token}");
            if (invoicePaymentRes != null && !invoicePaymentRes.IsSuccess)
                return NotFound();
            var invoicePayment = invoicePaymentRes.Data;
            var oldStatus = invoicePayment.Status;

            var invoicePaymentX = new InvoicePaymentDto
            {
                Id = invoicePayment.Id,
                Token = invoicePayment.Token,
                Status = invoicePayment.Status,
                Error = invoicePayment.Error,
                ErrDesc = invoicePayment.ErrDesc,
                ExpireDate = invoicePayment.ExpireDate,
                PaymentDate = invoicePayment.PaymentDate,
                PaymentMethod = invoicePayment.PaymentMethod,
                RemoteTransId = invoicePayment.RemoteTransId
            };

            if (otp == 111111)
            {
                invoicePaymentX.Status = Common.Enums.InvoicePaymentStatus.Error;
                invoicePaymentX.Error = "DemoErr";
                invoicePaymentX.ErrDesc = "Demo Hatası";
            }
            else if (otp == 222222)
            {
                invoicePaymentX.Status = Common.Enums.InvoicePaymentStatus.Timeout;
                invoicePaymentX.Error = "DemoTimeout";
                invoicePaymentX.ErrDesc = "Demo Zaman Aşımı";
                invoicePaymentX.ExpireDate = DateTime.Now;
            }
            else if (otp == 000000)
            {
                invoicePaymentX.Status = Common.Enums.InvoicePaymentStatus.Success;
                invoicePaymentX.Error = "DemoSuccess";
                invoicePaymentX.PaymentDate = DateTime.Now;
                invoicePaymentX.PaymentMethod = "creditcard";
                invoicePaymentX.RemoteTransId = Guid.NewGuid().ToString("D");
            }
            else
            {
                session.SetErrorMessage("Girdiğiniz şifre yanlıştır. Lütfen kontrol edip tekrar deneyniz!");
            }
            if (oldStatus != invoicePaymentX.Status)
            {
                var resultRes = clientHelper.Post<InvoicePaymentDto, DataResult<InvoicePaymentDto>>(AppConfigHelper.ApiUrl, $"invoicePayment/result", invoicePaymentX);
                if (resultRes != null && resultRes.IsSuccess)
                {
                    return View("Result", resultRes.Data);
                }
                else
                {
                    session.SetErrorMessage("Ödeme işleminiz alınırken bir hata ile karşılaşıldı.. Lütfen daha sonra tekrar deneyiniz!");
                }
            }
            return View(invoicePaymentRes);
        }
    }
}
