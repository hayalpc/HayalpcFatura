using Hayalpc.Fatura.Data;
using Hayalpc.Fatura.Data.Models;
using Hayalpc.Fatura.Panel.Internal.Services.Interfaces;
using Hayalpc.Library.Log;
using Hayalpc.Library.Repository.Interfaces;
using Microsoft.Extensions.Caching.Memory;

namespace Hayalpc.Fatura.Panel.Internal.Services
{
    public class CarrierCollectionItemService : BaseService<CarrierCollectionItem>, ICarrierCollectionItemService
    {
        public CarrierCollectionItemService(IRepository<CarrierCollectionItem> repository, IHpLogger logger, IHpUnitOfWork<HpDbContext> unitOfWork, IMemoryCache memoryCache) :
            base(repository, logger, unitOfWork, memoryCache)
        {
        }
       
    }
}
