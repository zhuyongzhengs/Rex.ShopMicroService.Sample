using System;
using Volo.Abp.Application.Dtos;

namespace Rex.BaseService.Systems.UserOrganizationUnits
{
    /// <summary>
    /// 修改组织单元【用户】
    /// </summary>
    public class UserOrganizationUnitUpdateDto : EntityDto
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// 组织单元ID
        /// </summary>
        public Guid OrganizationUnitId { get; set; }

        /// <summary>
        /// 并发(控制)戳
        /// </summary>
        public string ConcurrencyStamp { get; set; }
    }
}