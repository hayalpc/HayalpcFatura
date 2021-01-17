using Hayalpc.Library.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Hayalpc.Fatura.Data.Models
{
    [Table("dealers")]
    public class Dealer : HpModel
    {
        [Required]
        public long Code { get; set; }

        [StringLength(256)]
        [Updatable]
        public string Logo { get; set; }

        [Required]
        [StringLength(64)]
        [Updatable]
        public string Name { get; set; }

        [Required]
        [StringLength(256)]
        [Updatable]
        public string Description { get; set; }

        [Updatable]
        public int Priorty { get; set; } = 100;

        [Updatable]
        public bool Default { get; set; } = false;

        [Updatable]
        [StringLength(64)]
        public string Channel { get; set; }

        [Updatable]
        [StringLength(64)]
        public string PersonName { get; set; }

        [Updatable]
        [StringLength(64)]
        public string PersonSurname { get; set; }

        [Updatable]
        [StringLength(64)]
        public string PersonPhone { get; set; }

        [Updatable]
        [StringLength(64)]
        public string PersonEmail { get; set; }

        [Updatable]
        [StringLength(64)]
        public string PersonAddress { get; set; }

        [Updatable]
        [StringLength(64)]
        public string PersonTckNo { get; set; }

        [Updatable]
        [StringLength(64)]
        public string PersonIban { get; set; }

        [Updatable]
        [StringLength(64)]
        public string PersonAccountName { get; set; }

    }
}
