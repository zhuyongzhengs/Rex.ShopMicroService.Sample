using System.Net;
using Volo.Abp.Application.Dtos;

namespace Rex.BaseService.Systems.AuditLoggings
{
    /// <summary>
    /// 查询审计日志Dto
    /// </summary>
    public class GetAuditLogInput : PagedResultRequestDto, IPagedAndSortedResultRequest, IPagedResultRequest, ILimitedResultRequest
    {
        /// <summary>
        /// 排序
        /// </summary>
        public string Sorting { get; set; }

        /// <summary>
        /// Url链接
        /// </summary>
        public string? Url { get; set; }

        /// <summary>
        /// 用户名称
        /// </summary>
        public string? UserName { get; set; }

        /// <summary>
        /// 应用程序名称
        /// </summary>
        public string? ApplicationName { get; set; }

        /// <summary>
        /// 关联标识符
        /// </summary>
        public string? CorrelationId { get; set; }

        /// <summary>
        /// Http方法
        /// </summary>
        public string? HttpMethod { get; set; }

        /// <summary>
        /// Http状态码
        /// </summary>
        public HttpStatusCode? HttpStatusCode { get; set; }

        /// <summary>
        /// 最大执行时长
        /// </summary>
        public int? MaxExecutionDuration { get; set; }

        /// <summary>
        /// 最小执行时长
        /// </summary>
        public int? MinExecutionDuration { get; set; }

        /// <summary>
        /// 是否包含异常
        /// </summary>
        public bool? HasException { get; set; }
    }
}