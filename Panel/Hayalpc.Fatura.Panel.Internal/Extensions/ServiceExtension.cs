using Microsoft.Extensions.DependencyInjection;
using Hayalpc.Fatura.Panel.Internal.Services;
using Hayalpc.Fatura.Panel.Internal.Services.Interfaces;

namespace Hayalpc.Fatura.Panel.Internal.Extensions
{
    public static class ServiceExtension
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<ICategoryService, CategoryService>();

            services.AddScoped<IUserService, UserService>();

            services.AddScoped<IRoleService, RoleService>();

            services.AddScoped<IUserRoleService, UserRoleService>();

            services.AddScoped<IRolePermissionService, RolePermissionService>();

            services.AddScoped<IResetPasswordService, ResetPasswordService>();
            services.AddScoped<IMerchantService, MerchantService>();
            services.AddScoped<IServiceService, ServiceService>();
            services.AddScoped<ITransactionService, TransactionService>();
            services.AddScoped<ISubscriptionService, SubscriptionService>();

            services.AddScoped<ITableDefinitionService, TableDefinitionService>();
            services.AddScoped<ITableHistoryService, TableHistoryService>();

            services.AddScoped<ISmsService, SmsService>();

            services.AddScoped<IUserBulletinService, UserBulletinService>();

            services.AddScoped<IParametersService, ParametersService>();

            services.AddScoped<IBlobFileService, BlobFileService>();

            services.AddScoped<ITariffService, TariffService>();

            services.AddScoped<ICarrierCollectionService, CarrierCollectionService>();
            services.AddScoped<ICarrierCollectionItemService, CarrierCollectionItemService>();
            services.AddScoped<IMerchantPaymentService, MerchantPaymentService>();
        }
    }
}
