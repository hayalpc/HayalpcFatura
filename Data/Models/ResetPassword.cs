using Hayalpc.Library.Common.Enums;
using Hayalpc.Library.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Hayalpc.Fatura.Data.Models
{
    [Table("reset_passwords", Schema = "panel")]
    public class ResetPassword : HpModel
    {
        public Guid Token { get; set; }
        
        [Column("user_id")]
        public long UserId {get;set;}
        
        [Updatable]
        public Status Status { get; set; }
    }
}
