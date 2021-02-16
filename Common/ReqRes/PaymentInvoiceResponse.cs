using System;
using System.Collections.Generic;

namespace Hayalpc.Fatura.Common.ReqRes
{
    public class PaymentInvoiceResponse : BaseResponse
    {
        public long PaymentId { get; set; }
        public Guid PaymentToken { get; set; }
        public List<PaymentMethod> PaymentMethods { get; set; }
    }
}
