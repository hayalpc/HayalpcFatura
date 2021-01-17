using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Hayalpc.Fatura.Common.Enums
{
    public enum TableHistoryStatus
    {
        [Display(Name = "Enum.New")]
        New = 0,
        [Display(Name = "Enum.Step1")]
        Step1 = 1,
        [Display(Name = "Enum.Step2")]
        Step2 = 2,
        [Display(Name = "Enum.Approved")]
        Approved = 10,
        [Display(Name = "Enum.Rejected")]
        Rejected = -1,
        [Display(Name = "Enum.Log")]
        Log = -10,

    }
}
