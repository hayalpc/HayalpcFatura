using Hayalpc.Library.Common.Results;
using Hayalpc.Fatura.Data.Models;
using System;

namespace Hayalpc.Fatura.Panel.Internal.Services
{
    public interface IResetPasswordService
    {
        IDataResult<ResetPassword> Add(ResetPassword resetPassword);
        IDataResult<ResetPassword> Add(User user);
        IDataResult<ResetPassword> GetByToken(Guid token);
        IDataResult<ResetPassword> Update(ResetPassword model);
        IDataResult<ResetPassword> Update(ResetPassword model, params string[] fields);
    }
}
