using Hayalpc.Fatura.Panel.External.Models;
using Hayalpc.Library.Common.Helpers;
using Hayalpc.Library.Common.Helpers.Interfaces;
using Hayalpc.Library.Common.Results;
using Hayalpc.Library.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hayalpc.Fatura.Panel.External.Services
{
    public class BaseService<TentityVM> : IBaseService<TentityVM> where TentityVM : BaseVM, new()
    {
        public string Path { get; set; }

        protected readonly IHttpClientHelper clientHelper;
        protected readonly IHpLogger logger;


        public BaseService(IHttpClientHelper clientHelper, IHpLogger logger, string path)
        {
            this.clientHelper = clientHelper;
            this.logger = logger;
            this.Path = path;
        }

        public TResponse Delete<TResponse>(string method) => clientHelper.Delete<TResponse>(AppConfigHelper.ApiUrl, method);

        public TResponse Put<TRequest, TResponse>(string method, TRequest request) => clientHelper.Put<TRequest, TResponse>(AppConfigHelper.ApiUrl, method, request);

        public TResponse Put<TRequest, TResponse>(string method, TRequest request, TimeSpan timeout) where TResponse : class, new()
        {
            try
            {
                clientHelper.SetTimeout(timeout);
                return clientHelper.Put<TRequest, TResponse>(AppConfigHelper.ApiUrl, method, request);
            }
            catch (Exception)
            {
                return new TResponse();
            }
        }

        public TResponse Post<TRequest, TResponse>(string method, TRequest request) => clientHelper.Post<TRequest, TResponse>(AppConfigHelper.ApiUrl, method, request);

        public TResponse Post<TRequest, TResponse>(string method, TRequest request, TimeSpan timeout) where TResponse : class, new()
        {
            try
            {
                clientHelper.SetTimeout(timeout);
                return clientHelper.Post<TRequest, TResponse>(AppConfigHelper.ApiUrl, method, request);
            }
            catch (Exception)
            {
                return new TResponse();
            }
        }

        public TResponse Get<TResponse>(string method) => clientHelper.Get<TResponse>(AppConfigHelper.ApiUrl, method);

        public TResponse Get<TResponse>(string method, TimeSpan timeout) where TResponse : class, new()
        {
            try
            {
                clientHelper.SetTimeout(timeout);
                return clientHelper.Get<TResponse>(AppConfigHelper.ApiUrl, method);
            }
            catch (Exception)
            {
                return new TResponse();
            }
        }

        public virtual IDataResult<TentityVM> Get(long id)
        {
            return Get<DataResult<TentityVM>>(this.Path + "/get/" + id);
        }

        public virtual IDataResult<TentityVM> Update(long id, TentityVM model)
        {
            try
            {
                model.Id = id;
                return Put<IBaseVM, DataResult<TentityVM>>(this.Path + "/update", model);
            }
            catch (Exception exp)
            {
                logger.Error(exp.ToString());
                return new ErrorDataResult<TentityVM>(500, exp.Message);
            }
        }
    }
}
