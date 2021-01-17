using Hayalpc.Library.Common.Helpers;
using Hayalpc.Library.Common.Helpers.Interfaces;
using Hayalpc.Library.Log;
using Microsoft.Extensions.DependencyInjection;
using Hayalpc.Fatura.Panel.Internal.Helpers;
using Hayalpc.Fatura.Common.Helpers;

namespace Hayalpc.Fatura.Panel.Internal.Extensions
{
    public static class ScopeExtension
    {
        public static void AddScopes(this IServiceCollection services)
        {
            services.AddScoped<IHpLogger, NlogImpl>();
            services.AddScoped<ITokenCreator, TokenCreator>();
            services.AddScoped<Fatura.Panel.Internal.Helpers.Interfaces.IMailHelper, Fatura.Panel.Internal.Helpers.MailHelper>();
            services.AddScoped<Hayalpc.Library.Common.Helpers.Interfaces.IHtttpClientCreator, HttpClientCreator>();
            services.AddScoped<Hayalpc.Library.Common.Helpers.Interfaces.IHttpClientHelper, Library.Common.Helpers.HttpClientHelper>();
        }

    }
}
