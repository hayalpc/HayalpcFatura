using Hayalpc.Library.Common.Dtos;
using Hayalpc.Library.Common.Enums;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hayalpc.Fatura.Panel.External.Models
{
    public class ServiceVM : BaseVM
    {
        [Required(ErrorMessage = "RequiredField")]
        [StringLength(64)]
        [DisplayName("ServiceName")]
        public string Name { get; set; }
        [Required(ErrorMessage = "RequiredField")]
        [DisplayName("Merchant")]
        public long MerchantId { get; set; }
        [Required(ErrorMessage = "RequiredField")]
        [DisplayName("VM.CategoryId")]
        public long CategoryId { get; set; }
        [Required(ErrorMessage = "RequiredField")]
        [DisplayName("ServiceType")]
        public ServiceType Type { get; set; }
        [DisplayName("VM.SubType")]
        public SubscriptionType SubType { get; set; } = 0;
        [Required(ErrorMessage = "RequiredField")]
        [StringLength(64)]
        [DisplayName("PlatformType")]
        public string PlatformType  { get; set; }
        [Required(ErrorMessage = "RequiredField")]
        [StringLength(64)]
        public string PrivateKey { get; set; }
        [Column(TypeName = "decimal(4,2)")]
        [DisplayName("PanelFee")]
        public decimal PanelFee { get; set; } = 0.00m;
        [StringLength(64)]
        [DisplayName("CredentialUrl")]
        public string CredentialUrl { get; set; }
        [DisplayName("HasCredential")]
        public bool HasCredential { get; set; }
        [StringLength(64)]
        [DisplayName("NotificationUrl")]
        public string NotificationUrl { get; set; }
        [DisplayName("HasNotification")]
        public bool HasNotification { get; set; }
        [StringLength(16)]
        public string SmsKeyword { get; set; }
        [Required(ErrorMessage = "RequiredField")]
        [StringLength(64)]
        public string WebSite { get; set; }
        [Required(ErrorMessage = "RequiredField")]
        [StringLength(512)]
        [DisplayName("BusinessDesc")]
        public string BusinessDesc { get; set; }

        [DisplayName("TurkcellActive")]
        public bool TurkcellActive { get; set; } = false;
        [DisplayName("VodafoneActive")]
        public bool VodafoneActive { get; set; } = false;
        [DisplayName("TurkTelekomActive")]
        public bool TurkTelekomActive { get; set; } = false;

        [Required(ErrorMessage = "RequiredField")]
        [DisplayName("TariffId")]
        public long TariffId { get; set; }
        public long ReferralId { get; set; } = 0;

        [Required(ErrorMessage = "RequiredField")]
        [DisplayName("Status")]
        public Status Status { get; set; }

        [DisplayName("IsRefundable")]
        public bool IsRefundable { get; set; } = false;
        [DisplayName("RefundableTime")]
        public int? RefundableTime { get; set; }
        public long? LimitProfileId { get; set; }

        //public Merchant Merchant { get; set; }
        //[JsonIgnore]
        //public virtual ServiceLimitProfile ServiceLimitProfile { get; set; }
        //[JsonIgnore]
        //public virtual List<ServiceParameter> ServiceParameters { get; set; }
        public virtual List<BlobFileDto> BlobFiles { get; set; }

    }
}
