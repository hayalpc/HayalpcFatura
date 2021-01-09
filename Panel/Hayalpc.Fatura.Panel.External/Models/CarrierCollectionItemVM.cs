using Hayalpc.Library.Common.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hayalpc.Fatura.Panel.External.Models
{
    public class CarrierCollectionItemVM : BaseVM
    {
        [Required]
        public long CarrierCollectionId { get; set; }
        public long MerchantPaymentId { get; set; } = 0;

        [Required]
        public long TransactionId { get; set; }
        [Required]
        public long MerchantId { get; set; }
        [Required]
        public long ServiceId { get; set; }
        [Required]
        public CarrierCollectionItemType Type { get; set; }
        [Required]
        [DataType(DataType.Currency)]
        public decimal Amount { get; set; } = 0.00m;
        [Required]
        [DataType(DataType.Currency)]
        public decimal OperatorAmount { get; set; } = 0.00m; //commisionamount
        [Required]
        [DataType(DataType.Currency)]
        public decimal AggregatorAmount { get; set; } = 0.00m; //0
        [Required]
        [DataType(DataType.Currency)]
        public decimal MerchantAmount { get; set; } = 0.00m; // transcationamoun-commisionamount-aggamount
        public ModelStatus Status { get; set; }
        public DateTime? PaymentDate { get; set; }
        public DateTime? ChargeDate { get; set; } //transactiondate
        public DateTime? ReportDate { get; set; } //invoicedate

    }
}
