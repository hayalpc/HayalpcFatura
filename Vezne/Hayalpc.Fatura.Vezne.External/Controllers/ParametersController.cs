using Hayalpc.Fatura.Common.Dtos;
using Hayalpc.Library.Common.Helpers;
using Hayalpc.Library.Common.Helpers.Interfaces;
using Hayalpc.Library.Common.Results;
using Hayalpc.Library.Log;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hayalpc.Fatura.Vezne.External.Controllers
{
    [Route("[controller]/[action]")]
    public class ParametersController : Controller
    {
        private readonly IHpLogger logger;
        private readonly IHttpClientHelper clientHelper;
        private readonly ISessionHelper session;
        private readonly IMemoryCache memoryCache;

        public ParametersController(IHpLogger logger, IHttpClientHelper clientHelper, ISessionHelper session, IMemoryCache memoryCache)
        {
            this.logger = logger;
            this.clientHelper = clientHelper;
            this.session = session;
            this.memoryCache = memoryCache;
        }

        public IActionResult Categories()
        {
            var list = memoryCache.GetOrCreate("Categories", entry =>
             {
                 entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(1);
                 return clientHelper.Get<DataResult<List<CategoryDto>>>(AppConfigHelper.ApiUrl, "category");
             });
            return Ok(list);
        }

        public IActionResult Institutions()
        {
            var list = memoryCache.GetOrCreate("Institutions", entry =>
             {
                 entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(1);
                 return clientHelper.Get<DataResult<List<InstitutionDto>>>(AppConfigHelper.ApiUrl, "institution");
             });
            return Ok(list);
        }

    }
}
