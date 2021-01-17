using Hayalpc.Fatura.Common.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hayalpc.Fatura.Panel.Internal.Helpers.Interfaces
{
    public interface IMailHelper : Hayalpc.Library.Common.Helpers.Interfaces.IMailHelper
    {
        bool SendResetPassword(UserDto user,Library.Common.Dtos.ResetPasswordDto resetPassword);
    }
}
