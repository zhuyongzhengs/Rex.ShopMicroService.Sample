using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.MultiTenancy;

namespace Rex.OrderService.Orders
{
    /// <summary>
    /// 订单明细Dto
    /// </summary>
    public class OrderItemDto : EntityDto<Guid>, IMultiTenant
    {
        /// <summary>
        /// 租户ID
        /// </summary>
        public Guid? TenantId { get; set; }

        /// <summary>
        /// 订单ID
        /// </summary>
        public Guid OrderId { get; set; }

        /// <summary>
        /// 订单
        /// </summary>
        public OrderDto? Order { get; set; }

        /// <summary>
        /// 商品ID
        /// </summary>
        public Guid GoodId { get; set; }

        /// <summary>
        /// 货品ID
        /// </summary>
        public Guid ProductId { get; set; }

        /// <summary>
        /// 货品编码
        /// </summary>
        public string Sn { get; set; }

        /// <summary>
        /// 商品编码
        /// </summary>
        public string Bn { get; set; }

        /// <summary>
        /// 商品名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 货品价格单价
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// 货品成本价单价
        /// </summary>
        public decimal CostPrice { get; set; }

        /// <summary>
        /// 市场价
        /// </summary>
        public decimal MktPrice { get; set; }

        /// <summary>
        /// 图片
        /// </summary>
        public string ImageUrl { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public int Nums { get; set; }

        /// <summary>
        /// 总价
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// 商品优惠总金额
        /// </summary>
        public decimal PromotionAmount { get; set; }

        /// <summary>
        /// 促销信息
        /// </summary>
        public string? PromotionList { get; set; }

        /// <summary>
        /// 总重量
        /// </summary>
        public decimal Weight { get; set; }

        /// <summary>
        /// 发货数量
        /// </summary>
        public int SendNums { get; set; }

        /// <summary>
        /// 退货数量
        /// </summary>
        public int ReshipNums { get; set; }

        /// <summary>
        /// 货品明细序列号存储
        /// </summary>
        public string? Addon { get; set; }

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