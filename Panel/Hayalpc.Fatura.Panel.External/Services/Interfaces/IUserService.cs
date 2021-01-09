using Hayalpc.Fatura.Panel.External.Models;
using Hayalpc.Library.Common.Models;
using Hayalpc.Library.Common.Results;

namespace Hayalpc.Fatura.Panel.External.Services.Interfaces
{
    public interface IUserService : IBaseService<UserVM>
    {
        IDataResult<SessionModel> Login(LoginRequest request);
        void LoadBulletins();
        void LoadUserData();
    }
}
