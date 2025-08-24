using System;
using System.Threading.Tasks;

namespace Rex.AuthService.Permissions
{
    /// <summary>
    /// 授权接口服务
    /// </summary>
    public interface IPermissionGrantAppService
    {
        /// <summary>
        /// 添加角色权限
        /// </summary>
        /// <param name="roleName">角色名称</param>
        /// <param name="permission">权限</param>
        /// <returns></returns>
        public Task CreateRolePermissionAsync(string roleName, string permission);

        /// <summary>
        /// 添加用户权限
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="permission">权限</param>
        /// <returns></returns>
        public Task CreateUserPermissionAsync(Guid userId, string permission);

        /// <summary>
        /// 添加客户端权限
        /// </summary>
        /// <param name="clientName">客户端名称</param>
        /// <param name="permission">权限</param>
        /// <returns></returns>
        public Task CreateClientPermissionAsync(string clientName, string permission);
    }
}