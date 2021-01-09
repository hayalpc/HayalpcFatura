using Hayalpc.Library.Common.Enums;
using Hayalpc.Library.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Hayalpc.Fatura.Data.Models
{
    [Table("merchant_payments", Schema = "accounting")]
    public class MerchantPayment : HpModel
    {
        [Column("merchant_id")]
        public long MerchantId { get; set; }
        [Updatable]
        public decimal TotalAmount { get; set; } = 0.00m;
        [Updatable]
        public decimal OperatorAmount { get; set; } = 0.00m;
        [Updatable]
        public decimal AggregatorAmount { get; set; } = 0.00m;
        [Updatable]
        public decimal RefundAmount { get; set; } = 0.00m;
        [Updatable]
        public decimal ExtraFee { get; set; } = 0.00m;
        [Updatable]
        public decimal ShareAmount { get; set; } = 0.00m;
        [Updatable]
        public MerchantPaymentStatus Status { get; set; }

    }
}
