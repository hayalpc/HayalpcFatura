using System.Collections.Generic;
using Hayalpc.Library.Common.Dtos;
using Hayalpc.Fatura.Common.Helpers.Interfaces;
using Hayalpc.Library.Common.Results;
using Hayalpc.Library.Log;
using Microsoft.AspNetCore.Mvc;
using Hayalpc.Fatura.Panel.External.Models;
using Hayalpc.Fatura.Panel.External.Resources;
using Hayalpc.Fatura.Panel.External.Services.Interfaces;
using Microsoft.Extensions.Caching.Memory;

namespace Hayalpc.Fatura.Panel.External.Controllers
{
    public class UserController : BaseController<UserVM, IUserService>
    {
        private readonly IMemoryCache memoryCache;

        public UserController(LocService tr, ISessionHelper session, IHpLogger logger, IUserService userService, IMemoryCache memoryCache) : base(userService,tr, session, logger)
        {
            this.memoryCache = memoryCache;
        }

        public override IActionResult Add()
        {
            var userViewModel = new UserVM();
            userViewModel.Roles = service.Get<List<RoleDto>>("role/getAll");
            return View("Form", userViewModel);
        }

        public IActionResult ResetPassword(long Id)
        {
            var res = service.Get<Result>("user/resetpassword/" + Id);
            if (res.IsSuccess)
                session.SetSuccessMessage("ResetPasswordSendSuccess");
            else
                session.SetErrorMessage("ResetPasswordSendError");
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Logout(long Id)
        {
            service.Get<Result>("user/logout/" + Id);
            return RedirectToAction(nameof(Detail), new { id = Id });
        }
    }
}
