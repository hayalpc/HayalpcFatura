using Hayalpc.Fatura.Data.Models;
using Hayalpc.Library.Common.Results;

namespace Hayalpc.Fatura.Panel.Internal.Services.Interfaces
{
    public interface IUserBulletinService : IBaseService<UserBulletin>
    {
        IResult Read(long Id);
        IResult ReadAll();
    }
}
