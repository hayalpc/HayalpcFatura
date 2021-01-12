using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;
using Hayalpc.Fatura.Panel.External.Resources;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using Hayalpc.Fatura.Panel.External.Filters;
using Microsoft.AspNetCore.Mvc;

namespace Hayalpc.Fatura.Panel.External.Extensions
{
    public static class ConfigurationExtension
    {
        public static void AddConfigurations(this IServiceCollection services, IWebHostEnvironment Env)
        {
            services.AddOptions();

            services.AddHttpContextAccessor();

            services.AddLocalization(options => options.ResourcesPath = "Resources");

            services.AddResponseCompression();

            services.AddSession(opt =>
            {
                opt.IdleTimeout = TimeSpan.FromDays(1);
            });

            services.Configure<RequestLocalizationOptions>(options =>
            {
                var supportedCultures = new List<CultureInfo> { new CultureInfo("tr"), new CultureInfo("en") };

                options.DefaultRequestCulture = new RequestCulture(culture: "tr", uiCulture: "tr");
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;

                options.RequestCultureProviders = new List<IRequestCultureProvider>{
                    new QueryStringRequestCultureProvider(),
                    new CookieRequestCultureProvider()
                };

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
                options.AddInitialRequestCultureProvider(new CustomRequestCultureProvider(async (context) =>
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
                {
                    var requestCulture = new ProviderCultureResult("tr");
                    return requestCulture;
                }));
            });

            services.Configure<ForwardedHeadersOptions>(options =>
            {
                options.ForwardLimit = 2;
                options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
                options.KnownNetworks.Clear();
                options.KnownProxies.Clear();
            });

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
            {
                options.Cookie.HttpOnly = true;
                options.Cookie.SameSite = SameSiteMode.Lax;
                options.Cookie.SecurePolicy = CookieSecurePolicy.None;
                //options.Cookie.SecurePolicy = Env.IsDevelopment() ? CookieSecurePolicy.None : CookieSecurePolicy.Always;

                options.SlidingExpiration = true;
                options.ExpireTimeSpan = new TimeSpan(24, 0, 0);

                options.LoginPath = "/login";
            });

            services.AddControllersWithViews(options =>
            {
                options.EnableEndpointRouting = false;
                options.Filters.Add(new AccessFilterAttribute());
            })
            .AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.None;
                options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
                //options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            })
            .AddViewLocalization()
            .AddDataAnnotationsLocalization(options =>
            {
                options.DataAnnotationLocalizerProvider = (type, factory) =>
                {
                    var assemblyName = new AssemblyName(typeof(SharedResource).GetTypeInfo().Assembly.FullName);
                    return factory.Create("SharedResource", assemblyName.Name);
                };
            })
            .SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
#if DEBUG
            services.AddRazorPages().AddRazorRuntimeCompilationIfDebug(Env);
#else
            services.AddRazorPages();
#endif
        }
    }
}
