using System;
using System.ComponentModel.DataAnnotations;

namespace Hayalpc.Fatura.Common.ReqRes
{
    public class CreditCardPayment
    {
        [Required]
        public string CardNumber { get; set; }
        [Required]
        public string CardExpiry { get; set; }
        [Required]
        public string CardCvc { get; set; }
        [Required]
        public string NameSurname { get; set; }
        [Required]
        public long PaymentId { get; set; }
        [Required]
        public Guid PaymentToken { get; set; }
    }
}
