using System;
using Volo.Abp.Domain.Entities;
using Volo.Abp.MultiTenancy;

namespace Rex.BaseService.Systems.RoleMenus
{
    /// <summary>
    /// 角色菜单
    /// </summary>
    public class RoleMenu : AggregateRoot<Guid>, IMultiTenant
    {
        public RoleMenu()
        {
        }

        public RoleMenu(Guid id)
        {
            Id = id;
        }

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
    }
}