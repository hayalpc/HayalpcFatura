using Hayalpc.Library.Common.Dtos;
using Hayalpc.Library.Common.Enums;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Security.Policy;

namespace Hayalpc.Fatura.Panel.External.Models
{
    public class MerchantVM : BaseVM
    {
        [DisplayName("MerchantName")]
        [StringLength(64)]
        [Required(ErrorMessage = "RequiredField")]
        public string Name { get; set; }

        [DisplayName("MerchantType")]
        [Required(ErrorMessage = "RequiredField")]
        public MerchantType Type { get; set; }

        [DisplayName("Address")]
        [Required(ErrorMessage = "RequiredField")]
        [StringLength(128)]
        public string Address { get; set; }

        [DisplayName("Email")]
        [Required(ErrorMessage = "RequiredField")]
        [StringLength(128)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [DisplayName("Country")]
        [Required(ErrorMessage = "RequiredField")]
        public long CountryId { get; set; }

        [DisplayName("City")]
        [Required(ErrorMessage = "RequiredField")]
        public long CityId { get; set; }

        [DisplayName("District")]
        [Required(ErrorMessage = "RequiredField")]
        public long? DistrictId { get; set; }

        [DisplayName("PostalCode")]
        [Required(ErrorMessage = "RequiredField")]
        [Range(9999,999999)]
        public long PostalCode { get; set; }

        [DisplayName("TaxNo")]
        [Required(ErrorMessage = "RequiredField")]
        [StringLength(16)]
        public string TaxNo { get; set; }

        [DisplayName("TaxOffice")]
        [Required(ErrorMessage = "RequiredField")]
        [StringLength(32)]
        public string TaxOffice { get; set; }

        [DisplayName("Iban")]
        [Required(ErrorMessage = "RequiredField")]
        [MinLength(26)]
        [MaxLength(26)]
        [RegularExpression(@"^[a-zA-Z]{2}[0-9]{2}\s?[a-zA-Z0-9]{4}\s?[0-9]{4}\s?[0-9]{3}([a-zA-Z0-9]\s?[a-zA-Z0-9]{0,4}\s?[a-zA-Z0-9]{0,4}\s?[a-zA-Z0-9]{0,4}\s?[a-zA-Z0-9]{0,3})?$", ErrorMessage = "Error.InvalidIban")]
        public string Iban { get; set; }

        [DisplayName("AccountName")]
        [Required(ErrorMessage = "RequiredField")]
        [StringLength(64)]
        public string AccountName { get; set; }

        [DisplayName("ActivityArea")]
        [Required(ErrorMessage = "RequiredField")]
        [StringLength(64)]
        public string ActivityArea { get; set; }

        [DisplayName("AuthorizedPersonName")]
        [Required(ErrorMessage = "RequiredField")]
        [StringLength(64)]
        public string AuthorizedPersonName { get; set; }

        [DisplayName("AuthorizedPersonPhone")]
        [Required(ErrorMessage = "RequiredField")]
        [StringLength(64)]
        public string AuthorizedPersonPhone { get; set; }

        [DisplayName("AuthorizedPersonEmail")]
        [Required(ErrorMessage = "RequiredField")]
        [StringLength(64)]
        [DataType(DataType.EmailAddress)]
        public string AuthorizedPersonEmail { get; set; }

        [DisplayName("AuthorizedPersonTckn")]
        [Required(ErrorMessage = "RequiredField")]
        [StringLength(11)]
        [RegularExpression("([0-9]+){11}")]
        public string AuthorizedPersonTckn { get; set; }

        [DisplayName("PrivateKey")]
        [Required(ErrorMessage = "RequiredField")]
        [StringLength(64)]
        public string PrivateKey { get; set; }

        [DisplayName("CommercialTitle")]
        [Required(ErrorMessage = "RequiredField")]
        [StringLength(128)]
        public string CommercialTitle { get; set; }

        [DisplayName("CommercialRegistryNo")]
        [StringLength(16)]
        public string CommercialRegistryNo { get; set; }

        [DisplayName("WebSite")]
        [Required(ErrorMessage = "RequiredField")]
        [StringLength(64)]
        public string WebSite { get; set; }

        [DisplayName("BusinessDesc")]
        [Required(ErrorMessage = "RequiredField")]
        [StringLength(256)]
        public string BusinessDesc { get; set; }

        [DisplayName("Status")]
        [Required(ErrorMessage = "RequiredField")]
        public Status Status { get; set; }

        //public virtual Country Country { get; set; }
        //public virtual City City { get; set; }
        //public virtual District District { get; set; }

        //public virtual ICollection<Transaction> Transactions { get; set; }
        //public virtual User CreaUser { get; set; }
        //public virtual User ModifUser { get; set; }

        public virtual List<BlobFileDto> BlobFiles { get; set; }

    }
}
