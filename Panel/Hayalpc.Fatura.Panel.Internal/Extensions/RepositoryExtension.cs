using Microsoft.Extensions.DependencyInjection;
using Hayalpc.Fatura.Data;
using Hayalpc.Fatura.Data.Models;

namespace Hayalpc.Fatura.Panel.Internal.Extensions
{
    public static class RepositoryExtension
    {
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IRepository<Category>, Repository<Category>>();
            services.AddScoped<IRepository<Country>, Repository<Country>>();
            services.AddScoped<IRepository<City>, Repository<City>>();
            services.AddScoped<IRepository<District>, Repository<District>>();
            services.AddScoped<IRepository<User>, Repository<User>>();
            services.AddScoped<IRepository<Role>, Repository<Role>>();
            services.AddScoped<IRepository<UserRole>, Repository<UserRole>>();
            services.AddScoped<IRepository<RolePermission>, Repository<RolePermission>>();
            services.AddScoped<IRepository<ResetPassword>, Repository<ResetPassword>>();
            services.AddScoped<IRepository<TableDefinition>, Repository<TableDefinition>>();
            services.AddScoped<IRepository<TableHistory>, Repository<TableHistory>>();
            services.AddScoped<IRepository<UserBulletin>, Repository<UserBulletin>>();
            services.AddScoped<IRepository<UserLog>, Repository<UserLog>>();
            services.AddScoped<IRepository<BlobFile>, Repository<BlobFile>>();

            services.AddScoped<IRepository<Dealer>, Repository<Dealer>>();
            services.AddScoped<IRepository<Institution>, Repository<Institution>>();
            services.AddScoped<IRepository<Invoice>, Repository<Invoice>>();
            services.AddScoped<IRepository<InvoicePayment>, Repository<InvoicePayment>>();
        }
    }
}
