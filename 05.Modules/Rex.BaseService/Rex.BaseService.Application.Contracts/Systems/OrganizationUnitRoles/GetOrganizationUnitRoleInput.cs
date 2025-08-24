using System;
using Volo.Abp.Application.Dtos;

namespace Rex.BaseService.Systems.OrganizationUnitRoles
{
    /// <summary>
    /// 查询角色组织单元参数
    /// </summary>
    public class GetOrganizationUnitRoleInput : PagedAndSortedResultRequestDto
    {
        /// <summary>
        /// 查询关键字
        /// </summary>
        public string? Filter { get; set; }

        /// <summary>
        /// 组织单元ID
        /// </summary>
        public Guid? OrganizationUnitId { get; set; }
    }
}