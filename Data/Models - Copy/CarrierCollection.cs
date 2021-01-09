using Hayalpc.Library.Common.Enums;
using Hayalpc.Library.Repository;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hayalpc.Fatura.Data.Models
{
    [Table("carrier_collections", Schema = "accounting")]
    public class CarrierCollection : HpModel
    {
        [Required]
        [Updatable]
        public string Name { get; set; }

        [Required]
        [Updatable]
        [StringLength(256)]
        public string FileName { get; set; }

        [StringLength(256)]
        [Updatable]
        public string Note { get; set; }

        [Required]
        [Column("carrier_id")]
        public long CarrierId { get; set; }

        [Required]
        [Updatable]
        [Column("blob_id")]
        public long BlobId { get; set; } = 0;

        [Required]
        [Updatable]
        public CarrierCollectionStatus Status { get; set; }
        
        [Updatable]
        public decimal TotalAmount { get; set; } = 0m;
        
        [Updatable]
        public decimal OperatorAmount { get; set; } = 0m;
        
        [Updatable]
        public decimal AggAmount { get; set; } = 0m;
        
        [Updatable]
        public decimal ShareAmount { get; set; } = 0m;
        
        [Updatable]
        public decimal SendedAmount { get; set; } = 0m;
        
        [Updatable]
        public decimal ResiduaryAmount { get; set; } = 0m;

        [Updatable]
        public decimal RefundAmount { get; set; } = 0m;

        [Updatable]
        public DateTime ReportDate { get; set; }
        [Updatable]
        public DateTime? ApproveDate { get; set; }
        [Updatable]
        public DateTime? ClosedDate { get; set; }


    }
}
