using Hayalpc.Library.Common.Enums;
using Hayalpc.Library.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Hayalpc.Fatura.Data.Models
{
    [Table("subscriptions", Schema = "sub")]
    public class Subscription : HpModel
    {
        [Required]
        [Column("merchant_id")]
        public long MerchantId { get; set; }

        [Required]
        [Column("service_id")]
        public long ServiceId { get; set; }

        [Required]
        [Column("carrier_id")]
        public long CarrierId { get; set; }

        [Required]
        [StringLength(16)]
        public string Msisdn { get; set; }


        [Required]
        [Column(TypeName = "decimal(8,2)")]
        public decimal HireAmount { get; set; }

        [Required]
        [Updatable]
        public SubscriptionStatus Status { get; set; }

        [Required]
        public SubscriptionType SubsType { get; set; }

        [Updatable]
        public DateTime? SubsDate { get; set; }

        [Updatable]
        public DateTime? CancelDate { get; set; }

        [Updatable]
        public string CancelReason { get; set; }

        [Updatable]
        public DateTime? RenewalDate { get; set; }

        [Required]
        public string MerchantOrder { get; set; }

        [Updatable]
        [Column("carrier_sub_id")]
        public string CarrierSubId { get; set; }

        [Updatable]
        public int RenewalCount { get; set; } = 99;

        [Required]
        public string Channel { get; set; }

        [Required]
        [Column("item")]
        public string Item { get; set; }

    }
}
