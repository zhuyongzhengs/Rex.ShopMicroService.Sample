using System;
using Volo.Abp.Application.Dtos;

namespace Rex.BaseService.Systems.UserOrganizationUnits
{
    /// <summary>
    /// 创建组织单元【用户】
    /// </summary>
    public class UserOrganizationUnitCreateDto : EntityDto
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// 组织单元ID
        /// </summary>
        public Guid OrganizationUnitId { get; set; }
    }
}