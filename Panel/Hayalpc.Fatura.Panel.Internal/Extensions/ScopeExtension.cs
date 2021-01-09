using Hayalpc.Library.Common.Helpers;
using Hayalpc.Library.Common.Helpers.Interfaces;
using Hayalpc.Library.Log;
using Microsoft.Extensions.DependencyInjection;
using Hayalpc.Fatura.Panel.Internal.Helpers;

namespace Hayalpc.Fatura.Panel.Internal.Extensions
{
    public static class ScopeExtension
    {
        public static void AddScopes(this IServiceCollection services)
        {
            services.AddScoped<IHpLogger, NlogImpl>();
            services.AddScoped<ITokenCreator, TokenCreator>();
            services.AddScoped<IMailHelper, MailHelper>();
            services.AddScoped<IViewRenderHelper, ViewRenderHelper>();
        }

    }
}
