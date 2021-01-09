using Hayalpc.Library.Common.Enums;
using Hayalpc.Library.Repository;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hayalpc.Fatura.Data.Models
{
    [Table("carrier_collection_items", Schema = "accounting")]
    public class CarrierCollectionItem : HpModel
    {
        [Required]
        [Column("carrier_collection_id")]
        public long CarrierCollectionId { get; set; }
        [Updatable]
        [Column("merchant_payment_id")]
        public long MerchantPaymentId { get; set; } = 0;

        [Required]
        [Column("transaction_id")]
        public long TransactionId { get; set; }
        [Required]
        [Column("merchant_id")]
        public long MerchantId { get; set; }
        [Required]
        [Column("service_id")]
        public long ServiceId { get; set; }
        [Required]
        public CarrierCollectionItemType Type { get; set; }
        [Required]
        public decimal Amount { get; set; } = 0.00m;
        [Required]
        [Updatable]
        public decimal OperatorAmount { get; set; } = 0.00m; //commisionamount
        [Required]
        [Updatable]
        public decimal AggregatorAmount { get; set; } = 0.00m; //0
        [Required]
        [Updatable]
        public decimal MerchantAmount { get; set; } = 0.00m; // transcationamoun-commisionamount-aggamount
        [Updatable]
        public CarrierCollectionStatus Status { get; set; }
        [Updatable]
        public DateTime? PaymentDate { get; set; }
        [Updatable]
        public DateTime? ChargeDate { get; set; }
        [Updatable]
        public DateTime? ReportDate { get; set; }

    }
}
