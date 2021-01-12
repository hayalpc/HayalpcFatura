using Hayalpc.Library.Common.Helpers;
using Hayalpc.Library.Common.Helpers.Interfaces;
using Hayalpc.Library.Log;
using Microsoft.Extensions.DependencyInjection;
using Hayalpc.Fatura.Vezne.External.Helpers;

namespace Hayalpc.Fatura.Vezne.External.Extensions
{
    public static class ScopeExtension
    {
        public static void AddScopes(this IServiceCollection services)
        {
            services.AddScoped<IHpLogger, NlogImpl>();
            services.AddScoped<IHtttpClientCreator, HtttpClientCreator>();
            services.AddScoped<IHttpClientHelper, HttpClientHelper>();
            services.AddScoped<ISessionHelper, SessionHelper>();
        }

    }
}
