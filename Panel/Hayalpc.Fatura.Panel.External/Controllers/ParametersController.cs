using DevExtreme.AspNet.Mvc;
using Hayalpc.Fatura.Panel.External.Models;
using Hayalpc.Fatura.Panel.External.Services;
using Hayalpc.Fatura.Panel.External.Services.Interfaces;
using Hayalpc.Library.Common.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hayalpc.Fatura.Panel.External.Controllers
{
    [AllowAnonymous]
    [Authorize]
    public class ParametersController : Controller
    {
        private readonly IBaseService<BaseVM> baseService;

        public ParametersController(IBaseService<BaseVM> baseService)
        {
            this.baseService = baseService;
        }

        [HttpPost]
        public virtual DataResult<List<CountryVM>> Country(DataSourceLoadOptions loadOptions)
        {
            return baseService.Post<DataSourceLoadOptions, DataResult<List<CountryVM>>>("parameters/country", loadOptions);
        }

        [HttpPost]
        public virtual DataResult<List<CityVM>> City(DataSourceLoadOptions loadOptions)
        {
            return baseService.Post<DataSourceLoadOptions, DataResult<List<CityVM>>>("parameters/city", loadOptions);
        }

        [HttpPost]
        public virtual DataResult<List<DistrictVM>> District(DataSourceLoadOptions loadOptions)
        {
            return baseService.Post<DataSourceLoadOptions, DataResult<List<DistrictVM>>>("parameters/district", loadOptions);
        }

    }
}
