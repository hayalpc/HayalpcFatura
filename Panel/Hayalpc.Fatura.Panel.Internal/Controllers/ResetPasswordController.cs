using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hayalpc.Library.Common.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Hayalpc.Fatura.Data.Models;
using Hayalpc.Fatura.Panel.Internal.Services;

namespace Hayalpc.Fatura.Panel.Internal.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ResetPasswordController : ControllerBase
    {
        private readonly IResetPasswordService resetPasswordService;

        public ResetPasswordController(IResetPasswordService resetPasswordService)
        {
            this.resetPasswordService = resetPasswordService;
        }

        [HttpGet("{token}")]
        [AllowAnonymous]
        public IDataResult<ResetPassword> GetByToken(Guid token)
        {
            return resetPasswordService.GetByToken(token);
        }
    }

}
