using Hayalpc.Library.Common.Dtos;
using Hayalpc.Library.Common.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Hayalpc.Fatura.Panel.External.Models
{
    public class UserVM : BaseVM
    {
        [Required(ErrorMessage = "RequiredField")]
        [DisplayName("UserType")]
        public UserType Type { get; set; }

        [Required(ErrorMessage = "RequiredField")]
        [DisplayName("UserStatus")]
        public Status Status { get; set; }

        public long? MerchantId { get; set; } = 0;

        [Required(ErrorMessage = "RequiredField")]
        [StringLength(64)]
        [DisplayName("UserName")]
        public string Name { get; set; }

        [Required(ErrorMessage = "RequiredField")]
        [StringLength(64)]
        [DisplayName("UserSurname")]
        public string Surname { get; set; }

        public virtual string FullName { get { return Name + " " + Surname; } }

        [Required(ErrorMessage = "RequiredField")]
        [StringLength(64)]
        [DisplayName("VM.UserTitle")]
        public string Title { get; set; }

        public string ImagePath { get; set; }

        [Required(ErrorMessage = "RequiredField")]
        [StringLength(64)]
        [DataType(DataType.EmailAddress)]
        [DisplayName("Email")]
        public string Email { get; set; }

        public string Password { get; set; }

        [Required(ErrorMessage = "RequiredField")]
        [DataType(DataType.PhoneNumber)]
        [DisplayName("Phone")]
        public string Phone { get; set; }

        public string LastSessionId { get; set; }

        [DisplayName("VM.LastLoginDate")]
        public DateTime? LastLoginDate { get; set; }

        public DateTime? LastPasswordChangeDate { get; set; }

        public int WrongLoginTryCount { get; set; } = 0;

        public DateTime? LastWrongLoginTryDate { get; set; }

        public virtual ChangePasswordVM ChangePasswordVM { get; set; }

        public virtual List<RoleDto> Roles { get; set; } = new List<RoleDto>();
        public virtual List<UserRoleDto> UserRoles { get; set; } = new List<UserRoleDto>();
        public virtual List<long> RoleIds {get;set; } = new List<long>();

    }
}
