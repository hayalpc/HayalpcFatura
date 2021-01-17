using Hayalpc.Fatura.Common.Enums;
using Hayalpc.Library.Common.Enums;
using Hayalpc.Library.Repository;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hayalpc.Fatura.Data.Models
{
    [Table("invoices")]
    public class Invoice : HpModel
    {
        [Required]
        [StringLength(64)]
        public Guid Token { get; set; }

        [Required]
        [Column("dealer_id")]
        public long DealerId { get; set; }

        [Required]
        [Column("payment_id")]
        public long PaymentId { get; set; }

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
        [Column("invoice_no")]
        public string InvoiceNo { get; set; }

        [Required]
        [StringLength(64)]
        [Column("invoice_date")]
        public string InvoiceDate { get; set; }

        [Required]
        [StringLength(64)]
        [Column("invoice_owner")]
        public string InvoiceOwner { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Updatable]
        public decimal DelayAmount { get; set; } = 0m;
        
        [Updatable]
        public decimal Fee { get; set; } = 0m;

        [Updatable]
        public decimal TotalAmount { get; set; }

        [Required]
        [Updatable]
        public InvoiceStatus Status { get; set; }

        [Updatable]
        [StringLength(128)]
        [Column("institution_trans_id")]
        public string InstitutionTransId { get; set; }

        [Required]
        [StringLength(64)]
        [Column("user_ip")]
        public string UserIp { get; set; }

        [Required]
        [StringLength(16)]
        public string Channel { get; set; }

        [StringLength(64)]
        [Updatable]
        public string Error { get; set; }

        [StringLength(256)]
        [Updatable]
        public string ErrDesc { get; set; }

        [Updatable]
        [StringLength(128)]
        public string Value1 { get; set; }

        [Updatable]
        [StringLength(128)]
        public string Value2 { get; set; }

        [Updatable]
        [StringLength(128)]
        public string Value3 { get; set; }

    }
}
