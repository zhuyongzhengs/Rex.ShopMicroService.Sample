using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Auditing;

namespace Rex.BaseService.Systems.AuditLoggings
{
    /// <summary>
    /// 实体变更Dto
    /// </summary>
    public class EntityChangeDto : EntityDto<Guid>
    {
        /// <summary>
        /// 审计日志ID
        /// </summary>
        public Guid AuditLogId { get; set; }

        /// <summary>
        /// 租户ID
        /// </summary>
        public Guid? TenantId { get; set; }

        /// <summary>
        /// 变更时间
        /// </summary>
        public DateTime ChangeTime { get; set; }

        /// <summary>
        /// 变更类型
        /// </summary>
        public EntityChangeType ChangeType { get; set; }

        /// <summary>
        /// 实体租户ID
        /// </summary>
        public Guid? EntityTenantId { get; set; }

        /// <summary>
        /// 实体ID
        /// </summary>
        public string EntityId { get; set; }

        /// <summary>
        /// 实体类型名称(全名)
        /// </summary>
        public string EntityTypeFullName { get; set; }

        /// <summary>
        /// 实体属性变更
        /// </summary>
        public Collection<EntityPropertyChangeDto> PropertyChanges { get; set; }

        /// <summary>
        /// 属性扩展
        /// </summary>
        public Dictionary<string, object> ExtraProperties { get; set; }
    }
}