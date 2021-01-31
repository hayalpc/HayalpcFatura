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
    public class CategoryService : ICategoryService
    {
        private readonly IRepository<Category> repository;
        private readonly IMemoryCache cache;

        public CategoryService(IRepository<Category> repository, IMemoryCache cache)
        {
            this.repository = repository;
            this.cache = cache;
        }

        public IDataResult<List<CategoryDto>> Get()
        {
            var cacheKey = "Category-Get";
            var list = cache.Get<List<CategoryDto>>(cacheKey);
            if(list == null)
            {
                list = repository.GetQuery().Select(x => new CategoryDto
                {
                    Id = x.Id,
                    Code = x.Code,
                    Name = x.Name,
                    Description = x.Description,
                }).ToList();
                cache.Set(cacheKey, list, TimeSpan.FromHours(1));
            }
            return new SuccessDataResult<List<CategoryDto>>(list,list.Count);
        }

    }
}
