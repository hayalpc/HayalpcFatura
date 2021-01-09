using Hayalpc.Library.Common.Enums;
using Hayalpc.Library.Repository;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hayalpc.Fatura.Data.Models
{
    [Table("merchants", Schema ="merchant")]
    public class Merchant : HpModel
    {
        [Required]
        [StringLength(64)]
        [Updatable]
        public string Name { get; set; }

        [Required]
        [Updatable]
        public int Type { get; set; }

        [Required]
        [StringLength(128)]
        [Updatable]
        public string Address { get; set; }

        [Required]
        [StringLength(128)]
        [Updatable]
        public string Email { get; set; }

        [Required]
        [Updatable]
        [Column("country_id")]
        public long CountryId { get; set; }

        [Required]
        [Updatable]
        [Column("city_id")]
        public long CityId { get; set; }

        [Updatable]
        [Column("district_id")]
        public long? DistrictId { get; set; }
        [Updatable]
        public long PostalCode { get; set; }

        [Required]
        [StringLength(64)]
        [Updatable]
        public string TaxNo { get; set; }

        [Required]
        [StringLength(64)]
        [Updatable]
        public string TaxOffice { get; set; }

        [Required]
        [StringLength(64)]
        [Updatable]
        [Column("iban")]
        public string Iban { get; set; }

        [Required]
        [StringLength(64)]
        [Updatable]
        public string AccountName { get; set; }

        [Required]
        [StringLength(64)]
        [Updatable]
        public string ActivityArea { get; set; }

        [Required]
        [StringLength(64)]
        [Updatable]
        public string AuthorizedPersonName { get; set; }

        [Required]
        [StringLength(64)]
        [Updatable]
        public string AuthorizedPersonPhone { get; set; }

        [Required]
        [StringLength(64)]
        [Updatable]
        public string AuthorizedPersonEmail { get; set; }

        [Required]
        [StringLength(64)]
        [Updatable]
        public string AuthorizedPersonTckn { get; set; }

        [Required]
        [StringLength(64)]
        [Updatable]
        public string PrivateKey { get; set; }

        [Required]
        [StringLength(128)]
        [Updatable]
        public string CommercialTitle { get; set; }

        [StringLength(64)]
        [Updatable]
        public string CommercialRegistryNo { get; set; }

        [Required]
        [StringLength(64)]
        [Updatable]
        public string WebSite { get; set; }

        [Required]
        [StringLength(256)]
        [Updatable]
        public string BusinessDesc { get; set; }

        [Required]
        [Updatable]
        public Status Status { get; set; }

        //public virtual Country Country { get; set; }
        //public virtual City City { get; set; }
        //public virtual District District { get; set; }

        //public virtual ICollection<Transaction> Transactions { get; set; }
        //public virtual User CreaUser { get; set; }
        //public virtual User ModifUser { get; set; }

        [NotMapped]
        public virtual List<BlobFile> BlobFiles { get; set; }
    }
}
