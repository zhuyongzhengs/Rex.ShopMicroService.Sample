using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace Rex.BaseService.Systems.UserGrades
{
    /// <summary>
    /// 用户等级
    /// </summary>
    public class UserGrade : FullAuditedAggregateRoot<Guid>, IMultiTenant
    {
        public UserGrade()
        {
        }

        public UserGrade(Guid id)
        {
            Id = id;
        }

        /// <summary>
        /// 租户ID
        /// </summary>
        public Guid? TenantId { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        [Required]
        [StringLength(60)]
        public string Title { get; set; }

        /// <summary>
        /// 是否默认
        /// </summary>
        [Required]
        public bool IsDefault { get; set; }
    }
}