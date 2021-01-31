using Hayalpc.Fatura.Panel.External.Models;
using Hayalpc.Fatura.Panel.External.Resources;
using Hayalpc.Fatura.Panel.External.Services.Interfaces;
using Hayalpc.Fatura.Common.Helpers.Interfaces;
using Hayalpc.Library.Log;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hayalpc.Fatura.Panel.External.Controllers
{
    public class InstitutionController : BaseController<InstitutionVM, IInstitutionService>
    {
        public InstitutionController(IInstitutionService service, LocService tr, ISessionHelper session, IHpLogger logger) : base(service, tr, session, logger)
        {
        }
    }
}
