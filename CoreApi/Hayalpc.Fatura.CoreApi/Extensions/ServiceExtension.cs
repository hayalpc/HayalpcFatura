using Hayalpc.Fatura.CoreApi.Services;
using Hayalpc.Fatura.CoreApi.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hayalpc.Fatura.CoreApi.Extensions
{
    public static class ServiceExtension
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IInstitutionService, InstitutionService>();
            services.AddScoped<IInvoiceService, InvoiceService>();
            services.AddScoped<IInvoicePaymentService, InvoicePaymentService>();
        }
    }
}
