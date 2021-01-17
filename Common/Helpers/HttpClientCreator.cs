using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Http;
using Hayalpc.Fatura.Common.Helpers.Interfaces;
using Hayalpc.Fatura.Common.Helpers;

namespace Hayalpc.Fatura.Common.Helpers
{
    public class HttpClientCreator : Library.Common.Helpers.Interfaces.IHtttpClientCreator
    {
        public HttpClientCreator()
        {
        }

        public HttpClient Create()
        {
            var client = new HttpClient();
            
            return client;
        }
    }
}
