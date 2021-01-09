using Hayalpc.Fatura.Panel.External.Models;
using Hayalpc.Fatura.Panel.External.Resources;
using Hayalpc.Fatura.Panel.External.Services.Interfaces;
using Hayalpc.Library.Common.Helpers.Interfaces;
using Hayalpc.Library.Log;
using Microsoft.AspNetCore.Mvc;

namespace Hayalpc.Fatura.Panel.External.Controllers
{
    public class TransactionController : BaseController<TransactionVM, ITransactionService>
    {
        public TransactionController(LocService tr, ISessionHelper session, IHpLogger logger, ITransactionService service) : base(service,tr, session, logger)
        {
        }

        public override IActionResult Add() => NotFound();

        [HttpPost]
        public override IActionResult Add(TransactionVM form) => NotFound();

        public override IActionResult Update(long id) => NotFound();

        [HttpPost]
        public override IActionResult Update(long id, TransactionVM model) => NotFound();
    }
}
