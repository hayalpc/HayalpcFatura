using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Hayalpc.Fatura.Common.Helpers.Interfaces;
using System.Linq;
using System.Net;
using Hayalpc.Library.Common.Extensions;
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
        private readonly IHpLogger logger;

        public AccessFilter(ISessionHelper sessionHelper, Library.Common.Helpers.Interfaces.IHttpClientHelper clientHelper, IHpLogger logger)
        {
            this.sessionHelper = sessionHelper;
            this.clientHelper = clientHelper;
            this.logger = logger;
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