using System.Diagnostics;
using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Hayalpc.Fatura.Panel.External.Models;
using Hayalpc.Fatura.Panel.External.Resources;

namespace Hayalpc.Fatura.Panel.External.Controllers
{
    public class ErrorController : Controller
    {
        private readonly LocService tr;

        public ErrorController(LocService tr) 
        {
            this.tr = tr;
        }

        [AllowAnonymous]
        [HttpGet("/error/{code}")]
        public IActionResult Index(int code)
        {
            ViewData["Title"] = "Hata";

            var model = new ErrorVM();
            //model.RequestId = Guid.NewGuid().ToString("N");
            model.RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            model.StatusCode = code;
            switch ((HttpStatusCode)code)
            {
                case HttpStatusCode.NotFound:
                    model.Title = tr.Get("NotFound");
                    break;
                case HttpStatusCode.Unauthorized:
                    model.Title = tr.Get("Unauthorized");
                    break;
                case HttpStatusCode.Forbidden:
                    model.Title = tr.Get("Forbidden");
                    break;
                default:
                    model.Title = tr.Get("GetError");
                    break;
            }
            return View(model);
        }
       
    }
}
