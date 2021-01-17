using Hayalpc.Fatura.Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Hayalpc.Fatura.Common.Dtos
{
    public class BlobFileDto : Hayalpc.Library.Common.Dtos.BlobFileDto
    {
        public new BlobFileType Type { get; set; }
    }
}
