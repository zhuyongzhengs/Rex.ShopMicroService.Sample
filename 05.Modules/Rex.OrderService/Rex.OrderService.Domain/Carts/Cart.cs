using System;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace Rex.OrderService.Carts
{
    /// <summary>
    /// 购物车表
    /// </summary>
    public class Cart : FullAuditedAggregateRoot<Guid>, IMultiTenant
    {
        /// <summary>
        /// 租户ID
        /// </summary>
        public Guid? TenantId { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// 货品ID
        /// </summary>
        public Guid ProductId { get; set; }

        /// <summary>
        /// 货品数量
        /// </summary>
        public int Nums { get; set; }

        /// <summary>
        /// 购物车类型
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// 关联对象ID
        /// </summary>
        public Guid? ObjectId { get; set; }
    }
}