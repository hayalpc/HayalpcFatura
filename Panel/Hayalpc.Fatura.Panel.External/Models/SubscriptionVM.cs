
using Hayalpc.Library.Common.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace Hayalpc.Fatura.Panel.External.Models
{
    public class SubscriptionVM : BaseVM
    {
        [Display(Name = "VM.MerchantId")]
        public long MerchantId { get; set; }
        [Display(Name = "VM.ServiceId")]
        public long ServiceId { get; set; }
        [Display(Name = "VM.CarrierId")]
        public long CarrierId { get; set; }
        [Display(Name = "VM.Msisdn")]
        public string Msisdn { get; set; }
        [Display(Name = "VM.HireAmount")]
        public decimal HireAmount { get; set; }
        [Display(Name = "VM.SubscriptionStatus")]
        public SubscriptionStatus Status { get; set; }
        [Display(Name = "VM.SubsType")]
        public SubscriptionType SubsType { get; set; }
        [Display(Name = "VM.SubsDate")]
        public DateTime? SubsDate { get; set; }
        [Display(Name = "VM.CancelDate")]
        public DateTime? CancelDate { get; set; }
        [Display(Name = "VM.CancelReason")]
        public string CancelReason { get; set; }
        [Display(Name = "VM.RenewalDate")]
        public DateTime? RenewalDate { get; set; }
        [Display(Name = "VM.MerchantOrder")]
        public string MerchantOrder { get; set; }
        [Display(Name = "VM.CarrierSubId")]
        public string CarrierSubId { get; set; }
        [Display(Name = "VM.RenewalCount")]
        public int RenewalCount { get; set; } = 99;
        [Display(Name = "VM.Channel")]
        public string Channel { get; set; }
        [Display(Name = "VM.Item")]
        public string Item { get; set; }

        public bool CanCancel
        {
            get
            {
                return Status == SubscriptionStatus.ACTIVE || Status == SubscriptionStatus.RETRY;
            }
        }
    }
}
