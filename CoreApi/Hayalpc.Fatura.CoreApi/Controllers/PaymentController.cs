using Hayalpc.Fatura.Common.Enums;
using Hayalpc.Fatura.Common.ReqRes;
using Hayalpc.Fatura.CoreApi.Services.Interfaces;
using Hayalpc.Fatura.Data.Models;
using Hayalpc.Library.Common.Results;
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
        private readonly IInstitutionService institutionService;
        private readonly ICategoryService categoryService;

        public PaymentController(IInvoicePaymentService invoicePaymentService, IInvoiceService invoiceService, IInstitutionService institutionService, ICategoryService categoryService)
        {
            this.invoicePaymentService = invoicePaymentService;
            this.invoiceService = invoiceService;
            this.institutionService = institutionService;
            this.categoryService = categoryService;
        }


        [HttpPost("Methods")]
        public PaymentInvoiceResponse Methods(ChoosePaymentMethod choosePaymentMethod)
        {
            var response = new PaymentInvoiceResponse { ResultCode = 500, ResultDescription = "Hata" };

            if (choosePaymentMethod.Invoices != null && choosePaymentMethod.Invoices.Count > 0 && choosePaymentMethod.InstitutionId > 0)
            {
                var TotalAmount = .0m;
                var DelayAmount = .0m;
                var Fee = .0m;

                var institution = institutionService.Get().Data.FirstOrDefault(x => x.Id == choosePaymentMethod.InstitutionId);
                var category = categoryService.Get().Data.FirstOrDefault(x => x.Id == institution.CategoryId);

                var invoicePayment = new InvoicePayment
                {
                    Token = Guid.NewGuid(),
                    DealerId = 0,
                    DealerName = "",
                    CategoryId = category.Id,
                    CategoryName = category.Name,
                    InstitutionId = institution.Id,
                    InstitutionName = institution.Name,
                    SubscriberNo = choosePaymentMethod.SubscriberNo,
                    PaymentMethod = "none",
                    ExpireDate = DateTime.Now.AddMinutes(5),
                    PaymentChannel = choosePaymentMethod.Channel,
                    Amount = TotalAmount,
                    DelayAmount = DelayAmount,
                    Fee = Fee,
                    UserIp = choosePaymentMethod.UserIp,
                    Status= InvoicePaymentStatus.New,
                };

                var result = invoicePaymentService.Add(invoicePayment);
                if (result.IsSuccess)
                {
                    var hasError = false;
                    foreach (var inDto in choosePaymentMethod.Invoices)
                    {
                        var invoiceRes = invoiceService.Get(inDto.Id);
                        if (invoiceRes.IsSuccess)
                        {
                            TotalAmount += invoiceRes.Data.TotalAmount;
                            DelayAmount += invoiceRes.Data.DelayAmount;
                            Fee += invoiceRes.Data.Fee;
                            invoiceRes.Data.PaymentId = invoicePayment.Id;
                            invoiceRes.Data.Status = InvoiceStatus.Active;
                            invoiceService.Update(invoiceRes.Data);
                        }
                        else
                        {
                            hasError = true;
                        }
                    }
                    if (!hasError)
                    {
                        invoicePayment.Amount = TotalAmount;
                        invoicePayment.DelayAmount = DelayAmount;
                        invoicePayment.Fee = Fee;
                        invoicePayment.Status = InvoicePaymentStatus.Active;
                        invoicePaymentService.Update(invoicePayment);

                        response.ResultCode = 0;
                        response.PaymentId = invoicePayment.Id;
                        response.PaymentToken = invoicePayment.Token;
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
                    else
                    {
                        response = new PaymentInvoiceResponse { ResultCode = 500, ResultDescription = "GecersizFatura" };
                    }
                }
                else
                {
                    response = new PaymentInvoiceResponse { ResultCode = 500, ResultDescription = "GecersizOdemeOlusturma" };
                }
            }
            return response;
        }
    }
}
