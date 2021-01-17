using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Hayalpc.Fatura.Common.Helpers.Interfaces;
using System.Linq;
using System.Net;
using Hayalpc.Library.Common.Extensions;
using Hayalpc.Fatura.Panel.External.Resources;
using Hayalpc.Library.Log;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using System.IO;
using System;
using Hayalpc.Fatura.Common.Helpers;

namespace Hayalpc.Fatura.Panel.External.Filters
{
    public class AccessFilter : IActionFilter
    {
        private readonly ISessionHelper sessionHelper;
        private readonly Library.Common.Helpers.Interfaces.IHttpClientHelper clientHelper;
        private readonly LocService tr;
        private readonly IHpLogger logger;

        public AccessFilter(ISessionHelper sessionHelper, Library.Common.Helpers.Interfaces.IHttpClientHelper clientHelper, LocService tr, IHpLogger logger)
        {
            this.sessionHelper = sessionHelper;
            this.clientHelper = clientHelper;
            this.logger = logger;
            this.tr = tr;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.HttpContext.Request.Path != "/api/User/Validate")
            {
                var obj = new
                {
                    Type = "OnActionExecuted",
                    RequestHelper.SessionId,
                    context.HttpContext.TraceIdentifier,
                    context.HttpContext.Response.StatusCode,
                };
                logger.Info(obj);
            }
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            dynamic info = context.ActionDescriptor;

            RequestHelper.Host = context.HttpContext.Request.Host.Value.ToLowerInvariant();
            RequestHelper.RemoteIp = RequestHelper.GetRealIp(context.HttpContext);

            RequestHelper.Controller = ((string)info.ControllerName).ToLowerInvariant();
            RequestHelper.Action = ((string)info.ActionName).ToLowerInvariant();
            RequestHelper.Path = context.HttpContext.Request.Path.ToString().ToLowerInvariant();
            RequestHelper.Referer = context.HttpContext.Request.Headers["Referer"].ToString();
            RequestHelper.UserAgent = context.HttpContext.Request.Headers["User-Agent"].ToString();
            RequestHelper.SessionId = context.HttpContext.Session.Id;
            RequestHelper.LocService = tr;

            if (!context.Filters.Any(x => x.GetType() == typeof(Microsoft.AspNetCore.Mvc.Authorization.AllowAnonymousFilter)))
            {
                if (sessionHelper.IsAuthenticated)
                {
                    RequestHelper.User = sessionHelper.User;
                    RequestHelper.UserId = sessionHelper.User.Id;
                    RequestHelper.DealerId = sessionHelper.User.DealerId ?? 0;
                    if (sessionHelper.Permissions.Count > 0)
                    {
                        if (context.HttpContext.Request.Path != "/")
                        {
                            var path = context.HttpContext.Request.Path.ToString().Substring(1, context.HttpContext.Request.Path.ToString().Length - 1).Split("/");

                            if (path.Length > 1 && !sessionHelper.HasPermission(path[1], path[0]))
                            {
                                if (path.Length > 2 && long.TryParse(path[2], out long xxx))
                                    RequestHelper.ModelId = path[2];
                                context.Result = new RedirectResult("/error/403");
                                return;
                            }
                            else if (path.Length == 1 && !sessionHelper.HasPermission("", path[0]))
                            {
                                context.Result = new RedirectResult("/error/403");
                                return;
                            }

                            var controller = context.Controller as Controller;

                            if (path.Length > 1)
                            {
                                switch (path[1])
                                {
                                    case "":
                                        controller.ViewBag.Title = tr.Get(RequestHelper.Controller.ToLowerInvariant().Ucfirst() + "s");
                                        break;
                                    case "detail":
                                        controller.ViewBag.Title = tr.Get(RequestHelper.Controller.ToLowerInvariant().Ucfirst() + "Detail");
                                        break;
                                    case "update":
                                        controller.ViewBag.Title = tr.Get(RequestHelper.Controller.ToLowerInvariant().Ucfirst() + "Update");
                                        break;
                                    case "add":
                                        controller.ViewBag.Title = tr.Get(RequestHelper.Controller.ToLowerInvariant().Ucfirst() + "Add");
                                        break;
                                    default:
                                        controller.ViewBag.Title = tr.Get(path[1].ToLowerInvariant().Ucfirst());
                                        break;
                                }
                            }
                            else
                            {
                                controller.ViewBag.Title = tr.Get(RequestHelper.Controller.ToLowerInvariant().Ucfirst() + "s");
                            }
                        }
                        if (RequestHelper.Action == "index" && context.HttpContext.Request.Method == "POST")
                        {
                            //continue;
                        }
                        else
                        {
                            var result = clientHelper.UserValidate(Library.Common.Helpers.AppConfigHelper.ApiUrl, "user/validate");
                            if (result.StatusCode == HttpStatusCode.OK)
                                return;
                            else if (result.StatusCode == HttpStatusCode.Unauthorized)
                                context.Result = new RedirectResult("/logout?code=newSession");
                            else
                                context.Result = new RedirectResult("/logout?code=invalidSession");
                        }
                    }
                    else
                        context.Result = new RedirectResult("/logout?code=forbiddenSession");
                }
                else
                    context.Result = new RedirectResult("/logout" + (context.HttpContext.Request.Path != "/" ? "?RedirectUrl=" + context.HttpContext.Request.Path : ""));
            }

            //var RequestBody = ReadBodyAsString(context.HttpContext.Request);
            var headerStr = context.HttpContext.Request.Headers.Aggregate("", (current, header) => current + $"{header.Key}: {header.Value}{Environment.NewLine}");
            var obj = new
            {
                Type = "OnActionExecuting",
                Url = context.HttpContext.Request.GetDisplayUrl(),
                RequestHelper.SessionId,
                context.HttpContext.TraceIdentifier,
                context.HttpContext.Request.Path,
                RequestHelper.Referer,
                RequestHelper.UserAgent,
                context.HttpContext.Request.Method,
                RequestHelper.RemoteIp,
                RequestHelper.RemotePort,
                RequestHelper.DealerId,
                RequestHelper.UserId,
                Header = headerStr
                //RequestBody,
            };
            logger.Info(obj);

        }

        private string ReadBodyAsString(HttpRequest request)
        {
            var initialBody = request.Body;
            try
            {
                request.EnableBuffering();
                using (StreamReader reader = new StreamReader(request.Body))
                {
                    return reader.ReadToEnd();
                }
            }
            finally
            {
                request.Body = initialBody;
            }
        }

        
    }
}