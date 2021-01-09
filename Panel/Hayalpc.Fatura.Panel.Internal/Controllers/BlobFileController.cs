using Hayalpc.Library.Common.Results;
using Microsoft.AspNetCore.Mvc;
using Hayalpc.Fatura.Data.Models;
using Hayalpc.Fatura.Panel.Internal.Services.Interfaces;
using System.Collections.Generic;
using DevExtreme.AspNet.Data;
using System;

namespace Hayalpc.Fatura.Panel.Internal.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BlobFileController : ControllerBase
    {
        private readonly IBlobFileService service;

        public BlobFileController(IBlobFileService service)
        {
            this.service = service;
        }

        [HttpGet("{id}")]
        public IDataResult<BlobFile> Get(long Id)
        {
            return service.Get(Id);
        }

        [HttpGet("{guid}")]
        public IDataResult<BlobFile> GetByGuid(Guid guid)
        {
            return service.GetByGuid(guid);
        }

        [HttpPost]
        public IDataResult<BlobFile> Add(BlobFile model)
        {
            return service.Add(model);
        }

        [HttpPost]
        public IDataResult<IEnumerable<BlobFile>> Search(DataSourceLoadOptionsBase req)
        {
            return service.Search(req);
        }

        [HttpPost]
        public IResult AddRange(List<BlobFile> models)
        {
            try
            {
                foreach(var model in models)
                {
                    return service.Add(model);
                }
                return new SuccessResult();
            }
            catch(Exception exp)
            {
                return new ErrorResult(exp.ToString());
            }
        }

    }
}
