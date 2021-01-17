using Hayalpc.Library.Common.Enums;
using Hayalpc.Library.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Hayalpc.Fatura;

namespace Hayalpc.Fatura.Data.Models
{
    [Table("blob_files")]
    public class BlobFile : HpModel
    {
        [Column("data_id")]
        public long DataId { get; set; }

        public Common.Enums.BlobFileType Type { get; set; }

        public Guid Token { get; set; }

        [Required]
        [StringLength(256)]
        public string Name { get; set; }

        [Required]
        [StringLength(256)]
        public string FileName { get; set; }

        [Required]
        [StringLength(256)]
        public string BlobUrl { get; set; }

        [Required]
        [StringLength(256)]
        public string AccountName { get; set; }

        [Required]
        [StringLength(256)]
        public string AccountKey { get; set; }
        
        [Updatable]
        public Status Status { get; set; }
    }
}
