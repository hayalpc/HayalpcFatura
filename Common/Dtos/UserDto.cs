using Hayalpc.Fatura.Common.Dtos;
using Hayalpc.Fatura.Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hayalpc.Fatura.Common.Dtos
{
    public class UserDto : Hayalpc.Library.Common.Dtos.UserDto
    {
        public new UserType Type { get; set; }
        public long? DealerId { get; set; }
        public virtual bool IsDealer { get { return DealerId > 0; } }

        [NotMapped]
        public virtual DealerDto Merchant { get; set; } = new DealerDto();

        [NotMapped]
        public new virtual List<UserBulletinDto> Bulletins { get; set; }

    }
}
