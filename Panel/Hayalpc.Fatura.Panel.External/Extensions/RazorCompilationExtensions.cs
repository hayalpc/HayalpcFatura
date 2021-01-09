using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
#if DEBUG
using Microsoft.Extensions.Hosting;
#endif

namespace Hayalpc.Fatura.Panel.External.Extensions
{
    public static class RazorCompilationExtensions
    {
        public static IMvcBuilder AddRazorRuntimeCompilationIfDebug(this IMvcBuilder builder, IWebHostEnvironment Env)
        {
#if DEBUG
            if (Env.IsDevelopment())
                builder.AddRazorRuntimeCompilation();
#endif
            return builder;
        }
    }
}
