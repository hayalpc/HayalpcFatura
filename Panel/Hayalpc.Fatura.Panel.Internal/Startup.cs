using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Hayalpc.Fatura.Data;
using Hayalpc.Fatura.Panel.Internal.Extensions;
using NLog;
using Hayalpc.Library.Common.Helpers;

namespace Hayalpc.Fatura.Panel.Internal
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScopes();
            services.AddRepositories();
            services.AddServices();

            services.AddConfigurations();

            services.AddJwtAuthentication();

            services.AddSwagger();

            services.AddAutoMapper(typeof(Startup));

            services.AddDbContextManager();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.AddUseSwagger("API v1.0.0");

            GlobalDiagnosticsContext.Set("connectionString", ConnectionManager.GetConnectionString());
            app.Use((context, next) =>
            {
                MappedDiagnosticsLogicalContext.Set("request_ip", RequestHelper.GetRealIp(context));
                MappedDiagnosticsLogicalContext.Set("trace_identifier", System.Diagnostics.Activity.Current.Id);
                return next();
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }
    }
}
