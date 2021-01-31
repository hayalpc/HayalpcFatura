using Hayalpc.Library.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Hayalpc.Fatura.Data.Models
{
    [Table("user_roles", Schema = "panel")]
    public class UserRole : HpModel
    {
        [Column("user_id")]
        public long UserId { get; set; }
        [Column("role_id")]
        public long RoleId { get; set; }

        public virtual Role Role { get; set; }
    }
}
