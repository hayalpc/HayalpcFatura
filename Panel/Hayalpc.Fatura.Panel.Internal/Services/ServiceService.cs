using Hayalpc.Fatura.Data;
using Hayalpc.Fatura.Data.Models;
using Hayalpc.Fatura.Panel.Internal.Services.Interfaces;
using Hayalpc.Library.Common.DataTables;
using Hayalpc.Library.Common.Helpers;
using Hayalpc.Library.Common.Results;
using Hayalpc.Library.Log;
using Hayalpc.Library.Repository.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hayalpc.Fatura.Panel.Internal.Services
{
    public class ServiceService : BaseService<Service>, IServiceService
    {
        public ServiceService(IRepository<Service> repository, IHpLogger logger, IHpUnitOfWork<HpDbContext> unitOfWork, IMemoryCache memoryCache)
            : base(repository, logger, unitOfWork, memoryCache)
        {
        }

        public override Service BeforeGet(Service model)
        {
            var blobFiles = repository.GetQuery<BlobFile>(x => x.DataId == model.Id && x.Type == Library.Common.Enums.BlobFileType.Service && x.Status == Library.Common.Enums.Status.Active).ToList();
            if (blobFiles.Count > 0)
            {
                model.BlobFiles = blobFiles;
            }
            return base.BeforeGet(model);
        }

        public IDataResult<List<Service>> GetByMerchant(long Id)
        {
            try
            {
                var model = repository.GetQuery(x => x.MerchantId == Id).ToList();
                return new SuccessDataResult<List<Service>>(model);
            }
            catch (Exception exp)
            {
                logger.Error(exp.ToString());
                return new ErrorDataResult<List<Service>>("InternalError");
            }
        }

        public override IQueryable<Service> BeforeSearch(IQueryable<Service> req)
        {
            if (RequestHelper.MerchantId > 0)
            {
                req = req.Where(x => x.MerchantId == RequestHelper.MerchantId);
            }
            return base.BeforeSearch(req);
        }
    }
}
