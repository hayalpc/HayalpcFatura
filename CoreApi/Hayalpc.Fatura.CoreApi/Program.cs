using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Hayalpc.Library.Common.Helpers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.WindowsServices;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Hayalpc.Fatura.CoreApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var isService = !(Debugger.IsAttached || args.Contains("--console"));

            bool isWindows = RuntimeInformation.IsOSPlatform(OSPlatform.Windows);

            var host = CreateHostBuilder(args).Build();

            if (isWindows && isService)
                host.RunAsService();
            else
                host.Run();
        }

        public static IWebHostBuilder CreateHostBuilder(string[] args)
        {
            var config = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: false).Build();

            var webSettings = new WebHostSettingHelper();
            config.GetSection("WebHostSettings").Bind(webSettings);

            var appConfig = new AppConfigHelper();
            config.GetSection("AppConfig").Bind(appConfig);

            var dbConfig = new DBConfigHelper();
            config.GetSection("DBConfig").Bind(dbConfig);

            var host = new WebHostBuilder()
            .UseKestrel(
            o =>
            {
                o.Limits.KeepAliveTimeout = TimeSpan.FromMinutes(15);
                o.Limits.RequestHeadersTimeout = TimeSpan.FromSeconds(30);
                o.Limits.MaxConcurrentConnections = webSettings.MaxConcurrentConnections;
                o.Limits.KeepAliveTimeout = TimeSpan.FromMinutes(2);
            })
            .ConfigureAppConfiguration((hostingContext, config) =>
            {
                config.SetBasePath(hostingContext.HostingEnvironment.ContentRootPath);
                config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                config.AddEnvironmentVariables(prefix: "ASPNETCORE_");
            })
            .ConfigureLogging(logging =>
            {
                logging.ClearProviders();
                logging.AddConsole();
                logging.AddDebug();
            })
            .UseIIS()
            .UseIISIntegration()
            .UseStartup<Startup>()
            .UseUrls(webSettings.Urls);
            //.UseNLog();

            return host;
        }
    }
}
