using System.ComponentModel.DataAnnotations;

namespace Hayalpc.Fatura.Vezne.External.Models
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
    }
}
