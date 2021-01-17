using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hayalpc.Fatura.Panel.Internal.Controllers
{
    [Route("[controller]/[action]")]
    [AllowAnonymous]
    public class RenderController : Controller
    {
        public IActionResult ResetPassword()
        {
            return View();
        }
    }
}
