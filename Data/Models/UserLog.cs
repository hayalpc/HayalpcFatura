using Hayalpc.Library.Common.Enums;
using Hayalpc.Library.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Hayalpc.Fatura.Data.Models
{
    [Table("user_logs", Schema = "tracking")]
    public class UserLog : HpModel
    {
        [Column("dealer_id")]
        public long? DealerId { get; set; } = 0;
        
        [Required]
        [Column("user_id")]
        public long UserId { get; set; } = 0;

        [Required]
        [StringLength(64)]
        public string ActionType { get; set; }

        [StringLength(512)]
        public string Note { get; set; }

        [StringLength(32)]
        [Column("user_ip")]
        public string UserIp { get; set; }

    }
}
