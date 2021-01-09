using Hayalpc.Fatura.Panel.External.Models;
using Hayalpc.Fatura.Panel.External.Resources;
using Hayalpc.Fatura.Panel.External.Services.Interfaces;
using Hayalpc.Library.Common.Helpers.Interfaces;
using Hayalpc.Library.Log;
using Microsoft.AspNetCore.Mvc;

namespace Hayalpc.Fatura.Panel.External.Controllers
{
    public class SubscriptionController : BaseController<SubscriptionVM, ISubscriptionService>
    {
        public SubscriptionController(LocService tr, ISessionHelper session, IHpLogger logger, ISubscriptionService service) : base(service,tr, session, logger)
        {
        }

        public override IActionResult Add() => NotFound();

        [HttpPost]
        public override IActionResult Add(SubscriptionVM form) => NotFound();

        public override IActionResult Update(long id) => NotFound();

        [HttpPost]
        public override IActionResult Update(long id, SubscriptionVM model) => NotFound();
    }
}
