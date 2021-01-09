using Hayalpc.Library.Common.Enums;
using Hayalpc.Library.Repository;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hayalpc.Fatura.Data.Models
{
    [Table("services", Schema ="service")]
    public class Service : HpModel
    {
        [Required]
        [StringLength(64)]
        [Updatable]
        public string Name { get; set; }
        
        [Required]
        [Column("merchant_id")]
        public long MerchantId { get; set; }

        [Required]
        [Column("category_id")]
        [Updatable]
        public long CategoryId { get; set; }
        
        [Required]
        public ServiceType Type { get; set; }

        [Updatable]
        public SubscriptionType SubType { get; set; } = 0;
        
        [Required]
        [StringLength(64)]
        [Updatable]
        public string PlatformType  { get; set; }
        
        [Required]
        [StringLength(64)]
        [Updatable]
        public string PrivateKey { get; set; }

        [Required]
        [Updatable]
        [Column(TypeName = "decimal(4,2)")]
        public decimal PanelFee { get; set; } = 0.00m;

        [StringLength(64)]
        [Updatable]
        public string CredentialUrl { get; set; }
        
        [StringLength(64)]
        [Updatable]
        public string NotificationUrl { get; set; }
        
        [StringLength(16)]
        [Updatable]
        public string SmsKeyword { get; set; }
        
        [Required]
        [StringLength(64)]
        [Updatable]
        public string WebSite { get; set; }

        [Required]
        [StringLength(512)]
        [Updatable]
        public string BusinessDesc { get; set; }

        [Updatable]
        public bool TurkcellActive { get; set; } = false;
        [Updatable]
        public bool VodafoneActive { get; set; } = false;
        [Updatable]
        public bool TurkTelekomActive { get; set; } = false;

        [Required]
        [Column("tariff_id")]
        [Updatable]
        public long TariffId { get; set; }

        [Column("referral_id")]
        [Updatable]
        public long ReferralId { get; set; } = 0;

        [Required]
        [Updatable]
        public Status Status { get; set; }

        [Updatable]
        [Column("is_refundable")]
        public bool IsRefundable { get; set; } = false;
       
        [Updatable]
        public int? RefundableTime { get; set; }
        
        [Updatable]
        [Column("limit_profile_id")]
        public long? LimitProfileId { get; set; }

        [NotMapped]
        public virtual List<BlobFile> BlobFiles { get; set; }
    }
}
