using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.MultiTenancy;

namespace Rex.BaseService.Systems.OrganizationUnitRoles
{
    /// <summary>
    /// 创建组织单元【角色】
    /// </summary>
    public class OrganizationUnitRoleCreateDto : EntityDto
    {
        /// <summary>
        /// 角色ID
        /// </summary>
        public Guid RoleId { get; set; }

        /// <summary>
        /// 组织单元ID
        /// </summary>
        public Guid OrganizationUnitId { get; set; }
    }
}