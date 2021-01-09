using Hayalpc.Library.Common.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace Hayalpc.Fatura.Panel.External.Models
{
    public class SmsVM : BaseVM
    {
        [Display(Name = "VM.SmsTitle")]
        public string Title { get; set; }
        [Display(Name = "VM.ShortCode")]
        public string ShortCode { get; set; }
        [Display(Name = "VM.MerchantId")]
        public long? MerchantId { get; set; } = 0;
        [Display(Name = "VM.ServiceId")]
        public long? ServiceId { get; set; } = 0;
        [Display(Name = "VM.CarrierId")]
        public long CarrierId { get; set; }
        [Display(Name = "VM.Msisdn")]
        public string Msisdn { get; set; }
        [Display(Name = "VM.Message")]
        public string Message { get; set; }
        [Display(Name = "VM.SmsType")]
        public string Type { get; set; }
        public string OperatorSMSId { get; set; }
        public int? Trycount { get; set; } = 0;
        [Display(Name = "VM.SmsStatus")]
        public Status Status { get; set; }
        [Display(Name = "VM.SendDate")]
        public DateTime? SendDate { get; set; }
        public DateTime? ExpireDate { get; set; }

        public bool CanResend
        {
            get
            {
                return Type == SmsType.MT && CreateTime >= DateTime.Now.AddDays(-3);
            }
        }
    }
}
