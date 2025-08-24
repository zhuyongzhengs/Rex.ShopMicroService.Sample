using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace Rex.GoodService.Goods
{
    /// <summary>
    /// 商品浏览记录表
    /// </summary>
    public class GoodBrowsing : FullAuditedAggregateRoot<Guid>, IMultiTenant
    {
        /// <summary>
        /// 租户ID
        /// </summary>
        public Guid? TenantId { get; set; }

        /// <summary>
        /// 商品id 关联goods.id
        /// </summary>
        [Required]
        public Guid GoodId { get; set; }

        /// <summary>
        /// 用户id
        /// </summary>
        [Required]
        public Guid UserId { get; set; }

        /// <summary>
        /// 商品名称
        /// </summary>
        [Required]
        [StringLength(200)]
        public string GoodName { get; set; }

        /// <summary>
        /// 商品图片
        /// </summary>
        [StringLength(1000)]
        public string? Image { get; set; }
    }
}