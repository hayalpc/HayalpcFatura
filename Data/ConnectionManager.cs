using Hayalpc.Library.Common.Helpers;
using Hayalpc.Library.Repository;
using Hayalpc.Library.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Runtime.CompilerServices;
using System.Text;

namespace Hayalpc.Fatura.Data
{
    public static class ConnectionManager
    {
        private static string key = "C1437B6D9F0B65F9A49E1DD43F94D6DF4C89695708B3B915A12D4B4831EE6FC3";

        public static string GetConnectionString()
        {
            //var address = AesEncryptionHelper.DecryptToAscii(DBConfigHelper.Address, key);
            var address = (DBConfigHelper.Address);
            var name = (DBConfigHelper.DbName);
            var username = (DBConfigHelper.Username);
            var password = (DBConfigHelper.Password);

            var conStr = $"Server={address};Host={address};Database={name};User Id={username};Password={password};";
            return conStr;
        }

        public static void AddDbContextManager(this IServiceCollection services)
        {
            services.AddDbContext<HpDbContext>(options => options.UseNpgsql(GetConnectionString()));
            services.AddScoped<IHpUnitOfWork<HpDbContext>, HpUnitOfWork>();
        }

        public static void SetKey(string strKey)
        {
            key = strKey;
        }
    }
}
