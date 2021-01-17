using Hayalpc.Library.Repository;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hayalpc.Fatura.Data.Models
{
    [Table("district", Schema = "parameter")]
    public class District : HpModel
    {
        [Required]
        [Column("city_id")]
        public long CityId { get; set; }

        [Required]
        [StringLength(256)]
        public string Name { get; set; }

        [StringLength(256)]
        public string Description { get; set; }

    }
}
