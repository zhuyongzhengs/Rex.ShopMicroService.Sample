using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;

namespace Rex.BaseService.Systems.AuditLoggings
{
    /// <summary>
    /// 审计日志操作Dto
    /// </summary>
    public class AuditLogActionDto : EntityDto<Guid>
    {
        /// <summary>
        /// 审计日志ID
        /// </summary>
        public Guid AuditLogId { get; set; }

        /// <summary>
        /// 服务名称
        /// </summary>
        public string ServiceName { get; set; }

        /// <summary>
        /// 方法名称
        /// </summary>
        public string MethodName { get; set; }

        /// <summary>
        /// 参数
        /// </summary>
        public string? Parameters { get; set; }

        /// <summary>
        /// 执行时间
        /// </summary>
        public DateTime ExecutionTime { get; set; }

        /// <summary>
        /// 执行时长
        /// </summary>
        public int ExecutionDuration { get; set; }

        /// <summary>
        /// 扩展属性
        /// </summary>
        public Dictionary<string, object> ExtraProperties { get; set; }
    }
}