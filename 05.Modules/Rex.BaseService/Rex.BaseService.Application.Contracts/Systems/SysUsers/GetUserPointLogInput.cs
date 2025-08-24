using System;
using Volo.Abp.Application.Dtos;

namespace Rex.BaseService.Systems.SysUsers
{
    /// <summary>
    /// 查询用户积分记录
    /// </summary>
    public class GetUserPointLogInput : PagedAndSortedResultRequestDto
    {
        /// <summary>
        /// 类型
        /// </summary>
        public int? Type { get; set; }
    }
}