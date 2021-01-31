using Hayalpc.Fatura.Common.Helpers.Interfaces;
using Hayalpc.Library.Log;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Hayalpc.Fatura.Panel.External.Resources;
using System.Collections.Generic;
using System;
using Hayalpc.Fatura.Panel.External.Services;
using Hayalpc.Library.Common.Results;
using Hayalpc.Fatura.Panel.External.Models;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using DevExtreme.AspNet.Data.ResponseModel;

namespace Hayalpc.Fatura.Panel.External.Controllers
{
    [Authorize]
    public class BaseController<TEntityVM,TService> : Controller
        where TEntityVM : BaseVM, new()
        where TService : IBaseService<TEntityVM>
    {
        protected readonly TService service;
        protected readonly LocService tr;
        protected readonly ISessionHelper session;
        protected readonly IHpLogger logger;

        public BaseController(TService service, LocService tr, ISessionHelper session, IHpLogger logger) 
        {
            this.tr = tr;
            this.session = session;
            this.logger = logger;
            this.service = service;
        }

        public virtual IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public virtual DataResult<List<TEntityVM>> Index(DataSourceLoadOptions loadOptions)
        {
            return service.Post<DataSourceLoadOptions, DataResult<List<TEntityVM>>>(service.Path + "/search", loadOptions);
        }

        public virtual IActionResult Detail(long id)
        {
            var modelRes = service.Get(id);
            if (modelRes.IsSuccess)
                return View(modelRes.Data);
            else
            {
                session.SetErrorMessage("RecordNotFound");
                return RedirectToAction(nameof(Index));
            }
        }

        public virtual IActionResult Add()
        {
            var model = new TEntityVM();
            return View("Form", model);
        }

        public virtual TEntityVM BeforeAdd(TEntityVM model)
        {
            return model;
        }

        public virtual TEntityVM AfterAdd(TEntityVM model)
        {
            return model;
        }

        [HttpPost]
        public virtual IActionResult Add(TEntityVM form)
        {
            form = BeforeAdd(form);
            if (ModelState.IsValid)
            {
                var modelRes = service.Post<TEntityVM, DataResult<TEntityVM>>(service.Path + "/add", form);
                if (modelRes.IsSuccess)
                {
                    AfterAdd(modelRes.Data);
                    session.SetSuccessMessage(modelRes.Message);
                    return RedirectToAction(nameof(Index));
                }
                else
                    session.SetErrorMessage(modelRes.Message);
            }
            else
                session.SetErrorMessage("PleaseFixErrors");
            return View("Form", form);
        }

        public virtual IActionResult Update(long id)
        {
            var modelRes = service.Get(id);
            if (modelRes.IsSuccess)
                return View("Form", modelRes.Data);
            else
            {
                session.SetErrorMessage("RecordNotFound");
                return RedirectToAction(nameof(Index));
            }
        }

        public virtual TEntityVM BeforeUpdate(long id, TEntityVM model)
        {
            return model;
        }
        public virtual TEntityVM AfterUpdate(TEntityVM model)
        {
            return model;
        }

        [HttpPost]
        public virtual IActionResult Update(long id, TEntityVM model)
        {
            model = BeforeUpdate(id, model);
            if (ModelState.IsValid)
            {
                var modelUp = service.Update(id,model);
                if (modelUp.IsSuccess)
                {
                    AfterUpdate(modelUp.Data);
                    session.SetSuccessMessage(modelUp.Message);
                    return RedirectToAction(nameof(Index));
                }
                else
                    session.SetErrorMessage(modelUp.Message);
            }
            else
                session.SetErrorMessage("PleaseFixErrors");
            return View("Form", model);
        }
    }
}
