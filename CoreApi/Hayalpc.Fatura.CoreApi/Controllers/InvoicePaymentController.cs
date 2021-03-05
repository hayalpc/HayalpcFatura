using Hayalpc.Fatura.Common.Dtos;
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
    public class InvoicePaymentController : ControllerBase
    {
        private readonly IInvoicePaymentService invoicePaymentService;

        public InvoicePaymentController(IInvoicePaymentService invoicePaymentService)
        {
            this.invoicePaymentService = invoicePaymentService;
        }

        [HttpGet("{token:guid}")]
        public IDataResult<InvoicePayment> GetByToken(Guid token)
        {
            return invoicePaymentService.GetByToken(token);
        }

        [HttpPost("creditCardPayment")]
        public IResult CreditCardPayment(CreditCardPayment creditCardPayment)
        {
            var invoicePayment = invoicePaymentService.GetByToken(creditCardPayment.PaymentToken)?.Data;
            if (invoicePayment == null)
                return new ErrorResult();
            invoicePayment.Value1 = creditCardPayment.NameSurname;
            invoicePayment.MaskedData = creditCardPayment.CardNumber.Substring(0, 4) + "********" + creditCardPayment.CardNumber.Substring(12, 4);
            invoicePaymentService.Update(invoicePayment);
            return new SuccessResult();
        }

        [HttpPost("result")]
        public IDataResult<InvoicePayment> Result(InvoicePayment invoicePaymentReq)
        {
            var invoicePaymentRes = invoicePaymentService.GetByToken(invoicePaymentReq.Token)?.Data;
            if(invoicePaymentRes != null)
            {
                invoicePaymentRes.Status = invoicePaymentReq.Status;
                invoicePaymentRes.Error = invoicePaymentReq.Error;
                invoicePaymentRes.ErrDesc = invoicePaymentReq.ErrDesc;
                invoicePaymentRes.ExpireDate = invoicePaymentReq.ExpireDate;
                invoicePaymentRes.PaymentDate = invoicePaymentReq.PaymentDate;
                invoicePaymentRes.PaymentMethod = invoicePaymentReq.PaymentMethod;
                invoicePaymentRes.RemoteTransId = invoicePaymentReq.RemoteTransId;
                return invoicePaymentService.Update(invoicePaymentRes);
            }
            else
            {
                return new ErrorDataResult<InvoicePayment>(404, "NotFound");
            }
        }

        [HttpGet("receipt/{id:long}/{token:guid}")]
        public IDataResult<ReceiptDto> Receipt(long id,Guid token)
        {
            return invoicePaymentService.Receipt(id, token);
        }

    }
}
