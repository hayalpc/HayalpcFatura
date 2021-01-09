using Hayalpc.Library.Common.Dtos;
using Hayalpc.Library.Common.Enums;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Hayalpc.Fatura.Panel.External.Models
{
    public class RoleVM : BaseVM
    {
        [DisplayName("RoleId")]
        public override long Id { get; set; }

        [Required(ErrorMessage = "RequiredField")]
        [DisplayName("RoleType")]
        public UserType Type { get; set; }

        [Required(ErrorMessage = "RequiredField")]
        [StringLength(64)]
        [DisplayName("RoleName")]
        public string Name { get; set; }

        public virtual string FullName { get { return Name + $"({Id})"; } }

        [Required(ErrorMessage = "RequiredField")]
        [StringLength(64)]
        public string Description { get; set; }

        [Required(ErrorMessage = "RequiredField")]
        [DisplayName("RoleStatus")]
        public Status Status { get; set; }

        public virtual List<RolePermissionDto> RolePermissions { get; set; }
    }
}
