using System;
using System.Collections.Generic;
using Hayalpc.Library.Common.Dtos;
using Hayalpc.Fatura.Common.Helpers.Interfaces;
using Hayalpc.Library.Common.Results;
using Hayalpc.Library.Log;
using Microsoft.AspNetCore.Mvc;
using Hayalpc.Fatura.Panel.External.Models;
using Hayalpc.Fatura.Panel.External.Resources;
using Hayalpc.Fatura.Panel.External.Services.Interfaces;

namespace Hayalpc.Fatura.Panel.External.Controllers
{
    public class RoleController : BaseController<RoleVM, IRoleService>
    {
        public RoleController(LocService tr, ISessionHelper session, IHpLogger logger, IRoleService service) : base(service,tr, session, logger)
        {
        }

        public IActionResult Permissions(long id)
        {
            var userRes = service.Get<DataResult<RoleVM>>("role/get/" + id);
            if (userRes.IsSuccess)
            {
                var perRes = service.Get<List<RolePermissionDto>>("rolePermission/permissions/" + id);

                userRes.Data.RolePermissions = perRes;
                return View(userRes.Data);
            }
            else
            {
                session.SetErrorMessage("RecordNotFound");
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        public IActionResult Permissions(long id, [FromForm] RolePermissionDto rolePermission, [FromQuery] long roleId)
        {
            rolePermission.RoleId = roleId;

            if (rolePermission.Type == "new")
            {
                rolePermission.Status = Hayalpc.Library.Common.Enums.Status.Active;
                var result = service.Post<RolePermissionDto, Result>("rolePermission/add", rolePermission);
                if (result.IsSuccess)
                {
                    session.SetSuccessMessage("OperationSuccess");
                    return Ok();
                }
                session.SetErrorMessage("OperationFail");
                return BadRequest();
            }
            else if (rolePermission.Type == "update")
            {
                var result = service.Put<RolePermissionDto, Result>("rolePermission/update/" + rolePermission.Id, rolePermission);
                if (result.IsSuccess)
                {
                    session.SetSuccessMessage("OperationSuccess");
                    return Ok();
                }
                session.SetErrorMessage("OperationFail");
                return BadRequest();
            }
            else if (rolePermission.Type == "delete")
            {
                var result = service.Delete<Result>("rolePermission/delete/" + rolePermission.Id);
                if (result.IsSuccess)
                {
                    session.SetSuccessMessage("OperationSuccess");
                    return Ok();
                }
                session.SetErrorMessage("OperationFail");
                return BadRequest();
            }
            session.SetErrorMessage("RecordNotfound");
            return NotFound();
        }


    }
}
