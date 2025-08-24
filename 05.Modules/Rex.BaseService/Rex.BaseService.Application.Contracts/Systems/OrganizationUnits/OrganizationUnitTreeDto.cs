using Rex.BaseService.Systems.OrganizationUnitRoles;
using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;

namespace Rex.BaseService.Systems.OrganizationUnits
{
    /// <summary>
    /// 组织单元树形Dto
    /// </summary>
    public class OrganizationUnitTreeDto : EntityDto<Guid>
    {
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
        /// 并发(控制)戳
        /// </summary>
        public string ConcurrencyStamp { get; set; }

        /// <summary>
        /// 组织单元角色
        /// </summary>
        public List<OrganizationUnitRoleDto> Roles { get; set; }

        /// <summary>
        /// 组织单元角色
        /// </summary>
        public List<OrganizationUnitTreeDto> Children { get; set; }
    }
}