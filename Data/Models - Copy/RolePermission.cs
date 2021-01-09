using Hayalpc.Library.Common.Enums;
using Hayalpc.Library.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Hayalpc.Fatura.Data.Models
{
    [Table("role_permissions", Schema = "panel")]
    public class RolePermission : HpModel
    {
        [Required]
        [Column("role_id")]
        public long RoleId { get; set; }
        
        [Required]
        [Updatable]
        public string Name { get; set; }
        
        [Updatable]
        [Column("icon")]
        public string Icon { get; set; }
        
        [Column("role_permission_id")]
        public long RolePermissionId { get; set; } = 0;
        
        [Updatable]
        public int Order { get; set; } = 0;
        
        [Updatable]
        public string Controller { get; set; }
        
        [Updatable]
        public string Action { get; set; }
        
        [Updatable]
        public string Description { get; set; }
        
        [Updatable]
        [Column("is_menu")]
        public bool IsMenu { get; set; }
        
        [Updatable]
        public Status Status { get; set; }

    }
}
