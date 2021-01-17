using Hayalpc.Library.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Hayalpc.Fatura.Data.Models
{
    [Table("payments")]
    public class InvoicePayment : HpModel
    {
        [Required]
        [StringLength(64)]
        public Guid Token { get; set; }

        [Required]
        [Column("dealer_id")]
        public long DealerId { get; set; }

        [Required]
        [StringLength(128)]
        public long DealerName { get; set; }

        [Required]
        [Column("category_id")]
        public long CategoryId { get; set; }

        [Required]
        [StringLength(128)]
        public long CategoryName { get; set; }

        [Required]
        [Column("institution_id")]
        public long InstitutionId { get; set; }

        [Required]
        [StringLength(128)]
        [Column("institution_name")]
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

        [Updatable]
        [StringLength(128)]
        [Column("institution_trans_id")]
        public string RemoteTransId { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Updatable]
        public decimal DelayAmount { get; set; } = 0m;

        [Updatable]
        public decimal Fee { get; set; } = 0m;

        [Required]
        [StringLength(64)]
        [Column("user_ip")]
        public string UserIp { get; set; }

        [StringLength(64)]
        [Updatable]
        public string Error { get; set; }

        [StringLength(256)]
        [Updatable]
        public string ErrDesc { get; set; }

        [StringLength(128)]
        public string RemoteOrder { get; set; }
    }
}
