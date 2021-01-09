using Hayalpc.Fatura.Data.Models;
using Hayalpc.Library.Repository;
using Hayalpc.Library.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Hayalpc.Fatura.Data
{
    public class Repository<Tentity> : HpRepository<Tentity, HpDbContext>, IRepository<Tentity>
        where Tentity : class, IHpModel
    {
        public Repository(HpDbContext context) : base(context)
        {
        }

        public TOEntity ExecSqlFirst<TOEntity>(string sql) where TOEntity : class, IHpModel
        {
            return context.Set<TOEntity>().FromSqlRaw(sql).FirstOrDefault();
        }

        public List<Tentity> ExecSql(string sql)
        {
            return context.Set<Tentity>().FromSqlRaw(sql).ToList();
        }
        public List<TQuery> ExecSqlQuery<TQuery>(string sql, params object[] parameters) where TQuery : class
        {
            return context.Set<TQuery>().FromSqlRaw(sql, parameters).ToList();
        }

    }
}
