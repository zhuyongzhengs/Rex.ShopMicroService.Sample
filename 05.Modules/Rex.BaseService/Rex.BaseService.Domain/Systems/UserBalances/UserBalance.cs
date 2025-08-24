using Rex.BaseService.Systems.SysUsers;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace Rex.BaseService.Systems.UserBalances
{
    /// <summary>
    /// 用户余额
    /// </summary>
    public class UserBalance : FullAuditedAggregateRoot<Guid>, IMultiTenant
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
        /// 用户
        /// </summary>
        [ForeignKey(nameof(UserId))]
        public SysUser? User { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        [Required]
        public int Type { get; set; }

        /// <summary>
        /// 金额
        /// </summary>
        [Required]
        public decimal Money { get; set; }

        /// <summary>
        /// 余额
        /// </summary>
        [Required]
        public decimal Balance { get; set; }

        /// <summary>
        /// 资源id
        /// </summary>
        [StringLength(50)]
        public string? SourceId { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        [StringLength(200)]
        public string? Memo { get; set; }
    }
}