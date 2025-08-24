using Rex.BaseService.Systems.OrganizationUnitRoles;
using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;
using Volo.Abp.MultiTenancy;

namespace Rex.BaseService.Systems.OrganizationUnits
{
    /// <summary>
    /// 组织单元Dto
    /// </summary>
    public class OrganizationUnitDto : EntityDto<Guid>, IMultiTenant
    {
        /// <summary>
        /// 租户ID
        /// </summary>
        public Guid? TenantId { get; set; }

        /// <summary>
        /// 父ID
        /// </summary>
        public Guid? ParentId { get; set; }

        /// <summary>
        /// 编码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 显示名称
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// 版本值
        /// </summary>
        public int EntityVersion { get; set; }

        /// <summary>
        /// 并发(控制)戳
        /// </summary>
        public string ConcurrencyStamp { get; set; }

        /// <summary>
        /// 组织单元角色
        /// </summary>
        public List<OrganizationUnitRoleDto> Roles { get; set; }
    }
}