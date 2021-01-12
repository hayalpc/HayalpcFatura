using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hayalpc.Fatura.Vezne.External.Models
{
    public class SearchInvoice
    {
        [Required]
        public string SubscriberNo { get; set; }
        [Required]
        public long InstituteId { get; set; }
    }
}
