using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace Rex.BaseService.Systems.SysUsers
{
    /// <summary>
    /// 用户积分记录表
    /// </summary>
    public class UserPointLog : FullAuditedAggregateRoot<Guid>, IMultiTenant
    {
        /// <summary>
        /// 租户ID
        /// </summary>
        public Guid? TenantId { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        [Required]
        public Guid UserId { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        [Required]
        public int Type { get; set; }

        /// <summary>
        /// 积分数量
        /// </summary>
        [Required]
        public int Num { get; set; }

        /// <summary>
        /// 积分余额
        /// </summary>
        [Required]
        public int Balance { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [StringLength(255)]
        public string? Remark { get; set; }
    }
}