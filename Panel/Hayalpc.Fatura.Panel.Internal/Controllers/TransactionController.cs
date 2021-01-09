using System.Collections.Generic;
using Hayalpc.Library.Common.Results;
using Microsoft.AspNetCore.Mvc;
using Hayalpc.Fatura.Data.Models;
using Hayalpc.Fatura.Panel.Internal.Services.Interfaces;
using DevExtreme.AspNet.Data;
using System;
using Hayalpc.Library.Common.Enums;
using Microsoft.AspNetCore.Authorization;
using Hayalpc.Fatura.Data;
using Hayalpc.Library.Repository.Interfaces;

namespace Hayalpc.Fatura.Panel.Internal.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService service;
        IRepository<Transaction> repository;
        IHpUnitOfWork<HpDbContext> unitOfWork;

        public TransactionController(ITransactionService service, IRepository<Transaction> repository, IHpUnitOfWork<HpDbContext> unitOfWork)
        {
            this.service = service;
            this.repository = repository;
            this.unitOfWork = unitOfWork;
        }

        [HttpGet("{id}")]
        public IDataResult<Transaction> Get(long Id)
        {
            return service.Get(Id);
        }

        [HttpPost]
        public IDataResult<IEnumerable<Transaction>> Search(DataSourceLoadOptionsBase req)
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

            var statues = new List<TransactionStatus> {
                TransactionStatus.NEW,
                TransactionStatus.CHARGED,
                TransactionStatus.ERROR,
                TransactionStatus.TIMEOUT,
                TransactionStatus.REFUNDED,
                TransactionStatus.UNDEFINED,
                TransactionStatus.FREE_TRIAL,
                TransactionStatus.INTERNAL_ERROR,
            };
            TransactionStatus status;
            CarrierId carrier;
            List<Transaction> reqList = new List<Transaction>();
            for (var i = 1; i <= 50000; i++)
            {
                status = statues[Faker.RandomNumber.Next(0, statues.Count - 1)];
                carrier = carriers[Faker.RandomNumber.Next(0, carriers.Count - 1)];

                var req = new Transaction
                {
                    MerchantId = 1,
                    ServiceId = 1,
                    CarrierId = (long)carrier,
                    Msisdn = "510" + Faker.RandomNumber.Next(1000000, 10000000),
                    Amount = Faker.RandomNumber.Next(5, 100),
                    Status = status,
                    Item = Faker.Name.First(),
                    UserIp = "127.0.0.1",
                    Channel = "test",
                    TxId = Guid.NewGuid(),
                    MerchantOrder = Faker.Identification.MedicareBeneficiaryIdentifier() + " " + Faker.RandomNumber.Next(5, 100),
                    Value1 = Faker.Identification.MedicareBeneficiaryIdentifier(),
                    Value2 = Faker.RandomNumber.Next(5, 100).ToString(),
                    Value3 = Faker.Name.First(),
                };
                req.CreateTime = DateTime.Now.AddDays((long)carrier * 2).AddMinutes(i);
                if (status == TransactionStatus.CHARGED)
                {
                    req.ChargeDate = DateTime.Now;
                    req.OperatorTransId = Faker.Identification.MedicareBeneficiaryIdentifier();
                }
                else if (status == TransactionStatus.ERROR)
                {
                    req.ErrDesc = "TestErr";
                    req.Error = "Err-001";
                }
                else if (status == TransactionStatus.REFUNDED)
                {
                    req.ChargeDate = DateTime.Now;
                    req.RefundDate = DateTime.Now;
                    req.RefundReason = Faker.Name.First();
                    req.OperatorTransId = Faker.Identification.MedicareBeneficiaryIdentifier();
                }
                reqList.Add(req);
                if(i % 1000 == 0)
                {
                    repository.InsertRange(reqList);
                    unitOfWork.SaveChanges();
                    reqList = new List<Transaction>();
                }
            }
            if(reqList.Count > 0)
            {
                repository.InsertRange(reqList);
                unitOfWork.SaveChanges();
            }
           

            return Ok(reqList.Count);
        }

    }
}
