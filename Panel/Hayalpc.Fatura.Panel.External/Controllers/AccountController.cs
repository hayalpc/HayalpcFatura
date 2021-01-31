using Hayalpc.Fatura.Common.Helpers.Interfaces;
using Hayalpc.Library.Log;
using Microsoft.AspNetCore.Mvc;
using Hayalpc.Fatura.Panel.External.Resources;
using Hayalpc.Fatura.Panel.External.Services.Interfaces;
using Hayalpc.Fatura.Panel.External.Models;
using Microsoft.Extensions.Caching.Memory;
using Hayalpc.Library.Common.Results;
using System.Collections.Generic;
using Hayalpc.Library.Common.Dtos;
using DevExtreme.AspNet.Mvc;
using Hayalpc.Library.Common.Helpers;

namespace Hayalpc.Fatura.Panel.External.Controllers
{
    public class AccountController : BaseController<UserVM, IUserService>
    {
        private readonly IMemoryCache memoryCache;

        public AccountController(LocService tr, ISessionHelper session, IHpLogger logger, IUserService userService, IMemoryCache memoryCache) : base(userService, tr, session, logger)
        {
            this.memoryCache = memoryCache;
        }

        public IActionResult Get()
        {
            return RedirectToAction(nameof(Profile));
        }

        public IActionResult Profile()
        {
            return View(new UserVM());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Profile(UserVM userVM)
        {
            return View(userVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ResetPassword(ChangePasswordVM changePassword)
        {
            if (ModelState.IsValid)
            {
                var currentPassword = Md5Helper.Get(RequestHelper.User.Email + ":" + changePassword.CurrentPassword);
                if (currentPassword == RequestHelper.User.Password)
                {
                    if (changePassword.NewPassword == changePassword.NewPasswordConfirm)
                    {
                        var result = service.Post<ChangePasswordVM, Result>("user/accountChangePassword", changePassword);
                        if (result.IsSuccess)
                        {
                            session.SetSuccessMessage("SuccessChangePassword");
                        }
                        else
                        {
                            session.SetErrorMessage(result.Message);
                        }
                    }
                    else
                    {
                        session.SetErrorMessage("NewPasswordNotEqual");
                    }
                }
                else
                {
                    session.SetErrorMessage("CurrentPasswordNotEqual");
                }
            }
            else
            {
                session.SetErrorMessage("InvalidResetPassword");
            }
            return RedirectToAction(nameof(Profile));
        }


        public IActionResult Bulletin()
        {
            service.Get<Result>("bulletin/readAll");
            memoryCache.Remove("UserBulletins-" + session.User.Id);
            service.LoadBulletins();

            return View();
        }

        [HttpPost]
        public IDataResult<List<UserBulletinDto>> Bulletin(DataSourceLoadOptions loadOptions)
        {

            return service.Post<DataSourceLoadOptions, DataResult<List<UserBulletinDto>>>("bulletin/search", loadOptions);
        }

    }
}
