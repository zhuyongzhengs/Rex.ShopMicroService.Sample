using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace Rex.BaseService.UserShips
{
    /// <summary>
    /// 用户(收货)地址
    /// </summary>
    public class UserShip : FullAuditedAggregateRoot<Guid>, IMultiTenant
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
        /// 区域ID
        /// </summary>
        [Required]
        public long AreaId { get; set; }

        /// <summary>
        /// 显示区域信息
        /// </summary>
        [StringLength(100)]
        public string? DisplayArea { get; set; }

        /// <summary>
        /// 收货详细地址
        /// </summary>
        [Required]
        [StringLength(200)]
        public string Address { get; set; }

        /// <summary>
        /// 收货人姓名
        /// </summary>
        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        /// <summary>
        /// 收货人电话
        /// </summary>
        [Required]
        [StringLength(30)]
        public string Mobile { get; set; }

        /// <summary>
        /// 是否默认
        /// </summary>
        [Required]
        public bool IsDefault { get; set; }
    }
}