using Microsoft.AspNetCore.Authorization;
using System;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.PermissionManagement;

namespace Rex.AuthService.Permissions
{
    /// <summary>
    /// 授权服务
    /// </summary>
    [RemoteService(IsEnabled = false)]
    [Authorize]
    public class PermissionGrantAppService : AuthServiceAppService, IPermissionGrantAppService
    {
        private readonly IPermissionManager _permissionsManager;

        public PermissionGrantAppService(IPermissionManager permissionsManager)
        {
            _permissionsManager = permissionsManager;
        }

        /// <summary>
        /// 添加客户端权限
        /// </summary>
        /// <param name="clientName">客户端名称</param>
        /// <param name="permission">权限</param>
        /// <returns></returns>
        public async Task CreateClientPermissionAsync(string clientName, string permission)
        {
            await _permissionsManager.SetAsync(permission, ClientPermissionValueProvider.ProviderName, clientName, true);
        }

        /// <summary>
        /// 添加角色权限
        /// </summary>
        /// <param name="roleName">角色名称</param>
        /// <param name="permission">权限</param>
        /// <returns></returns>
        public async Task CreateRolePermissionAsync(string roleName, string permission)
        {
            await _permissionsManager.SetAsync(permission, RolePermissionValueProvider.ProviderName, roleName, true);
        }

        /// <summary>
        /// 添加用户权限
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="permission">权限</param>
        /// <returns></returns>
        public async Task CreateUserPermissionAsync(Guid userId, string permission)
        {
            await _permissionsManager.SetAsync(permission, UserPermissionValueProvider.ProviderName, userId.ToString(), true);
        }
    }
}