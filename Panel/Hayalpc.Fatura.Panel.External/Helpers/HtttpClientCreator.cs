using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Http;
using Hayalpc.Fatura.Common.Helpers.Interfaces;
using Hayalpc.Library.Common.Helpers;

namespace Hayalpc.Fatura.Panel.External.Helpers
{
    public class HtttpClientCreator : Library.Common.Helpers.Interfaces.IHtttpClientCreator
    {
        private readonly ISessionHelper sessionHelper;
        private readonly IHttpContextAccessor httpContextAccessor;

        public HtttpClientCreator(ISessionHelper sessionHelper, IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.sessionHelper = sessionHelper;
        }

        public HttpClient Create()
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessionHelper.JwtToken);
            
            client.DefaultRequestHeaders.Add("SessionId", httpContextAccessor.HttpContext.Session.Id);

            client.DefaultRequestHeaders.Add("RemoteIp", httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString());
            client.DefaultRequestHeaders.Add("RemoteIp", httpContextAccessor.HttpContext.Connection.RemotePort.ToString());
            client.DefaultRequestHeaders.Add("UserAgent", httpContextAccessor.HttpContext.Request.Headers["User-Agent"].ToString());
            
            return client;
        }
    }
}
