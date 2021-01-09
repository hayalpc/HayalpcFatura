using Hayalpc.Library.Common.Dtos;
using Hayalpc.Library.Common.Models;

namespace Hayalpc.Fatura.Panel.Internal.Helpers
{
    public interface ITokenCreator
    {
        string CreateToken();
        string CreateToken(LoginRequest loginRequest);
        string CreateToken(LoginRequest loginRequest, UserDto user);
    }
}
