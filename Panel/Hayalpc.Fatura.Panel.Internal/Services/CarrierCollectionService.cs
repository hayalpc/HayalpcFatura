using Hayalpc.Fatura.Data;
using Hayalpc.Fatura.Data.Models;
using Hayalpc.Fatura.Panel.Internal.Services.Interfaces;
using Hayalpc.Library.Common;
using Hayalpc.Library.Common.Enums;
using Hayalpc.Library.Common.Helpers;
using Hayalpc.Library.Log;
using Hayalpc.Library.Repository.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Hayalpc.Fatura.Panel.Internal.Services
{
    public class CarrierCollectionService : BaseService<CarrierCollection>, ICarrierCollectionService
    {
        public CarrierCollectionService(IRepository<CarrierCollection> repository, IHpLogger logger, IHpUnitOfWork<HpDbContext> unitOfWork, IMemoryCache memoryCache) : base(repository, logger, unitOfWork, memoryCache)
        {
        }

        public override CarrierCollection AfterUpdate(CarrierCollection model)
        {
            model.Status = CarrierCollectionStatus.Processing;
            repository.Update(model);
            unitOfWork.SaveChanges();

            var totalAmount = 0m;
            var opAmount = 0m;
            var aggAmount = 0m;
            var merchantAmount = 0m;
            var list = new List<CarrierCollectionItem>();
            for (var i = 0; i < Faker.RandomNumber.Next(2000, 5000); i++)
            {
                var amount = Faker.RandomNumber.Next(50, 100);
                var req = new CarrierCollectionItem
                {
                    CarrierCollectionId = model.Id,
                    TransactionId = 0,
                    MerchantId = 2,
                    ServiceId = 1,
                    Amount = amount,
                    OperatorAmount = amount * 5m / 100,
                    AggregatorAmount = amount * 1.5m / 100,
                    Status = CarrierCollectionStatus.Active,
                    PaymentDate = DateTime.Now,
                    ChargeDate = DateTime.Now,
                    ReportDate = DateTime.Now,
                    CreateTime = DateTime.Now,
                    UpdateTime = DateTime.Now,
                    CreateUserId = RequestHelper.UserId,
                    UpdateUserId = RequestHelper.UserId,
                };
                req.MerchantAmount = amount - req.OperatorAmount - req.AggregatorAmount;

                totalAmount += amount;
                opAmount += req.OperatorAmount;
                aggAmount += req.AggregatorAmount;
                merchantAmount += req.MerchantAmount;

                list.Add(req);
            }
            repository.InsertRange(list);
            unitOfWork.SaveChanges();

            model.OperatorAmount = opAmount;
            model.TotalAmount = totalAmount;
            model.AggAmount = aggAmount;
            model.ShareAmount = merchantAmount;
            model.Status = CarrierCollectionStatus.Active;

            repository.Update(model);

            unitOfWork.SaveChanges();

            return base.AfterUpdate(model);
        }
    }
}
