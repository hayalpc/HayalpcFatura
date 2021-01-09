using Hayalpc.Library.Common.Enums;
using Hayalpc.Library.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Hayalpc.Fatura.Data.Models
{
    [Table("user_bulletins", Schema = "tracking")]
    public class UserBulletin : HpModel
    {
        [Column("merchant_id")]
        public long? MerchantId { get; set; } = 0;

        [Column("role_id")]
        public long? RoleGroupId { get; set; } = 0;

        [Column("user_id")]
        public long UserId { get; set; } = 0;

        [Required]
        [StringLength(64)]
        public string ActionType { get; set; }
        [Required]
        [StringLength(128)]
        public string Title { get; set; }

        [Required]
        [StringLength(1024)]
        public string Message { get; set; }
        [StringLength(8)]
        public string Language { get; set; }

        [Required]
        public UserBulletinType Type { get; set; }

        [Required]
        public UserBulletinStatus Status { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime ExpireDate { get; set; }

        public DateTime? ReadDate { get; set; }


    }
}
