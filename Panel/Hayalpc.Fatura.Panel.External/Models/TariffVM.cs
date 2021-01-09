using Hayalpc.Library.Common.Enums;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hayalpc.Fatura.Panel.External.Models
{
    public class TariffVM : BaseVM
    {
        [Display(Name = "VM.MerchantId")]
        public long? MerchantId { get; set; } = 0;

        [Required(ErrorMessage = "RequiredField")]
        [StringLength(64)]
        [Display(Name = "VM.TariffName")]
        public string Name { get; set; }

        [Required(ErrorMessage = "RequiredField")]
        [StringLength(256)]
        [Display(Name = "VM.Description")]
        public string Description { get; set; }

        [Display(Name = "VM.TurkcellCommission")]
        [Range(0, 50)]
        [Column(TypeName = "decimal(4,2)")]
        public decimal? TurkcellCommission { get; set; } = 00.00m;

        [Display(Name = "VM.VodafoneCommission")]
        [Range(0, 50)]
        [Column(TypeName = "decimal(4,2)")]
        public decimal? VodafoneCommission { get; set; } = 0.00m;

        [Display(Name = "VM.TurkTelekomCommission")]
        [Range(0, 50)]
        [Column(TypeName = "decimal(4,2)")]
        public decimal? TurkTelekomCommission { get; set; } = 0.00m;


        [Display(Name = "VM.TurkcellAggCommission")]
        [Range(0, 50)]
        [Column(TypeName = "decimal(4,2)")]
        public decimal? TurkcellAggCommission { get; set; } = 0.00m;

        [Display(Name = "VM.VodafoneAggCommission")]
        [Range(0, 50)]
        [Column(TypeName = "decimal(4,2)")]
        public decimal? VodafoneAggCommission { get; set; } = 0.00m;

        [Display(Name = "VM.TurkTelekomAggCommission")]
        [Range(0, 50)]
        [Column(TypeName = "decimal(4,2)")]
        public decimal? TurkTelekomAggCommission { get; set; } = 0.00m;


        public bool ReferralActive { get; set; } = false;

        [Display(Name = "VM.TurkcellRefCommission")]
        [Range(0, 50)]
        [Column(TypeName = "decimal(4,2)")]
        public decimal? TurkcellRefCommission { get; set; } = 0.00m;

        [Display(Name = "VM.VodafoneRefCommission")]
        [Range(0, 50)]
        [Column(TypeName = "decimal(4,2)")]
        public decimal? VodafoneRefCommission { get; set; } = 0.00m;

        [Display(Name = "VM.TelekomRefCommission")]
        [Range(0, 50)]
        [Column(TypeName = "decimal(4,2)")]
        public decimal? TelekomRefCommission { get; set; } = 0.00m;

        [Required(ErrorMessage = "RequiredField")]
        [DisplayName("VM.TariffStatus")]
        public Status Status { get; set; }
    }
}
