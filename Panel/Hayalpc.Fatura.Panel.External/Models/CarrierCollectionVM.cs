using Hayalpc.Fatura.Panel.External.Models;
using Hayalpc.Library.Common.Enums;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hayalpc.Fatura.Panel.External.Models
{
    public class CarrierCollectionVM : BaseVM
    {
        [Required(ErrorMessage = "RequiredField")]
        [DisplayName("VM.CollectionName")]
        public string Name { get; set; }

        [StringLength(256)]
        [DisplayName("VM.FileName")]
        public string FileName { get; set; }

        [StringLength(256)]
        [DisplayName("VM.Note")]
        public string Note { get; set; }

        [Required(ErrorMessage = "RequiredField")]
        [DisplayName("VM.CarrierId")]
        public long CarrierId { get; set; }

        [Required(ErrorMessage = "RequiredField")]
        [DisplayName("VM.BlobId")]
        public long BlobId { get; set; }

        [Required(ErrorMessage = "RequiredField")]
        [DisplayName("VM.CollectionStatus")]
        public CarrierCollectionStatus Status { get; set; }
        
        [DataType(DataType.Currency)]
        [DisplayName("VM.TotalAmount")]
        public decimal TotalAmount { get; set; } = 0.00m;
        
        [DataType(DataType.Currency)]
        [DisplayName("VM.OperatorAmount")]
        public decimal OperatorAmount { get; set; } = 0.00m;
        
        [DataType(DataType.Currency)]
        [DisplayName("VM.AggAmount")]
        public decimal AggAmount { get; set; } = 0.00m;
        
        [DataType(DataType.Currency)]
        [DisplayName("VM.ShareAmount")]
        public decimal ShareAmount { get; set; } = 0.00m;
        
        [DataType(DataType.Currency)]
        [DisplayName("VM.SendedAmount")]
        public decimal SendedAmount { get; set; } = 0.00m;
       
        [DataType(DataType.Currency)]
        [DisplayName("VM.ResiduaryAmount")]
        public decimal ResiduaryAmount { get; set; } = 0.00m;

        [DataType(DataType.Currency)]
        [DisplayName("VM.RefundAmount")]
        public decimal RefundAmount { get; set; } = 0.00m;

        [Required(ErrorMessage = "RequiredField")]
        [DisplayName("VM.ReportDate")]
        public DateTime ReportDate { get; set; }

        [DisplayName("VM.ApproveDate")]
        public DateTime? ApproveDate { get; set; }

        [DisplayName("VM.ClosedDate")]
        public DateTime? ClosedDate { get; set; }
    }
}
