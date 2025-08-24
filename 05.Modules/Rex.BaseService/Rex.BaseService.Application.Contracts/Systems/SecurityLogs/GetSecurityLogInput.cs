using System;
using Volo.Abp.Application.Dtos;

namespace Rex.BaseService.Systems.SecurityLogs
{
    /// <summary>
    /// 查询安全日志Dto
    /// </summary>
    public class GetSecurityLogInput : PagedResultRequestDto, IPagedAndSortedResultRequest, IPagedResultRequest, ILimitedResultRequest
    {
        /// <summary>
        /// 排序
        /// </summary>
        public string Sorting { get; set; }

        /// <summary>
        /// 执行的操作
        /// </summary>
        /// <remarks>
        /// 例如：登录、注销等
        /// </remarks>
        public string? ActionName { get; set; }

        /// <summary>
        /// 用户名称
        /// </summary>
        public string? UserName { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime? BeginTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? EndTime { get; set; }
    }
}