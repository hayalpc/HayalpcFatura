using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Hayalpc.Library.Common.Helpers.Interfaces;
using System.Linq;
using System.Net;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Security.Claims;
using Hayalpc.Fatura.Panel.Internal.Services.Interfaces;
using Hayalpc.Library.Log;
using Hayalpc.Library.Common.Dtos;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http.Extensions;
using Hayalpc.Fatura.Common.Helpers;

namespace Hayalpc.Fatura.Panel.Internal.Filters
{
    public class AccessFilter : IActionFilter
    {
        private readonly IUserService userService;
        private readonly IHpLogger logger;

        public AccessFilter(IUserService userService, IHpLogger logger)
        {
            this.userService = userService;
            this.logger = logger;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.HttpContext.Request.Path != "/api/user/validate")
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

            RequestHelper.Host = context.HttpContext.Request.Host.Value.ToLower();
            RequestHelper.RemoteIp = context.HttpContext.Connection.RemoteIpAddress.ToString();

            RequestHelper.Controller = ((string)info.ControllerName).ToLower();
            RequestHelper.Action = ((string)info.ActionName).ToLower();
            RequestHelper.Path = context.HttpContext.Request.Path.ToString().ToLower();

            //RequestHelper.Referer = context.HttpContext.Request.Headers["Referer"].ToString();
            RequestHelper.RemoteIp = context.HttpContext.Request.Headers["RemoteIp"].ToString();
            RequestHelper.RemotePort = context.HttpContext.Request.Headers["RemotePort"].ToString();
            RequestHelper.UserAgent = context.HttpContext.Request.Headers["UserAgent"].ToString();
            RequestHelper.SessionId = context.HttpContext.Request.Headers["SessionId"].ToString();

            long.TryParse(context.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier), out long userId);
            if (userId > 0)
            {
                var user = userService.GetFromCache(userId);
                if (user != null)
                {
                    RequestHelper.User = user;
                    RequestHelper.UserId = RequestHelper.User.Id;
                    RequestHelper.DealerId = user.DealerId ?? 0;
                }
            }

            if (context.HttpContext.Request.Path != "/api/user/validate")
            {
                var RequestBody = ReadBodyAsString(context.HttpContext.Request);
                var obj = new
                {
                    Type = "OnActionExecuting",
                    Url = context.HttpContext.Request.GetDisplayUrl(),
                    RequestHelper.SessionId,
                    context.HttpContext.TraceIdentifier,
                    //RequestHelper.Referer,
                    RequestHelper.UserAgent,
                    context.HttpContext.Request.Method,
                    RequestHelper.RemoteIp,
                    RequestHelper.RemotePort,
                    RequestHelper.DealerId,
                    RequestHelper.UserId,
                    RequestBody,
                };

                logger.Info(obj);
            }
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