using Hayalpc.Fatura.Common.Enums;
using Hayalpc.Library.Common.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hayalpc.Fatura.Panel.External.Models
{
    public class InvoiceVM : BaseVM
    {
        [Required]
        [StringLength(64)]
        public Guid Token { get; set; }

        [Required]
        public long DealerId { get; set; }

        [Required]
        public long PaymentId { get; set; }

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
        public string InvoiceNo { get; set; }

        [Required]
        [StringLength(64)]
        public string InvoiceDate { get; set; }

        [Required]
        [StringLength(64)]
        public string InvoiceOwner { get; set; }

        [Required]
        public decimal Amount { get; set; }

        public decimal DelayAmount { get; set; } = 0m;
        
        public decimal Fee { get; set; } = 0m;

        public decimal TotalAmount { get; set; }

        [Required]
        public InvoiceStatus Status { get; set; }

        [StringLength(128)]
        public string InstitutionTransId { get; set; }

        [Required]
        [StringLength(64)]
        public string UserIp { get; set; }

        [Required]
        [StringLength(16)]
        public string Channel { get; set; }

        [StringLength(64)]
        public string Error { get; set; }

        [StringLength(256)]
        public string ErrDesc { get; set; }

        [StringLength(128)]
        public string Value1 { get; set; }

        [StringLength(128)]
        public string Value2 { get; set; }

        [StringLength(128)]
        public string Value3 { get; set; }

    }
}
