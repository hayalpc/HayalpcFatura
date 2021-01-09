using Hayalpc.Library.Common.Enums;
using Hayalpc.Library.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Hayalpc.Fatura.Data.Models
{
    [Table("table_histories", Schema = "tracking")]
    public class TableHistory : HpModel
    {
        [Column("merchant_id")]
        public long? MerchantId { get; set; } = 0;

        [Required]
        [Column("table_definition_id")]
        public long TableDefinitionId { get; set; }

        public ActionType ActionType { get; set; }

        [Required]
        [StringLength(64)]
        public string ActionDetail { get; set; }

        [StringLength(256)]
        public string Note { get; set; }

        [Required]
        [StringLength(64)]
        public string ModelName { get; set; }

        [Column("data_id")]
        public long? DataId { get; set; } = 0;

        public string OldData { get; set; }
        public string NewData { get; set; }

        [Column("role_id1")]
        public long? RoleId1 { get; set; } = 0;
        [Column("role_id2")]
        public long? RoleId2 { get; set; } = 0;
        [Required]
        [Updatable]
        public TableHistoryStatus Status { get; set; }

    }
}
