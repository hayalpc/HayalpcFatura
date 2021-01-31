using Hayalpc.Fatura.Common.Dtos;
using Hayalpc.Fatura.CoreApi.Services.Interfaces;
using Hayalpc.Fatura.Data;
using Hayalpc.Fatura.Data.Models;
using Hayalpc.Library.Common.Results;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hayalpc.Fatura.CoreApi.Services
{
    public class InstitutionService : IInstitutionService
    {
        private readonly IRepository<Institution> repository;
        private readonly IMemoryCache cache;

        public InstitutionService(IRepository<Institution> repository, IMemoryCache cache)
        {
            this.repository = repository;
            this.cache = cache;
        }

        public IDataResult<List<InstitutionDto>> Get()
        {
            var cacheKey = "Institution-Get";
            var list = cache.Get<List<InstitutionDto>>(cacheKey);
            if(list == null)
            {
                list = repository.GetQuery().Select(x => new InstitutionDto
                {
                    Id = x.Id,
                    Code = x.Code,
                    Name = x.Name,
                    Description = x.Description,
                    CategoryId = x.CategoryId,
                    Type = x.Type,
                    Placeholder = x.Placeholder,
                    Reverse = x.Reverse,
                    Logo = x.Logo,
                    ValidationText = x.ValidationText,
                }).ToList();
                cache.Set(cacheKey, list, TimeSpan.FromHours(1));
            }
            return new SuccessDataResult<List<InstitutionDto>>(list,list.Count);
        }

    }
}
