using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;

namespace Rex.BaseService.Systems.AuditLoggings
{
    /// <summary>
    /// 审计日志Dto
    /// </summary>
    public class AuditLogDto : EntityDto<Guid>
    {
        /// <summary>
        /// 应用程序名称
        /// </summary>
        public string ApplicationName { get; set; }

        /// <summary>
        /// 操作人ID
        /// </summary>
        public Guid? UserId { get; set; }

        /// <summary>
        /// 操作人名称
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 租户ID
        /// </summary>
        public Guid? TenantId { get; set; }

        /// <summary>
        /// 租户名称
        /// </summary>
        public string TenantName { get; set; }

        /// <summary>
        /// 代替操作人ID
        /// </summary>
        /// <remarks>
        /// 用于记录执行操作的用户的ID，当一个用户代表另一个用户执行操作时，这个字段会记录被代表用户的ID
        /// </remarks>
        public Guid? ImpersonatorUserId { get; set; }

        /// <summary>
        /// 代替操作人名称
        /// </summary>
        /// <remarks>
        /// 用于记录执行操作的用户所属的租户的ID，当一个用户代表另一个用户执行操作时，这个字段会记录被代表用户所属的租户的ID
        /// </remarks>
        public Guid? ImpersonatorTenantId { get; set; }

        /// <summary>
        /// 执行时间
        /// </summary>
        public DateTime ExecutionTime { get; set; }

        /// <summary>
        /// 执行时长
        /// </summary>
        public int ExecutionDuration { get; set; }

        /// <summary>
        /// 客户端IP
        /// </summary>
        public string ClientIpAddress { get; set; }

        /// <summary>
        /// 客户端名称
        /// </summary>
        public string ClientName { get; set; }

        /// <summary>
        /// 客户端名称
        /// </summary>
        public string ClientId { get; set; }

        /// <summary>
        /// 关联标识符
        /// </summary>
        public string CorrelationId { get; set; }

        /// <summary>
        /// 浏览器信息
        /// </summary>
        public string BrowserInfo { get; set; }

        /// <summary>
        /// Http方法
        /// </summary>
        public string HttpMethod { get; set; }

        /// <summary>
        /// 请求链接
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// 异常信息
        /// </summary>
        public string Exceptions { get; set; }

        /// <summary>
        /// 注释
        /// </summary>
        public string Comments { get; set; }

        /// <summary>
        /// Http状态码
        /// </summary>
        public int? HttpStatusCode { get; set; }

        /// <summary>
        /// 扩展属性
        /// </summary>
        public Dictionary<string, object> ExtraProperties { get; set; }

        /// <summary>
        /// 实体变更
        /// </summary>
        public List<EntityChangeDto> EntityChanges { get; set; } = new();

        /// <summary>
        /// 审计日志操作
        /// </summary>
        public List<AuditLogActionDto> Actions { get; set; } = new();
    }
}