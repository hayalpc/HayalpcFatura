using Hayalpc.Fatura.Panel.External.Models;
using Hayalpc.Library.Common.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hayalpc.Fatura.Panel.External.Services
{
    public interface IBaseService<TentityVM> where TentityVM : BaseVM, new()
    {
        string Path { get; set; }
        TResponse Delete<TResponse>(string method);
        TResponse Put<TRequest, TResponse>(string method, TRequest request);
        TResponse Put<TRequest, TResponse>(string method, TRequest request, TimeSpan timeout) where TResponse : class, new();
        TResponse Post<TRequest, TResponse>(string method, TRequest request);
        TResponse Post<TRequest, TResponse>(string method, TRequest request, TimeSpan timeout) where TResponse : class, new();
        TResponse Get<TResponse>(string method);
        TResponse Get<TResponse>(string method, TimeSpan timeout) where TResponse : class, new();

        IDataResult<TentityVM> Get(long id);

        IDataResult<TentityVM> Update(long id, TentityVM model);
    }
}
