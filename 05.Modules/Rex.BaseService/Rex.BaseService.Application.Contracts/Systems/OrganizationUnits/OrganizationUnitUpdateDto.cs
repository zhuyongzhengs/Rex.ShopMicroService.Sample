using System;
using Volo.Abp.Application.Dtos;

namespace Rex.BaseService.Systems.OrganizationUnits
{
    /// <summary>
    /// 修改组织单元Dto
    /// </summary>
    public class OrganizationUnitUpdateDto : EntityDto
    {
        /// <summary>
        /// 编码
        /// </summary>
        //public string Code { get; set; }

        /// <summary>
        /// 显示名称
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// 并发(控制)戳
        /// </summary>
        public string ConcurrencyStamp { get; set; }
    }
}