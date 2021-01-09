using Hayalpc.Library.Common.Enums;
using Hayalpc.Library.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Hayalpc.Fatura.Data.Models
{
    [Table("table_definitions", Schema = "tracking")]
    public class TableDefinition : HpModel
    {
        [Required]
        public ActionType ActionType { get; set; }

        [Required]
        [Updatable]
        [StringLength(256)]
        public string Assembly { get; set; }

        [Required]
        [Updatable]
        [StringLength(256)]
        public string Namespace { get; set; }

        [Required]
        [StringLength(256)]
        public string ModelName { get; set; }

        [Updatable]
        [Column("role_id1")]
        public long? RoleId1 { get; set; } = 0;
        
        [Updatable]
        [Column("role_id2")]
        public long? RoleId2 { get; set; } = 0;

        [StringLength(128)]
        [Updatable]
        public string Description { get; set; }

        [Required]
        [Updatable]
        public Status Status{ get; set; }

    }
}
