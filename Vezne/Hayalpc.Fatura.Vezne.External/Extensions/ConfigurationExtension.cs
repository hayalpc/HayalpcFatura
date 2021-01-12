using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;
using System;
using Microsoft.AspNetCore.Mvc;

namespace Hayalpc.Fatura.Vezne.External.Extensions
{
    public static class ConfigurationExtension
    {
        public static void AddConfigurations(this IServiceCollection services, IWebHostEnvironment Env)
        {
            services.AddOptions();

            services.AddHttpContextAccessor();

            services.AddResponseCompression();

            services.AddSession(opt =>
            {
                opt.IdleTimeout = TimeSpan.FromDays(1);
            });

            services.Configure<ForwardedHeadersOptions>(options =>
            {
                options.ForwardLimit = 2;
                options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
                options.KnownNetworks.Clear();
                options.KnownProxies.Clear();
            });


            services.AddControllersWithViews(options =>
            {
                options.EnableEndpointRouting = false;
                //options.Filters.Add(new AccessFilterAttribute());
            })
            .AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.None;
                options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
                //options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            })
            .AddViewLocalization()
            .SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

#if DEBUG
            services.AddRazorPages().AddRazorRuntimeCompilationIfDebug(Env);
#else
            services.AddRazorPages();
#endif
        }
    }
}
