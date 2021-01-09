using Hayalpc.Library.Common.Enums;
using Hayalpc.Library.Repository;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hayalpc.Fatura.Data.Models
{
    [Table("tariffs", Schema = "service")]

    public class Tariff : HpModel
    {
        [Column("merchant_id")]
        public long MerchantId { get; set; } = 0;

        [Required]
        [StringLength(64)]
        [Updatable]
        public string Name { get; set; }

        [Required]
        [StringLength(256)]
        [Updatable]
        public string Description { get; set; }

        [Range(0, 50)]
        [Column(TypeName = "decimal(4,2)")]
        [Updatable]
        public decimal? TurkcellCommission { get; set; } = 00.00m;

        [Range(0, 50)]
        [Column(TypeName = "decimal(4,2)")]
        [Updatable]
        public decimal? VodafoneCommission { get; set; } = 0.00m;

        [Range(0, 50)]
        [Column(TypeName = "decimal(4,2)")]
        [Updatable]
        public decimal? TurkTelekomCommission { get; set; } = 0.00m;


        [Range(0, 50)]
        [Column(TypeName = "decimal(4,2)")]
        [Updatable]
        public decimal? TurkcellAggCommission { get; set; } = 0.00m;

        [Range(0, 50)]
        [Column(TypeName = "decimal(4,2)")]
        [Updatable]
        public decimal? VodafoneAggCommission { get; set; } = 0.00m;

        [Range(0, 50)]
        [Column(TypeName = "decimal(4,2)")]
        [Updatable]
        public decimal? TurkTelekomAggCommission { get; set; } = 0.00m;

        [Updatable]
        public bool ReferralActive { get; set; } = false;

        [Range(0, 50)]
        [Column(TypeName = "decimal(4,2)")]
        [Updatable]
        public decimal? TurkcellRefCommission { get; set; } = 0.00m;

        [Range(0, 50)]
        [Column(TypeName = "decimal(4,2)")]
        [Updatable]
        public decimal? VodafoneRefCommission { get; set; } = 0.00m;

        [Range(0, 50)]
        [Column(TypeName = "decimal(4,2)")]
        [Updatable]
        public decimal? TurkTelekomRefCommission { get; set; } = 0.00m;

        [Required]
        [Updatable]
        public Status Status { get; set; }

    }
}
