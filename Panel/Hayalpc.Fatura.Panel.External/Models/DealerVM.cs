using Hayalpc.Fatura.Common.Dtos;
using Hayalpc.Fatura.Panel.External.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Hayalpc.Fatura.Panel.External.Models
{
    public class DealerVM : BaseVM
    {
        [Required]
        [Display(Name = "VM.DealerCode")]
        public long Code { get; set; }

        [StringLength(256)]
        [Display(Name = "VM.Logo")]
        public string Logo { get; set; }

        [Required]
        [StringLength(64)]
        [Display(Name = "VM.DealerName")]
        public string Name { get; set; }

        [Required]
        [StringLength(256)]
        [Display(Name = "VM.Description")]
        public string Description { get; set; }

        [Display(Name = "VM.Priorty")]
        public int Priorty { get; set; } = 100;

        [Display(Name = "VM.Default")]
        public bool Default { get; set; } = false;

        [StringLength(64)]
        [Display(Name = "VM.Channel")]
        public string Channel { get; set; }

        [StringLength(64)]
        [Display(Name = "VM.PersonName")]
        public string PersonName { get; set; }

        [StringLength(64)]
        [Display(Name = "VM.PersonSurname")]
        public string PersonSurname { get; set; }

        [StringLength(64)]
        [Display(Name = "VM.PersonPhone")]
        public string PersonPhone { get; set; }

        [StringLength(64)]
        [Display(Name = "VM.PersonEmail")]
        public string PersonEmail { get; set; }

        [StringLength(64)]
        [Display(Name = "VM.PersonAddress")]
        public string PersonAddress { get; set; }

        [StringLength(64)]
        [Display(Name = "VM.PersonTckNo")]
        public string PersonTckNo { get; set; }

        [StringLength(64)]
        [Display(Name = "VM.PersonIban")]
        public string PersonIban { get; set; }

        [StringLength(64)]
        [Display(Name = "VM.PersonAccountName")]
        public string PersonAccountName { get; set; }

        [NotMapped]
        public virtual List<BlobFileDto> BlobFiles { get; set; }
    }
}
