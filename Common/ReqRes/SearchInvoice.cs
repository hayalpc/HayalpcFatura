using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hayalpc.Fatura.Common.ReqRes
{
    public class SearchInvoice
    {
        [Required]
        public string SubscriberNo { get; set; }
        [Required]
        public long InstitutionId { get; set; }
        [Required]
        public string UserIp { get; set; }
        [Required]
        public string Channel { get; set; }
    }
}
