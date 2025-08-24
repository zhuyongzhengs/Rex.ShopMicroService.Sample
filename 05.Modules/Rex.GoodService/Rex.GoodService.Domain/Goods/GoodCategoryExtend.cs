using System;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace Rex.GoodService.Goods
{
    /// <summary>
    /// 商品分类扩展表
    /// </summary>
    public class GoodCategoryExtend : FullAuditedAggregateRoot<Guid>, IMultiTenant
    {
        /// <summary>
        /// 租户ID
        /// </summary>
        public Guid? TenantId { get; set; }

        /// <summary>
        /// 商品id
        /// </summary>
        public Guid? GoodId { get; set; }

        /// <summary>
        /// 商品分类id
        /// </summary>
        public Guid? GoodCategroyId { get; set; }
    }
}