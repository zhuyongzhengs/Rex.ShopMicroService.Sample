using System;
using Volo.Abp.Application.Dtos;

namespace Rex.BaseService.Systems.OrganizationUnits
{
    /// <summary>
    /// 创建组织单元Dto
    /// </summary>
    public class OrganizationUnitCreateDto : EntityDto
    {
        /// <summary>
        /// 父ID
        /// </summary>
        public Guid? ParentId { get; set; }

        /// <summary>
        /// 显示名称
        /// </summary>
        public string DisplayName { get; set; }
    }
}