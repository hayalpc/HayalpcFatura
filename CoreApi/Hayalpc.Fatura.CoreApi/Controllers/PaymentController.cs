using Hayalpc.Fatura.Common.ReqRes;
using Hayalpc.Fatura.CoreApi.Services.Interfaces;
using Hayalpc.Fatura.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hayalpc.Fatura.CoreApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IInvoicePaymentService invoicePaymentService;
        private readonly IInvoiceService invoiceService;

        public PaymentController(IInvoicePaymentService invoicePaymentService, IInvoiceService invoiceService)
        {
            this.invoicePaymentService = invoicePaymentService;
            this.invoiceService = invoiceService;
        }

        [HttpPost("Methods")]
        public PaymentInvoiceResponse  Methods(ChoosePaymentMethod choosePaymentMethod)
        {
            var response = new PaymentInvoiceResponse { ResultCode = 500, ResultDescription = "Hata" };

            if (choosePaymentMethod.Invoices != null && choosePaymentMethod.Invoices.Count > 0)
            {

                var invoicePaument = new InvoicePayment
                {
                    DealerId=0,
                    DealerName="",
                    CategoryId=0,
                    CategoryName = "",
                    InstitutionId=0,
                    InstitutionName="",
                    SubscriberNo="",
                    PaymentMethod="",
                    ExpireDate=DateTime.Now.AddMinutes(5),
                    PaymentChannel="",
                    MaskedData="",
                    Amount=0,
                    DelayAmount=0,
                    Fee=0,
                    UserIp="",

                };



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
            }
            return response;
        }
    }
}
