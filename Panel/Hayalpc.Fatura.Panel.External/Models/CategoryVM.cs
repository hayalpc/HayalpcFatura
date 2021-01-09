using Hayalpc.Fatura.Panel.External.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Hayalpc.Fatura.Panel.External.Models
{
    public class CategoryVM : BaseVM
    {
        [Required]
        [StringLength(64)]
        [Display(Name = "VM.Slug")]
        public string Slug { get; set; }

        [Required]
        [StringLength(64)]
        [Display(Name = "VM.Name")]
        public string Name { get; set; }

        [Required]
        [StringLength(256)]
        [Display(Name = "VM.Description")]
        public string Description { get; set; }
    }
}
