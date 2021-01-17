using System;

namespace Hayalpc.Fatura.Common.Dtos
{
    public class UserBulletinDto : Hayalpc.Library.Common.Dtos.UserBulletinDto
    {
        public long? DealerId { get; set; } = 0;
    }
}
