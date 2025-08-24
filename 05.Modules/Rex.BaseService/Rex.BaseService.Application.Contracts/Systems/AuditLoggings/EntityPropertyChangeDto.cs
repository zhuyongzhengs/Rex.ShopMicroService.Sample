using System;
using Volo.Abp.Application.Dtos;

namespace Rex.BaseService.Systems.AuditLoggings
{
    /// <summary>
    /// 实体属性变更Dto
    /// </summary>
    public class EntityPropertyChangeDto : EntityDto<Guid>
    {
        /// <summary>
        /// 租户ID
        /// </summary>
        public Guid? TenantId { get; set; }

        /// <summary>
        /// 实体变更ID
        /// </summary>
        public Guid EntityChangeId { get; set; }

        /// <summary>
        /// 新值
        /// </summary>
        public string NewValue { get; set; }

        /// <summary>
        /// 原始值
        /// </summary>
        public string OriginalValue { get; set; }

        /// <summary>
        /// 属性名称
        /// </summary>
        public string PropertyName { get; set; }

        /// <summary>
        /// 属性类型名称(全名)
        /// </summary>
        public string PropertyTypeFullName { get; set; }
    }
}