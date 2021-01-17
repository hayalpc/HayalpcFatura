using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Hayalpc.Fatura.Common.Helpers.Interfaces;
using Hayalpc.Library.Common.Models;
using Hayalpc.Library.Common.Results;
using Hayalpc.Library.Log;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Hayalpc.Fatura.Panel.External.Resources;
using Hayalpc.Fatura.Panel.External.Services.Interfaces;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using Azure.Storage.Blobs;
using System.IO;
using Hayalpc.Fatura.Common.Helpers;
using Azure.Storage;
using System.Security.Cryptography;
using System.Threading;

namespace Hayalpc.Fatura.Panel.External.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUserService userService;
        private readonly ISessionHelper session;
        private readonly IHpLogger logger;
        private readonly LocService tr;
        private readonly IMemoryCache memoryCache;

        public HomeController(IUserService userService, ISessionHelper session, IHpLogger logger, LocService tr, IMemoryCache memoryCache)
        {
            this.userService = userService;
            this.session = session;
            this.logger = logger;
            this.tr = tr;
            this.memoryCache = memoryCache;
        }

        public IActionResult Index()
        {
            //userService.LoadBulletins();
            //userService.LoadUserData();
            return View();
        }

        [HttpGet("/login")]
        [AllowAnonymous]
        public IActionResult Login([FromQuery] string code = null)
        {
            session.Set("login", "1");
            var request = new LoginRequest();

            if (!string.IsNullOrWhiteSpace(code))
                ViewData["login.code"] = code;

            return View(request);
        }

        [HttpPost("/login")]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginRequest request, [FromQuery] string ReturnUrl = null)
        {
            request.SessionId = session.SessionId;
            var loginResult = userService.Login(request);
            if (loginResult.IsSuccess)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Email, loginResult.Data.User.Email),
                    new Claim(ClaimTypes.Hash, loginResult.Data.JwtToken),
                };

                var userIdentity = new ClaimsIdentity(claims, "login");

                ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);

                session.User = loginResult.Data.User;

                memoryCache.Set("UserBulletins-" + session.User.Id, session.User.Bulletins, TimeSpan.FromMinutes(5));

                session.JwtToken = loginResult.Data.JwtToken;
                session.Permissions = loginResult.Data.Permissions;

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                if (ReturnUrl != null)
                {
                    return Redirect(ReturnUrl);
                }
                else
                {
                    return Redirect("/");
                }
            }
            else
            {
                session.SetErrorMessage(loginResult.Message);
                return View(request);
            }
        }

        [HttpGet("/logout")]
        [AllowAnonymous]
        public async Task<IActionResult> Logout(string code = null)
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            session.Clear();
            foreach (var cookie in HttpContext.Request.Cookies)
            {
                Response.Cookies.Delete(cookie.Key);
            }
            return Redirect("/login" + (code != null ? "?code=" + code : ""));
        }


        [HttpGet("/create-password/{token}")]
        [AllowAnonymous]
        public IActionResult CreatePassword(Guid token)
        {
            return View();
        }

        [HttpPost("/create-password/{token}")]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public IActionResult CreatePassword(Guid token, PasswordRequest passwordRequest)
        {
            if (ModelState.IsValid)
            {
                passwordRequest.Token = token;
                var res = userService.Post<PasswordRequest, Result>("user/updatePassword", passwordRequest);
                if (res.IsSuccess)
                {
                    session.SetSuccessMessage("CreatePasswordSuccess");
                    return RedirectToAction(nameof(Login));
                }
                else
                    session.SetErrorMessage(res.Message);
            }
            return View(passwordRequest);
        }

        [HttpGet("/forgot-password")]
        [AllowAnonymous]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost("/forgot-password")]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public IActionResult ForgotPassword(PasswordForgotRequest request)
        {
            if (ModelState.IsValid)
            {
                var res = userService.Get<Result>("user/forgotPassword?email=" + request.Email);
                if (res.IsSuccess)
                {
                    session.SetSuccessMessage("ForgotPasswordSuccess");
                    return RedirectToAction(nameof(Login));
                }
                else
                    session.SetErrorMessage(res.Message);
            }
            return View();
        }

        [AllowAnonymous]
        public IActionResult Privacy()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpGet("/avatar.jpg")]
        public IActionResult UserImage()
        {
            try
            {
                if (session.IsAuthenticated && session.User != null && session.User.ImagePath != null && session.User.ImagePath.Length > 0)
                {
                    var list = new string[] { };
                    list = session.User.ImagePath.Split(";");
                    if (list.Length > 0)
                    {
                        var bytes = Convert.FromBase64String(list[1]);
                        return File(bytes, list[0]);
                    }
                }
            }
            catch (Exception)
            {
            }
            return File("~/assets/img/no-avatar.png", "image/jpeg");
        }

        [AllowAnonymous]
        [HttpGet("/change-language/{culture}")]
        public IActionResult SetLanguage(string culture)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
            );
            return Ok();
        }

        [AllowAnonymous]
        public IActionResult Check()
        {
            for(var i = 0; i < 10; i++)
            {
                Thread.Sleep(500);
                logger.Debug(RequestHelper.RemoteIp + " " + i);
            }
            return Ok();
        }

    }
}
