using Hayalpc.Library.Common.Enums;
using Hayalpc.Library.Repository;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hayalpc.Fatura.Data.Models
{
    [Table("smses", Schema = "sms")]
    public class Sms : HpModel
    {
        [Required]
        [StringLength(16)]
        public string Title { get; set; }
        [Required]
        [StringLength(16)]
        public string ShortCode { get; set; }

        [Column("merchant_id")]
        public long? MerchantId { get; set; } = 0;

        [Column("service_id")]
        public long? ServiceId { get; set; } = 0;

        [Column("carrier_id")]
        public long CarrierId { get; set; }

        [Required]
        [StringLength(16)]
        public string Msisdn { get; set; }

        [Required]
        [StringLength(256)]
        public string Message { get; set; }

        [Required]
        [StringLength(8)]
        public string Type { get; set; }

        [StringLength(128)]
        [Updatable]
        [Column("operator_sms_id")]
        public string OperatorSMSId { get; set; }

        [Updatable]
        public int? Trycount { get; set; } = 0;

        [Required]
        [Updatable]
        public Status Status { get; set; }

        [Updatable]
        public DateTime? SendDate { get; set; }

        [Updatable]
        public DateTime? ExpireDate { get; set; }

    }
}
