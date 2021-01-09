using Hayalpc.Fatura.Data.Models;
using Hayalpc.Library.Common.Results;
using System;

namespace Hayalpc.Fatura.Panel.Internal.Services.Interfaces
{
    public interface IBlobFileService : IBaseService<BlobFile>
    {
        IDataResult<BlobFile> GetByGuid(Guid guid);
    }
}
