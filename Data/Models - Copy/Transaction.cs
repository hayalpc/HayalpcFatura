using Hayalpc.Library.Common.Enums;
using Hayalpc.Library.Repository;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hayalpc.Fatura.Data.Models
{
    [Table("transactions", Schema = "txn")]
    public class Transaction: HpModel
    {
        [Required]
        [Column("tx_id")]
        [StringLength(64)]
        public Guid TxId { get; set; }

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

        [Column(TypeName = "decimal(8,2)")]
        [Required]
        public decimal Amount { get; set; }

        [Required]
        [Updatable]
        public TransactionStatus Status { get; set; }
        [Updatable]
        public DateTime? ChargeDate { get; set; }
       
        [Required]
        [StringLength(64)]
        [Column("item")]
        public string Item { get; set; }
       
        [Updatable]
        public DateTime? RefundDate { get; set; }
       
        [Updatable]
        [StringLength(256)]
        public string RefundReason { get; set; }

        [Updatable]
        [StringLength(128)]
        [Column("operator_trans_id")]
        public string OperatorTransId { get; set; }

        [Required]
        [StringLength(128)]
        public string MerchantOrder { get; set; }

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
        [Column("error_id")]
        public long? ErrorId { get; set; } = 0;

        [Column("sub_id")]
        public long? SubId { get; set; } = 0;

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
