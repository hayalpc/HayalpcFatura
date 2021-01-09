using Hayalpc.Fatura.Panel.External.Models;
using Hayalpc.Fatura.Panel.External.Resources;
using Hayalpc.Fatura.Panel.External.Services.Interfaces;
using Hayalpc.Library.Common.Helpers.Interfaces;
using Hayalpc.Library.Log;
using Microsoft.AspNetCore.Mvc;

namespace Hayalpc.Fatura.Panel.External.Controllers
{
    public class SmsController : BaseController<SmsVM, ISmsService>
    {
        public SmsController(LocService tr, ISessionHelper session, IHpLogger logger, ISmsService service) : base(service,tr, session, logger)
        {
        }

        public override IActionResult Add() => NotFound();

        [HttpPost]
        public override IActionResult Add(SmsVM form) => NotFound();

        public override IActionResult Update(long id) => NotFound();

        [HttpPost]
        public override IActionResult Update(long id, SmsVM model) => NotFound();
    }
}
