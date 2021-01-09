using Hayalpc.Library.Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Hayalpc.Fatura.Panel.External.Models
{
    public class BlobFileVM : BaseVM
    {
        public long DataId { get; set; }
        public BlobFileType Type { get; set; }
        public Guid Token { get; set; }
        public string Name { get; set; }
        public string FileName { get; set; }
        public string BlobUrl { get; set; }
        public string AccountName { get; set; }
        public string AccountKey { get; set; }
        public Status Status { get; set; }

    }
}
