using Hayalpc.Fatura.Data;
using Hayalpc.Fatura.Data.Models;
using Hayalpc.Fatura.Panel.Internal.Services.Interfaces;
using Hayalpc.Library.Common;
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
    public class MerchantPaymentService : BaseService<MerchantPayment>, IMerchantPaymentService
    {
        public MerchantPaymentService(IRepository<MerchantPayment> repository, IHpLogger logger, IHpUnitOfWork<HpDbContext> unitOfWork, IMemoryCache memoryCache) :
            base(repository, logger, unitOfWork, memoryCache)
        {
        }

        public IResult Calculate(long[] merchantIds)
        {
            //iyi bak
            var items = new List<CarrierCollectionItem>();
            if (merchantIds != null)
            {
                items = repository.GetQuery<CarrierCollectionItem>(x => x.Status == Library.Common.Enums.CarrierCollectionStatus.Active && merchantIds.Contains(x.MerchantId)).ToList();
            }
            else
            {
                items = repository.GetQuery<CarrierCollectionItem>(x => x.Status == Library.Common.Enums.CarrierCollectionStatus.Active).ToList();
            }

            if (items.Count > 0)
            {
                try
                {
                    unitOfWork.BeginTransaction();
                    var merchants = items.GroupBy(x => x.MerchantId).Select(x => x.Key).ToArray();
                    var paymentList = new List<MerchantPayment>();
                    foreach (var merchant in merchants)
                    {
                        var payment = new MerchantPayment
                        {
                            MerchantId = merchant,
                            Status = Library.Common.Enums.MerchantPaymentStatus.Processing,
                            CreateTime = DateTime.Now,
                            CreateUserId = RequestHelper.UserId,
                        };
                        repository.Insert(payment);
                        paymentList.Add(payment);
                    }
                    unitOfWork.SaveChanges();

                    foreach (var item in items)
                    {
                        var merchantPayment = paymentList.Where(x => x.MerchantId == item.MerchantId).FirstOrDefault();
                        merchantPayment.TotalAmount += item.Amount;
                        merchantPayment.OperatorAmount += item.OperatorAmount;
                        merchantPayment.AggregatorAmount += item.AggregatorAmount;
                        merchantPayment.ShareAmount += item.MerchantAmount;

                        item.UpdateTime = DateTime.Now;
                        item.UpdateUserId = RequestHelper.UserId;
                        item.Status = Library.Common.Enums.CarrierCollectionStatus.PaymentPending;
                        item.MerchantPaymentId = merchantPayment.Id;
                        repository.Update(item, "UpdateTime", "UpdateUserId", "Status", "MerchantPaymentId");

                        repository.Update(merchantPayment, "TotalAmount", "OperatorAmount", "AggregatorAmount", "ShareAmount");
                    }
                    unitOfWork.SaveChanges();

                    foreach (var merchantPayment in paymentList)
                    {
                        merchantPayment.Status = Library.Common.Enums.MerchantPaymentStatus.ApprovePending;
                        merchantPayment.UpdateTime = DateTime.Now;
                        merchantPayment.UpdateUserId = RequestHelper.UserId;
                        repository.Update(merchantPayment, "Status", "UpdateTime", "UpdateUserId");
                    }
                    unitOfWork.SaveChanges();
                    unitOfWork.CommitTransaction();

                }
                catch (Exception exp)
                {
                    unitOfWork.RollBackTransaction();
                    logger.Error(exp.ToString());
                }
            }
            return new SuccessResult();
        }
    }
}
