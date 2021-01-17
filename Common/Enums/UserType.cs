using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Hayalpc.Fatura.Common.Enums
{
    public enum UserType
    {
        [Display(Name = "User")]
        User = 0, 
        
        [Display(Name = "Admin")]
        Admin = 10,

        [Display(Name = "GeneralManager")]
        GeneralManager = 15,

        [Display(Name = "Operation")]
        Operation = 20,

        [Display(Name = "Accounting")]
        Accounting = 21,

        [Display(Name = "Dealer")]
        Dealer = 30,
       
    }
}
