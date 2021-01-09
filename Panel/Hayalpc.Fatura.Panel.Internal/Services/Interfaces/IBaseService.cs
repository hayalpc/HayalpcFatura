using DevExtreme.AspNet.Data;
using Hayalpc.Fatura.Data.Models;
using Hayalpc.Library.Common.Results;
using Hayalpc.Library.Repository.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Hayalpc.Fatura.Panel.Internal.Services.Interfaces
{
    public interface IBaseService<TEntity> where TEntity : IHpModel
    {
        IDataResult<TEntity> Get(long Id);
        TEntity BeforeGet(TEntity model);

        IDataResult<TEntity> Add(TEntity model);
        TEntity AfterAdd(TEntity model);
        IDataResult<TOEntity> AddCustom<TOEntity>(TOEntity model) where TOEntity : class, IHpModel;

        IDataResult<TEntity> Update(TEntity model);
        IDataResult<TEntity> Update(TEntity model, params string[] fields);
        IDataResult<TOEntity> UpdateCustom<TOEntity>(TOEntity model) where TOEntity : class, IHpModel;
        TEntity BeforeUpdate(TEntity model, TEntity data);
        TEntity AfterUpdate(TEntity model);

        IQueryable<TEntity> BeforeSearch(IQueryable<TEntity> req);
        IDataResult<IEnumerable<TEntity>> Search(DataSourceLoadOptionsBase req);
        IDataResult<IEnumerable<TOEntity>> Search<TOEntity>(DataSourceLoadOptionsBase req) where TOEntity : class, IHpModel;
        IResult AddRange<TOEntity>(IEnumerable<TOEntity> models) where TOEntity : class, IHpModel;
    }
}
