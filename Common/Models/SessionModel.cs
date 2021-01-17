using Hayalpc.Fatura.Common.Dtos;
using System.Collections.Generic;

namespace Hayalpc.Fatura.Common.Models
{
    public class SessionModel : Hayalpc.Library.Common.Models.SessionModel
    {
        public new UserDto User { get; set; }
    }
}
