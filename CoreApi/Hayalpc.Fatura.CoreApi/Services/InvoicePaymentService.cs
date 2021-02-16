﻿using Hayalpc.Fatura.CoreApi.Services.Interfaces;
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
    public class InvoicePaymentService : IInvoicePaymentService
    {
        private readonly IRepository<InvoicePayment> repository;
        private readonly IHpUnitOfWork<HpDbContext> unitOfWork;
        private readonly IMemoryCache cache;
        private readonly IHpLogger logger;

        public InvoicePaymentService(IRepository<InvoicePayment> repository, IHpUnitOfWork<HpDbContext> unitOfWork, IMemoryCache cache, IHpLogger logger)
        {
            this.repository = repository;
            this.unitOfWork = unitOfWork;
            this.logger = logger;
            this.cache = cache;
        }

        public IDataResult<InvoicePayment> Add(InvoicePayment invoice)
        {
            try
            {
                if (invoice.Token == null)
                    invoice.Token = Guid.NewGuid();
                invoice.CreateTime = DateTime.Now;
                invoice.UpdateTime = DateTime.Now;
                invoice.CreateUserId = -1;
                invoice.UpdateUserId = -1;

                repository.Insert(invoice);
                unitOfWork.SaveChanges();
                return new SuccessDataResult<InvoicePayment>(invoice);
            }
            catch (Exception exp)
            {
                logger.Error(exp);
                return new ErrorDataResult<InvoicePayment>(500, exp.Message);
            }
        }

        public IDataResult<InvoicePayment> Update(InvoicePayment invoice)
        {
            try
            {
                invoice.UpdateTime = DateTime.Now;

                repository.Update(invoice);
                unitOfWork.SaveChanges();
                return new SuccessDataResult<InvoicePayment>(invoice);
            }
            catch (Exception exp)
            {
                logger.Error(exp);
                return new ErrorDataResult<InvoicePayment>(500, exp.Message);
            }
        }
    }
}
