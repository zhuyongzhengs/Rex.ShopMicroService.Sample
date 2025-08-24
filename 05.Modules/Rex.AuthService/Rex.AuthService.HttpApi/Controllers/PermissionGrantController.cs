using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rex.AuthService.Permissions;
using System;
using System.Threading.Tasks;

namespace Rex.AuthService.Controllers
{
    /// <summary>
    ///权限授予控制器
    /// </summary>
    [Route("api/auth/permission-grant")]
    [Authorize]
    public class PermissionGrantController : AuthServiceController
    {
        private readonly IPermissionGrantAppService _permissionGrantAppService;

        public PermissionGrantController(IPermissionGrantAppService permissionGrantAppService)
        {
            _permissionGrantAppService = permissionGrantAppService;
        }

        /// <summary>
        /// 添加客户端权限
        /// </summary>
        /// <param name="clientName">客户端名称</param>
        /// <param name="permission">权限</param>
        /// <returns></returns>
        [HttpPost("client")]
        public async Task<IActionResult> CreateClientPermissionAsync(string clientName, string permission)
        {
            await _permissionGrantAppService.CreateClientPermissionAsync(clientName, permission);
            return NoContent();
        }

        /// <summary>
        /// 添加角色权限
        /// </summary>
        /// <param name="roleName">角色名称</param>
        /// <param name="permission">权限</param>
        /// <returns></returns>
        [HttpPost("role")]
        public async Task<IActionResult> CreateRolePermissionAsync(string roleName, string permission)
        {
            await _permissionGrantAppService.CreateRolePermissionAsync(roleName, permission);
            return NoContent();
        }

        /// <summary>
        /// 添加用户权限
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="permission">权限</param>
        /// <returns></returns>
        [HttpPost("user")]
        public async Task<IActionResult> CreateUserPermissionAsync(Guid userId, string permission)
        {
            await _permissionGrantAppService.CreateUserPermissionAsync(userId, permission);
            return NoContent();
        }
    }
}