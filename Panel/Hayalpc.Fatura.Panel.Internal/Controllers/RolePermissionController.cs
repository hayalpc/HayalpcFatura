using System.Collections.Generic;
using Hayalpc.Library.Common.Results;
using Microsoft.AspNetCore.Mvc;
using Hayalpc.Fatura.Data.Models;
using Hayalpc.Fatura.Panel.Internal.Services.Interfaces;

namespace Hayalpc.Fatura.Panel.Internal.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RolePermissionController : ControllerBase
    {
        private readonly IRolePermissionService service;

        public RolePermissionController(IRolePermissionService service)
        {
            this.service = service;
        }

        [HttpGet("{id}")]
        public List<RolePermission> Permissions(long Id)
        {
            return service.GetByRoleId(Id);
        }

        [HttpGet("{id}")]
        public IDataResult<RolePermission> Get(long Id)
        {
            return service.Get(Id);
        }

        [HttpPut("{id}")]
        public IDataResult<RolePermission> Update(long Id,RolePermission rolePermission)
        {
            var record = service.Get(Id);
            var rolePermissionData = record.Data;

            rolePermissionData.Name = rolePermission.Name;
            rolePermissionData.Controller = rolePermission.Controller;
            rolePermissionData.Action = rolePermission.Action;
            rolePermissionData.IsMenu = rolePermission.IsMenu;
            rolePermissionData.Icon = rolePermission.Icon;
            rolePermissionData.Order = rolePermission.Order;

            return service.Update(rolePermissionData);
        }

        [HttpPost]
        public IDataResult<RolePermission> Add(RolePermission rolePermission)
        {
            return service.Add(rolePermission);
        }

        [HttpDelete("{id}")]
        public IResult Delete(long id)
        {
            return service.Delete(id);
        }
    }

}
