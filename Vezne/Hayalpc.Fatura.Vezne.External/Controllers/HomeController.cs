using Hayalpc.Fatura.Vezne.External.Models;
using Hayalpc.Library.Common.Helpers;
using Hayalpc.Library.Common.Helpers.Interfaces;
using Hayalpc.Library.Common.Models;
using Hayalpc.Library.Log;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Hayalpc.Fatura.Vezne.External.Controllers
{
    public class HomeController : Controller
    {
        protected readonly IHpLogger logger;
        protected readonly IHttpClientHelper clientHelper;
        protected readonly ISessionHelper session;

        public HomeController(IHpLogger logger, IHttpClientHelper clientHelper, ISessionHelper session)
        {
            this.logger = logger;
            this.clientHelper = clientHelper;
            this.session = session;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Index(string type)
        {
            var reCaptcha = HttpContext.Request.Form["g-recaptcha-response"];
            var ReCaptchaSecret = AppConfigHelper.GoogleSecretKey;
            var reCaptchaResponse = clientHelper.Get<GoogleReCaptchaResponse>("https://www.google.com", $"/recaptcha/api/siteverify?secret={ReCaptchaSecret}&response={reCaptcha}");
            if (!reCaptchaResponse.Success)
            {
                session.SetErrorMessage("Doğrulamanız yapılamadı. Lütfen başka bir tarayıcıdan deneyiniz");
            }
            else
            {
                session.Set("Authenticated", "1");
            }
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
