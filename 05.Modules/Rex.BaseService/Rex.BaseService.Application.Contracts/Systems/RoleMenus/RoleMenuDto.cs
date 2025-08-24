using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.MultiTenancy;

namespace Rex.BaseService.Systems.RoleMenus
{
    /// <summary>
    /// 角色菜单
    /// </summary>
    public class RoleMenuDto : EntityDto<Guid>, IMultiTenant
    {
        /// <summary>
        /// 租户ID
        /// </summary>
        public Guid? TenantId { get; set; }

        /// <summary>
        /// 角色ID
        /// </summary>
        public Guid RoleId { get; set; }

        /// <summary>
        /// 菜单ID
        /// </summary>
        public Guid MenuId { get; set; }

        /// <summary>
        /// 并发(控制)戳
        /// </summary>
        public string ConcurrencyStamp { get; set; }
    }
}