using Hayalpc.Fatura.Data;
using Hayalpc.Fatura.Data.Models;
using Hayalpc.Fatura.Panel.Internal.Services.Interfaces;
using Hayalpc.Library.Common.Helpers;
using Hayalpc.Library.Log;
using Hayalpc.Library.Repository.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using System.Linq;

namespace Hayalpc.Fatura.Panel.Internal.Services
{
    public class DealerService : BaseService<Dealer>, IDealerService
    {
        public DealerService(IRepository<Dealer> repository, IHpLogger logger, IHpUnitOfWork<HpDbContext> unitOfWork, IMemoryCache memoryCache) :
            base(repository, logger, unitOfWork, memoryCache)
        {
        }

        public override Dealer BeforeGet(Dealer model)
        {
            var blobFiles = repository.GetQuery<BlobFile>(x => x.DataId == model.Id && x.Type == Common.Enums.BlobFileType.Dealer && x.Status == Library.Common.Enums.Status.Active).ToList();
            if(blobFiles.Count > 0)
            {
                model.BlobFiles = blobFiles;
            } 
            return base.BeforeGet(model);
        }

        public override IQueryable<Dealer> BeforeSearch(IQueryable<Dealer> req)
        {
            if(Fatura.Common.Helpers.RequestHelper.DealerId > 0)
            {
                req = req.Where(x => x.Id == Fatura.Common.Helpers.RequestHelper.DealerId);
            }
            return base.BeforeSearch(req);
        }
        
    }
}
