using Hayalpc.Fatura.Data;
using Hayalpc.Fatura.Data.Models;
using Hayalpc.Fatura.Panel.Internal.Services.Interfaces;
using Hayalpc.Library.Common.Helpers;
using Hayalpc.Library.Common.Results;
using Hayalpc.Library.Log;
using Hayalpc.Library.Repository.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Linq;

namespace Hayalpc.Fatura.Panel.Internal.Services
{
    public class TransactionService : BaseService<Transaction>, ITransactionService
    {
        public TransactionService(IRepository<Transaction> repository, IHpLogger logger, IHpUnitOfWork<HpDbContext> unitOfWork, IMemoryCache memoryCache)
            : base(repository, logger, unitOfWork, memoryCache)
        {
        }
        public override IDataResult<Transaction> Update(Transaction model) => throw new NotImplementedException();
        public override IDataResult<Transaction> Update(Transaction model, params string[] fields) => throw new NotImplementedException();

        public override IQueryable<Transaction> BeforeSearch(IQueryable<Transaction> req)
        {
            if (RequestHelper.MerchantId > 0)
            {
                req = req.Where(x => x.MerchantId == RequestHelper.MerchantId);
            }
            return base.BeforeSearch(req);
        }
    }
}
