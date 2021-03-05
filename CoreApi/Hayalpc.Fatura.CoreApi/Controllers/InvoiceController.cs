using Hayalpc.Fatura.Common.Dtos;
using Hayalpc.Fatura.Common.ReqRes;
using Hayalpc.Fatura.CoreApi.Services.Interfaces;
using Hayalpc.Fatura.Data.Models;
using Hayalpc.Library.Common.Extensions;
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
    public class InvoiceController : ControllerBase
    {
        private readonly IInvoiceService service;
        private readonly IInstitutionService institutionService;
        private readonly ICategoryService categoryService;

        public InvoiceController(IInvoiceService service, IInstitutionService institutionService, ICategoryService categoryService)
        {
            this.service = service;
            this.institutionService = institutionService;
            this.categoryService = categoryService;
        }

        [HttpPost("search")]
        public SearchInvoiceResponse Search(SearchInvoice searchInvoice)
        {
            var response = new SearchInvoiceResponse();
            if (ModelState.IsValid)
            {
                response.ResultCode = 0;
                response.ResultDescription = "Ok";
                response.Invoices = new List<InvoiceDto>();
                var institution = institutionService.Get().Data.FirstOrDefault(x => x.Id == searchInvoice.InstitutionId);
                var category = categoryService.Get().Data.FirstOrDefault(x => x.Id == institution.CategoryId);

                for (var i = 0; i < Faker.NumberFaker.Number(0, 3); i++)
                {
                    var inv = new Invoice
                    {
                        InstitutionId = searchInvoice.InstitutionId,
                        SubscriberNo = searchInvoice.SubscriberNo,
                        UserIp = searchInvoice.UserIp,
                        Channel = searchInvoice.Channel,
                        InstitutionName = institution.Name,
                        CategoryId = institution.CategoryId,
                        CategoryName = category.Name,
                        InvoiceNo = Faker.StringFaker.AlphaNumeric(11),
                        InvoiceDate = DateTime.Now.ToString(),
                        InvoiceOwner = Faker.NameFaker.Name(),
                        Amount = Faker.NumberFaker.Number(10, 100),
                        DelayAmount = Faker.NumberFaker.Number(0, 5),
                        Fee = Faker.NumberFaker.Number(1, 5),
                        TotalAmount = 12,
                    };
                    inv.TotalAmount = inv.Amount + inv.DelayAmount + inv.Fee;
                    var result = service.Add(inv);
                    if (result.IsSuccess)
                    {
                        response.Invoices.Add(result.Data.Convert<InvoiceDto>());
                    }
                }
            }
            else
            {
                response.ResultCode = 400;
                response.ResultDescription = "BadRequest";
            }
            return response;
        }

    }
}
