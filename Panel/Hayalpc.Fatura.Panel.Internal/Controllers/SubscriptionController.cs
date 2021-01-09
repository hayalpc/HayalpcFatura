using System.Collections.Generic;
using Hayalpc.Library.Common.DataTables;
using Hayalpc.Library.Common.Results;
using Microsoft.AspNetCore.Mvc;
using Hayalpc.Fatura.Data.Models;
using Hayalpc.Fatura.Panel.Internal.Services.Interfaces;
using DevExtreme.AspNet.Mvc;
using DevExtreme.AspNet.Data.ResponseModel;
using DevExtreme.AspNet.Data;
using Microsoft.AspNetCore.Authorization;
using System;
using Hayalpc.Library.Common.Enums;
using Hayalpc.Fatura.Data;
using Hayalpc.Library.Repository.Interfaces;

namespace Hayalpc.Fatura.Panel.Internal.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SubscriptionController : ControllerBase
    {
        private readonly ISubscriptionService service;
        IRepository<Subscription> repository;
        IHpUnitOfWork<HpDbContext> unitOfWork;

        public SubscriptionController(ISubscriptionService service, IRepository<Subscription> repository, IHpUnitOfWork<HpDbContext> unitOfWork)
        {
            this.service = service;
            this.repository = repository;
            this.unitOfWork = unitOfWork;
        }

        [HttpGet("{id}")]
        public IDataResult<Subscription> Get(long Id)
        {
            return service.Get(Id);
        }

        [HttpPost]
        public IDataResult<IEnumerable<Subscription>> Search(DataSourceLoadOptionsBase req)
        {
            return service.Search(req);
        }

        [AllowAnonymous]
        public IActionResult Add()
        {
            var carriers = new List<CarrierId>
            {
                CarrierId.Turkcell,
                CarrierId.TurkTelekom,
                CarrierId.Vodafone,
            };

            var typies = new List<SubscriptionType>
            {
                SubscriptionType.Daily,
                SubscriptionType.Weekly,
                SubscriptionType.Montly,
            };

            var statues = new List<SubscriptionStatus> {
                SubscriptionStatus.RETRY,
                SubscriptionStatus.ACTIVE,
                SubscriptionStatus.SUSPEND,
                SubscriptionStatus.REJECT,
                SubscriptionStatus.CANCEL,
            };

            SubscriptionType type;
            SubscriptionStatus status;
            CarrierId carrier;
            Subscription req;
            List<Subscription> reqList = new List<Subscription>();

            for (var i = 1; i <= 50000; i++)
            {
                status = statues[Faker.RandomNumber.Next(0, statues.Count - 1)];
                carrier = carriers[Faker.RandomNumber.Next(0, carriers.Count - 1)];
                type = typies[Faker.RandomNumber.Next(0, typies.Count - 1)];

                req = new Subscription
                {
                    RenewalCount = 99,
                    MerchantId = 1,
                    ServiceId = 1,
                    CarrierId = (long)carrier,
                    SubsType = type,
                    Msisdn = "510" + Faker.RandomNumber.Next(1000000, 10000000),
                    HireAmount = Faker.RandomNumber.Next(5, 100),
                    Status = status,
                    Item = Faker.Name.First(),
                    Channel = "test",
                    SubsDate = DateTime.Now,
                    MerchantOrder = Faker.Identification.MedicareBeneficiaryIdentifier(),
                    CarrierSubId = Faker.Identification.MedicareBeneficiaryIdentifier(),
                    RenewalDate = DateTime.Now.AddDays((int)type)
                };
                req.CreateTime = DateTime.Now.AddDays((long)carrier*2).AddMinutes(i);

                if (status == SubscriptionStatus.CANCEL)
                {
                    req.CancelDate = DateTime.Now;
                    req.CancelReason = Faker.Name.First();
                }
                else if (status == SubscriptionStatus.SUSPEND)
                {
                    req.RenewalCount = req.RenewalCount - 10;
                    req.CancelDate = DateTime.Now;
                    req.CancelReason = "SUSPEND";
                }
                else if (status == SubscriptionStatus.RETRY)
                {
                    req.RenewalCount--;
                }
                reqList.Add(req);
                if (i % 1000 == 0)
                {
                    repository.InsertRange(reqList);
                    unitOfWork.SaveChanges();
                    reqList = new List<Subscription>();
                }
            }

            if (reqList.Count > 0)
            {
                repository.InsertRange(reqList);
                unitOfWork.SaveChanges();
            }

            return Ok(reqList.Count);
        }
    }
}
