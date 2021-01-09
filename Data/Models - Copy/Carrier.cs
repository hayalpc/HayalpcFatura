using Hayalpc.Library.Repository;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hayalpc.Fatura.Data.Models
{
    [Table("carriers", Schema = "txn")]
    public class Carrier : HpModel
    {
        [Required]
        [StringLength(64)]
        public string Code { get; set; }
        
        [Required]
        [StringLength(64)]
        public string Name { get; set; }
        
        [Required]
        [StringLength(64)]
        public string Title { get; set; }
        
        [StringLength(16)]
        public string Phone1 { get; set; }
        
        [StringLength(16)]
        public string Phone2 { get; set; }
        
        [StringLength(256)]
        public string Address { get; set; }

        [StringLength(64)]
        public string AuthorizedPerson { get; set; }
        
        [StringLength(64)]
        public string AuthorizedPersonPhone { get; set; }

        [StringLength(64)]
        public string AuthorizedPersonEmail { get; set; }
        
        [StringLength(256)]
        public string Logo { get; set; }

    }
}