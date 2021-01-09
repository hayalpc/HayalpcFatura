using Hayalpc.Fatura.Data;
using Hayalpc.Fatura.Data.Models;
using Hayalpc.Fatura.Panel.Internal.Services.Interfaces;
using Hayalpc.Library.Common.Results;
using Hayalpc.Library.Log;
using Hayalpc.Library.Repository.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Linq;

namespace Hayalpc.Fatura.Panel.Internal.Services
{
    public class BlobFileService : BaseService<BlobFile>, IBlobFileService
    {
        public BlobFileService(IRepository<BlobFile> repository, IHpLogger logger, IHpUnitOfWork<HpDbContext> unitOfWork, IMemoryCache memoryCache) :
            base(repository, logger, unitOfWork, memoryCache)
        {
        }

        public virtual IDataResult<BlobFile> GetByGuid(Guid guid)
        {
            var model = repository.GetQuery(x=>x.Token == guid && x.Status == Library.Common.Enums.Status.Active).FirstOrDefault();
            if (model != null)
            {
                return new SuccessDataResult<BlobFile>(model);
            }
            return new ErrorDataResult<BlobFile>("RecordNotFound");
        }

    }
}
