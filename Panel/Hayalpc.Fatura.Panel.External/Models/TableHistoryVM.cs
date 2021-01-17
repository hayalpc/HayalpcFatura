using Hayalpc.Fatura.Common.Enums;
using Hayalpc.Library.Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Hayalpc.Fatura.Panel.External.Models
{
    public class TableHistoryVM : BaseVM
    {
        [Display(Name = "VM.DealerId")]
        public long? DealerId { get; set; } = 0;

        [Required(ErrorMessage = "RequiredField")]
        [Display(Name = "VM.TableDefinitionId")]
        public long TableDefinitionId { get; set; }

        [Display(Name = "VM.ActionType")]
        public ActionType ActionType { get; set; }

        [Required(ErrorMessage = "RequiredField")]
        [StringLength(64)]
        [Display(Name = "VM.ActionDetail")]
        public string ActionDetail { get; set; }

        [StringLength(256)]
        [Display(Name = "VM.Note")]
        public string Note { get; set; }

        [Required(ErrorMessage = "RequiredField")]
        [StringLength(64)]
        [Display(Name = "VM.ModelName")]
        public string ModelName { get; set; }

        [Display(Name = "VM.DataId")]
        public long? DataId { get; set; } = 0;

        public string OldData { get; set; }
        public string NewData { get; set; }

        [Display(Name = "VM.RoleId1")]
        public long? RoleId1 { get; set; } = 0;
        [Display(Name = "VM.RoleId2")]
        public long? RoleId2 { get; set; } = 0;
        [Display(Name = "VM.TableHistoryStatus")]
        public TableHistoryStatus Status { get; set; }

    }
}
