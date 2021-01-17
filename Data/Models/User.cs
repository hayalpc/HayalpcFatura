using Hayalpc.Library.Common.Enums;
using Hayalpc.Library.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Hayalpc.Fatura.Data.Models
{
    [Table("users", Schema = "panel")]
    public class User : HpModel
    {
        [Required]
        [Updatable]
        public UserType Type { get; set; }
        [Required]
        [Updatable]
        public Status Status { get; set; }
        [Column("dealer_id")]
        public long? DealerId { get; set; }
        [Required]
        [StringLength(64)]
        [Updatable]
        public string Name { get; set; }
        [Required]
        [StringLength(64)]
        [Updatable]
        public string Surname { get; set; }
        [Updatable]
        [Column("image_path")]
        public string ImagePath { get; set; }
        [Required]
        [StringLength(64)]
        public string Email { get; set; }
        [Required]
        [StringLength(64)]
        [Updatable]
        public string Title { get; set; }
        [StringLength(128)]
        public string Password { get; set; }
        [Required]
        [StringLength(64)]
        [Updatable]
        public string Phone { get; set; }
        [StringLength(128)]
        [Updatable]
        [Column("last_session_id")]
        public string LastSessionId { get; set; }
        public DateTime? LastLoginDate { get; set; }
        public DateTime? LastPasswordChangeDate { get; set; }
        public int WrongLoginTryCount { get; set; } = 0;
        public DateTime? LastWrongLoginTryDate { get; set; }
        
        [NotMapped]
        public virtual List<Role> Roles { get; set; }
        [NotMapped]
        public virtual List<UserRole> UserRoles { get; set; }

        [NotMapped]
        public virtual List<long> RoleIds { get; set; }
    }
}
