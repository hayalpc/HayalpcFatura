using Hayalpc.Fatura.CoreApi.Services.Interfaces;
using Hayalpc.Fatura.Data;
using Hayalpc.Fatura.Data.Models;
using Hayalpc.Library.Common.Results;
using Hayalpc.Library.Log;
using Hayalpc.Library.Repository.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hayalpc.Fatura.CoreApi.Services
{
    public class InvoiceService : IInvoiceService
    {
        private readonly IRepository<Invoice> repository;
        private readonly IHpUnitOfWork<HpDbContext> unitOfWork;
        private readonly IMemoryCache cache;
        private readonly IHpLogger logger;

        public InvoiceService(IRepository<Invoice> repository, IHpUnitOfWork<HpDbContext> unitOfWork, IMemoryCache cache, IHpLogger logger)
        {
            this.repository = repository;
            this.unitOfWork = unitOfWork;
            this.logger = logger;
            this.cache = cache;
        }

        public IDataResult<Invoice> Add(Invoice invoice)
        {
            try
            {
                invoice.Token = Guid.NewGuid();
                invoice.CreateTime = DateTime.Now;
                invoice.UpdateTime = DateTime.Now;
                invoice.CreateUserId = -1;
                invoice.UpdateUserId = -1;

                repository.Insert(invoice);
                unitOfWork.SaveChanges();
                return new SuccessDataResult<Invoice>(invoice);
            }
            catch (Exception exp)
            {
                logger.Error(exp);
                return new ErrorDataResult<Invoice>(500,exp.Message);
            }
        }

        public IDataResult<Invoice> Get(long invoiceId)
        {
            try
            {
                var data = repository.GetById(invoiceId);
                if(data!= null)
                    return new SuccessDataResult<Invoice>(data);
                else
                    return new ErrorDataResult<Invoice>(404, "NotFound");
            }
            catch (Exception exp)
            {
                logger.Error(exp);
                return new ErrorDataResult<Invoice>(500, exp.Message);
            }
        }

        public IDataResult<Invoice> Update(Invoice invoice)
        {
            try
            {
                invoice.UpdateTime = DateTime.Now;

                repository.Update(invoice);
                unitOfWork.SaveChanges();
                return new SuccessDataResult<Invoice>(invoice);
            }
            catch (Exception exp)
            {
                logger.Error(exp);
                return new ErrorDataResult<Invoice>(500, exp.Message);
            }
        }
    }
}
