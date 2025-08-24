using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace Rex.OrderService.Orders
{
    /// <summary>
    /// 订单明细表
    /// </summary>
    public class OrderItem : FullAuditedAggregateRoot<Guid>, IMultiTenant
    {
        /// <summary>
        /// 租户ID
        /// </summary>
        public Guid? TenantId { get; set; }

        /// <summary>
        /// 订单ID 关联order.id
        /// </summary>
        [Required]
        public Guid OrderId { get; set; }

        /// <summary>
        /// 订单
        /// </summary>
        [ForeignKey(nameof(OrderId))]
        public Order? Order { get; set; }

        /// <summary>
        /// 商品ID 关联goods.id
        /// </summary>
        [Required]
        public Guid GoodId { get; set; }

        /// <summary>
        /// 货品ID 关联products.id
        /// </summary>
        [Required]
        public Guid ProductId { get; set; }

        /// <summary>
        /// 货品编码
        /// </summary>
        [StringLength(30)]
        [Required]
        public string Sn { get; set; }

        /// <summary>
        /// 商品编码
        /// </summary>
        [StringLength(30)]
        [Required]
        public string Bn { get; set; }

        /// <summary>
        /// 商品名称
        /// </summary>
        [Required]
        [StringLength(200)]
        public string Name { get; set; }

        /// <summary>
        /// 货品价格单价
        /// </summary>
        [Required]
        public decimal Price { get; set; }

        /// <summary>
        /// 货品成本价单价
        /// </summary>
        [Required]
        public decimal CostPrice { get; set; }

        /// <summary>
        /// 市场价
        /// </summary>
        [Required]
        public decimal MktPrice { get; set; }

        /// <summary>
        /// 图片
        /// </summary>
        [Required]
        [StringLength(200)]
        public string ImageUrl { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        [Required]
        public int Nums { get; set; }

        /// <summary>
        /// 总价
        /// </summary>
        [Required]
        public decimal Amount { get; set; }

        /// <summary>
        /// 商品优惠总金额
        /// </summary>
        [Required]
        public decimal PromotionAmount { get; set; }

        /// <summary>
        /// 促销信息
        /// </summary>
        [StringLength(255)]
        public string? PromotionList { get; set; }

        /// <summary>
        /// 总重量
        /// </summary>
        [Required]
        public decimal Weight { get; set; }

        /// <summary>
        /// 发货数量
        /// </summary>
        [Required]
        public int SendNums { get; set; }

        /// <summary>
        /// 货品明细序列号存储
        /// </summary>
        public string? Addon { get; set; }
    }
}