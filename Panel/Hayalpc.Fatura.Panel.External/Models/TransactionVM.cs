using Hayalpc.Library.Common.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hayalpc.Fatura.Panel.External.Models
{
    public class TransactionVM : BaseVM
    {
        [Display(Name = "VM.TxId")]
        public Guid TxId { get; set; }

        [Display(Name = "VM.MerchantId")]
        public long MerchantId { get; set; }

        [Display(Name = "VM.ServiceId")]
        public long ServiceId { get; set; }

        [Display(Name = "VM.CarrierId")]
        public long CarrierId { get; set; }

        [Display(Name = "Msisdn")]
        public string Msisdn { get; set; }

        [Display(Name = "Amount")]
        [DataType(DataType.Currency)]
        public decimal Amount { get; set; }

        [Display(Name = "TransactionStatus")]
        public TransactionStatus Status { get; set; }

        [Display(Name = "VM.ChargeDate")]
        public DateTime? ChargeDate { get; set; } = null;

        [Display(Name = "VM.Item")]
        public string Item { get; set; }

        [Display(Name = "VM.RefundDate")]
        public DateTime? RefundDate { get; set; } = null;

        [Display(Name = "VM.RefundReason")]
        public string RefundReason { get; set; }

        [Display(Name = "VM.OperatorTransId")]
        public string OperatorTransId { get; set; }

        [Display(Name = "VM.MerchantOrder")]
        public string MerchantOrder { get; set; }

        [Display(Name = "VM.UserIp")]
        public string UserIp { get; set; }

        [Display(Name = "VM.Channel")]
        public string Channel { get; set; }

        [Display(Name = "VM.Error")]
        public string Error { get; set; }

        [Display(Name = "VM.ErrDesc")]
        public string ErrDesc { get; set; }

        [Display(Name = "VM.ErrorId")]
        public long? ErrorId { get; set; } = 0;

        [Display(Name = "VM.SubId")]
        public long? SubId { get; set; } = 0;

        [Display(Name = "VM.Value1")]
        public string Value1 { get; set; }
        [Display(Name = "VM.Value2")]
        public string Value2 { get; set; }
        [Display(Name = "VM.Value3")]
        public string Value3 { get; set; }

        public bool CanRefund
        {
            get
            {
                return Status == TransactionStatus.CHARGED;
            }
        }
        //public Merchant Merchant { get; set; }
        //public Service Service { get; set; }
    }
}
