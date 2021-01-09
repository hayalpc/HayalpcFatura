using Hayalpc.Library.Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Hayalpc.Fatura.Panel.External.Models
{
    public class TableDefinitionVM : BaseVM
    {
        [Required]
        public ActionType ActionType { get; set; }

        [StringLength(256)]
        public string Assembly { get; set; }

        [StringLength(256)]
        public string Namespace { get; set; }
        [Required]
        [StringLength(256)]
        public string ModelName { get; set; }

        public long? RoleId1 { get; set; } = 0;
        public long? RoleId2 { get; set; } = 0;

        [StringLength(128)]
        public string Description { get; set; }

        [Required(ErrorMessage = "RequiredField")]
        [DisplayName("TableDefinitionStatus")]
        public Status Status { get; set; }

    }
}
