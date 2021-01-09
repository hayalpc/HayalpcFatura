using Hayalpc.Fatura.Panel.External.Models;
using Hayalpc.Fatura.Panel.External.Resources;
using Hayalpc.Fatura.Panel.External.Services.Interfaces;
using Hayalpc.Library.Common;
using Hayalpc.Library.Common.Helpers.Interfaces;
using Hayalpc.Library.Common.Results;
using Hayalpc.Library.Log;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Hayalpc.Fatura.Panel.External.Controllers
{
    public class MerchantPaymentController : BaseController<MerchantPaymentVM, IMerchantPaymentService>
    {
        public MerchantPaymentController(IMerchantPaymentService service, LocService tr, ISessionHelper session, IHpLogger logger) : base(service, tr, session, logger)
        {
        }

        public IActionResult Calculate()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Calculate(MerchantPaymentCalculateVM calculateVM)
        {
            Task.Run(() => service.Post<long[], Result>(service.Path + "/calculate", calculateVM.MerchantId)).Forget();
            Thread.Sleep(100);
            session.SetSuccessMessage(tr.Get("CalculateRequestSuccess"));
            return RedirectToAction("calculate");
        }

        public override IActionResult Add() => NotFound();
        [HttpPost]
        public override IActionResult Add(MerchantPaymentVM model) => NotFound();
        public override IActionResult Update(long id) => NotFound();
        [HttpPost]
        public override IActionResult Update(long id, MerchantPaymentVM model) => NotFound();
    }
}
