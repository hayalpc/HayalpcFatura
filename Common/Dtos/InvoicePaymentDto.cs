using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Hayalpc.Fatura.Common.Dtos
{
    public class InvoicePaymentDto
    {
        [Required]
        public Guid Token { get; set; }

        [Required]
        public long DealerId { get; set; }

        [Required]
        [StringLength(128)]
        public long DealerName { get; set; }

        [Required]
        public long CategoryId { get; set; }

        [Required]
        [StringLength(128)]
        public long CategoryName { get; set; }

        [Required]
        public long InstitutionId { get; set; }

        [Required]
        [StringLength(128)]
        public string InstitutionName { get; set; }

        [Required]
        [StringLength(64)]
        public string SubscriberNo { get; set; }

        [Required]
        [StringLength(64)]
        public string PaymentMethod { get; set; }

        public DateTime? PaymentDate { get; set; }

        [Required]
        public DateTime ExpireDate { get; set; }

        [Required]
        [StringLength(64)]
        public string PaymentChannel { get; set; }

        [StringLength(128)]
        public string MaskedData { get; set; }

        [StringLength(128)]
        public string MaskedData2 { get; set; }

        [StringLength(128)]
        public string MaskedData3 { get; set; }

        [StringLength(128)]
        public string RemoteTransId { get; set; }

        [Required]
        public decimal Amount { get; set; }

        public decimal DelayAmount { get; set; } = 0m;

        public decimal Fee { get; set; } = 0m;

        [Required]
        [StringLength(64)]
        public string UserIp { get; set; }

        [StringLength(64)]
        public string Error { get; set; }

        [StringLength(256)]
        public string ErrDesc { get; set; }

        [StringLength(128)]
        public string RemoteOrder { get; set; }
    }
}
