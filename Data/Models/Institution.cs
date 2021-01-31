using Hayalpc.Library.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Hayalpc.Fatura.Data.Models
{
    [Table("institutions")]
    public class Institution : HpModel
    {
        [Required]
        [StringLength(64)]
        [Updatable]
        public string Code { get; set; }

        [Required]
        [StringLength(64)]
        [Updatable]
        public string Name { get; set; }

        [Required]
        [StringLength(256)]
        [Updatable]
        public string Description { get; set; }

        [Required]
        [Updatable]
        [Column("category_id")]
        public long CategoryId { get; set; }

        [StringLength(256)]
        [Updatable]
        public string Type { get; set; }

        [StringLength(256)]
        [Updatable]
        public string Placeholder { get; set; }

        [StringLength(256)]
        [Updatable]
        public string ValidationText { get; set; }

        [Updatable]
        public bool Reverse { get; set; }

        [StringLength(256)]
        [Updatable]
        public string Logo { get; set; }

    }
}
