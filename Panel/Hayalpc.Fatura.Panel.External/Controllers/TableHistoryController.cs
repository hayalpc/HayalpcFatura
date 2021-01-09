using Hayalpc.Fatura.Panel.External.Models;
using Hayalpc.Fatura.Panel.External.Resources;
using Hayalpc.Fatura.Panel.External.Services.Interfaces;
using Hayalpc.Library.Common.Helpers.Interfaces;
using Hayalpc.Library.Common.Results;
using Hayalpc.Library.Log;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hayalpc.Fatura.Panel.External.Controllers
{
    public class TableHistoryController : BaseController<TableHistoryVM, ITableHistoryService>
    {
        public TableHistoryController(ITableHistoryService service, LocService tr, ISessionHelper session, IHpLogger logger) : base(service, tr, session, logger)
        {
        }

        [HttpGet]
        public IActionResult Approve(long Id)
        {
            var result = service.Get<Result>($"{service.Path}/approve/{Id}");
            if (result.IsSuccess)
            {
                session.SetSuccessMessage(result.Message);
                return RedirectToAction(nameof(Detail), new { Id });
            }
            else
            {
                session.SetErrorMessage(result.Message);
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpGet]
        public IActionResult Reject(long Id)
        {
            var result = service.Get<Result>($"{service.Path}/reject/{Id}");
            if (result.IsSuccess)
            {
                session.SetSuccessMessage(result.Message);
                return RedirectToAction(nameof(Detail), new { Id });
            }
            else
            {
                session.SetErrorMessage(result.Message);
                return RedirectToAction(nameof(Index));
            }
        }
    }

}
