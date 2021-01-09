using DevExtreme.AspNet.Data;
using Hayalpc.Fatura.Data.Models;
using Hayalpc.Fatura.Panel.Internal.Services.Interfaces;
using Hayalpc.Library.Common.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Hayalpc.Fatura.Panel.Internal.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [AllowAnonymous]
    public class ParametersController : ControllerBase
    {
        private readonly IParametersService parametersService;

        public ParametersController(IParametersService parametersService)
        {
            this.parametersService = parametersService;
        }

        [HttpPost]
        public IDataResult<IEnumerable<Country>> Country(DataSourceLoadOptionsBase req)
        {
            return parametersService.Search<Country>(req);
        }

        [HttpPost]
        public IDataResult<IEnumerable<City>> City(DataSourceLoadOptionsBase req)
        {
            return parametersService.Search<City>(req);
        }

        [HttpPost]
        public IDataResult<IEnumerable<District>> District(DataSourceLoadOptionsBase req)
        {
            return parametersService.Search<District>(req);
        }
    }
}
