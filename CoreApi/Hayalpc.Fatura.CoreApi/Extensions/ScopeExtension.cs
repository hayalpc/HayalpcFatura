using Hayalpc.Library.Log;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hayalpc.Fatura.CoreApi.Extensions
{
    public static class ScopeExtension
    {
        public static void AddScopes(this IServiceCollection services)
        {
            //services.AddScoped<IHpLogger, NlogImpl>();
        }

    }
}
