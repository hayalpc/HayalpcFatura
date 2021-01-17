using Hayalpc.Library.Common.Helpers;
using Hayalpc.Library.Common.Helpers.Interfaces;
using Hayalpc.Library.Log;
using Microsoft.Extensions.DependencyInjection;
using Hayalpc.Fatura.Panel.External.Helpers;
using Hayalpc.Fatura.Panel.External.Resources;

namespace Hayalpc.Fatura.Panel.External.Extensions
{
    public static class ScopeExtension
    {
        public static void AddScopes(this IServiceCollection services)
        {
            services.AddScoped<IHpLogger, NlogImpl>();
            services.AddScoped<IHtttpClientCreator, HtttpClientCreator>();
            services.AddScoped<IHttpClientHelper, HttpClientHelper>();
            services.AddScoped<Fatura.Common.Helpers.Interfaces.ISessionHelper, Fatura.Common.Helpers.SessionHelper>();

            services.AddScoped<IBlobStorageHelper, AzureBlobHelper>();

            services.AddSingleton<LocService>();
        }

    }
}
