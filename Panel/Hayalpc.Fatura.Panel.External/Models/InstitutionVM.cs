using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Hayalpc.Fatura.Panel.External.Models
{
    public class InstitutionVM : BaseVM
    {
        [Required]
        [StringLength(64)]
        [Display(Name = "VM.InstitutionCode")]
        public string Code { get; set; }

        [Required]
        [StringLength(64)]
        [Display(Name = "VM.Name")]
        public string Name { get; set; }

        [Required]
        [StringLength(256)]
        [Display(Name = "VM.Description")]
        public string Description { get; set; }

        [Display(Name = "VM.CategoryId")]
        public long CategoryId { get; set; }

        [StringLength(256)]
        [Display(Name = "VM.Type")]
        public string Type { get; set; }

        [StringLength(256)]
        [Display(Name = "VM.Placeholder")]
        public string Placeholder { get; set; }

        [StringLength(256)]
        [Display(Name = "VM.ValidationText")]
        public string ValidationText { get; set; }

        [Display(Name = "VM.Reverse")]
        public bool Reverse { get; set; }

        [StringLength(256)]
        [Display(Name = "VM.Logo")]
        public string Logo { get; set; }

    }
}
