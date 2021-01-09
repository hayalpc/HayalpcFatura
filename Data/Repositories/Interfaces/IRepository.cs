using Hayalpc.Library.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Hayalpc.Fatura.Data
{
    public interface IRepository<Tentity> : IHpRepository<Tentity,HpDbContext>
        where Tentity :class, IHpModel
    {
        TOEntity ExecSqlFirst<TOEntity>(string sql) where TOEntity : class, IHpModel;
        List<Tentity> ExecSql(string sql);
        List<TQuery> ExecSqlQuery<TQuery>(string sql, params object[] parameters) where TQuery : class;
    }
}
