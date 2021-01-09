using Hayalpc.Library.Repository;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hayalpc.Fatura.Data.Models
{
    [Table("city", Schema = "parameter")]
    public class City : HpModel
    {
        [Required]
        [StringLength(128)]
        public string Name { get; set; }

        [StringLength(128)]
        public string Description { get; set; }

        [Required]
        [Column("country_id")]
        public long CountryId { get; set; }

    }
}
