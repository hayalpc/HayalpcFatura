using Hayalpc.Fatura.Panel.External.Models;
using Hayalpc.Library.Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Hayalpc.Fatura.Panel.External.Models
{
    public class MerchantPaymentVM : BaseVM
    {
        [DisplayName("VM.MerchantId")]
        public long MerchantId { get; set; }
        [DisplayName("VM.TotalAmount")]
        [DataType(DataType.Currency)]
        public decimal TotalAmount { get; set; } = 0.00m;
        [DisplayName("VM.OperatorAmount")]
        [DataType(DataType.Currency)]
        public decimal OperatorAmount { get; set; } = 0.00m;
        [DisplayName("VM.AggregatorAmount")]
        [DataType(DataType.Currency)]
        public decimal AggregatorAmount { get; set; } = 0.00m;
        [DisplayName("VM.RefundAmount")]
        [DataType(DataType.Currency)]
        public decimal RefundAmount { get; set; } = 0.00m;
        [DisplayName("VM.ExtraFee")]
        [DataType(DataType.Currency)]
        public decimal ExtraFee { get; set; } = 0.00m;
        [DisplayName("VM.ShareAmount")]
        [DataType(DataType.Currency)]
        public decimal ShareAmount { get; set; } = 0.00m;
        [DisplayName("VM.MerchantPaymentStatus")]
        public MerchantPaymentStatus Status { get; set; }

    }
}
