using Hayalpc.Library.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Hayalpc.Fatura.Data.Models
{
    [Table("categories", Schema = "parameter")]
    public class Category : HpModel
    {
        [Required]
        [StringLength(64)]
        [Updatable]
        public string Slug { get; set; }

        [Required]
        [StringLength(64)]
        [Updatable]
        public string Name { get; set; }

        [Required]
        [StringLength(256)]
        [Updatable]
        public string Description { get; set; }
    }
}
