using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.MultiTenancy;

namespace Rex.OrderService.Carts
{
    /// <summary>
    /// 购物车Dto
    /// </summary>
    public class CartDto : EntityDto<Guid>, IMultiTenant
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

        /// <summary>
        /// 并发(控制)戳
        /// </summary>
        public string ConcurrencyStamp { get; set; }

        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime? CreationTime { get; set; }
    }
}