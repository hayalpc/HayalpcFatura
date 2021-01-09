using System.Collections.Generic;
using Hayalpc.Library.Common.Models;
using Hayalpc.Library.Common.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Hayalpc.Fatura.Data.Models;
using Hayalpc.Fatura.Panel.Internal.Services.Interfaces;
using System.Security.Claims;
using Microsoft.Extensions.Caching.Memory;
using DevExtreme.AspNet.Data;
using Hayalpc.Library.Common.Dtos;
using System;
using Hayalpc.Library.Log;

namespace Hayalpc.Fatura.Panel.Internal.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService service;
        private readonly IHttpContextAccessor httpContext;
        private readonly IMemoryCache cache;
        private readonly IHpLogger logger;

        public UserController(IUserService service, IHttpContextAccessor httpContext, IMemoryCache cache, IHpLogger logger)
        {
            this.service = service;
            this.httpContext = httpContext;
            this.cache = cache;
            this.logger = logger;
        }

        [HttpPost]
        [AllowAnonymous]
        public IDataResult<SessionModel> Login(LoginRequest loginRequest)
        {
            return service.Login(loginRequest);
        }

        [HttpGet]
        public IDataResult<List<UserBulletin>> Bulletin()
        {
            return new SuccessDataResult<List<UserBulletin>>(service.Bulletin());
        }
        [HttpGet]
        public IDataResult<UserDataDto> Data()
        {
            return service.Data();
        }

        [HttpGet]
        public IActionResult Validate()
        {
            var userId = long.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            if (userId > 0)
            {
                var sessionId = User.FindFirstValue(ClaimTypes.Sid);
                var userSessionId = cache.Get($"SessionId-{userId}");
                if (userSessionId != null && sessionId == userSessionId.ToString())
                {
                    //var user = service.Get(userId,false);
                    //if (user.IsSuccess && user.Data.LastSessionId == sessionId && user.Data.Status == Hayalpc.Library.Common.Enums.Status.Active)
                    //{
                    //    return Ok();
                    //}
                    //else
                    //{
                    //    return Unauthorized();
                    //}
                    return Ok();
                }
                else
                {
                    return Unauthorized();
                }
            }
            return Unauthorized();
        }

        [HttpGet("{id}")]
        public Result ResetPassword(long Id)
        {
            return service.ResetPassword(Id);
        }

        [HttpPost]
        public Result AccountChangePassword(ChangePassword changePassword)
        {
            return service.AccountChangePassword(changePassword);
        }

        [HttpGet("{id}")]
        public Result Logout(long Id)
        {
            return service.Logout(Id);
        }

        [HttpGet]
        [AllowAnonymous]
        public Result ForgotPassword(string email)
        {
            return service.ForgetPassword(email);
        }

        [HttpPost]
        [AllowAnonymous]
        public Result UpdatePassword(PasswordRequest passwordRequest)
        {
            return service.UpdatePassword(passwordRequest);
        }

        [HttpGet("{id}")]
        public IDataResult<User> Get(long Id)
        {
            return service.Get(Id);
        }

        [HttpGet("{id}")]
        public IDataResult<User> Detail(long Id)
        {
            return service.Get(Id, false);
        }

        [HttpPost]
        public IDataResult<User> Add(User user)
        {
            return service.Add(user);
        }

        [HttpPut]
        public IDataResult<User> Update(User model)
        {
            return service.Update(model);
        }

        [HttpPost]
        public IDataResult<IEnumerable<User>> Search(DataSourceLoadOptionsBase req)
        {
            return service.Search(req);
        }

    }
}
