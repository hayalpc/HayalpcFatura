using Hayalpc.Fatura.Common.Enums;
using Hayalpc.Library.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Hayalpc.Fatura.Data.Models
{
    [Table("roles", Schema = "panel")]
    public class Role : HpModel
    {
        [Required]
        [Updatable]
        public UserType Type { get; set; }
        
        [Required]
        [StringLength(64)]
        [Updatable]
        public string Name { get; set; }

        [Required]
        [StringLength(64)]
        [Updatable]
        public string Description { get; set; }
        
        [Updatable]
        public Library.Common.Enums.Status Status { get; set; }
    }
}
