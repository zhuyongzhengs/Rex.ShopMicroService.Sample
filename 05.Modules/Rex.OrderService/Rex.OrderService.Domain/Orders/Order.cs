using Rex.OrderService.Bills;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace Rex.OrderService.Orders
{
    /// <summary>
    /// 订单表
    /// </summary>
    public class Order : FullAuditedAggregateRoot<Guid>, IMultiTenant
    {
        /// <summary>
        /// 租户ID
        /// </summary>
        public Guid? TenantId { get; set; }

        /// <summary>
        /// 订单号
        /// </summary>
        [Required]
        [StringLength(50)]
        public string No { get; set; }

        /// <summary>
        /// 商品总价
        /// </summary>
        [Required]
        public decimal GoodAmount { get; set; }

        /// <summary>
        /// 已支付的金额
        /// </summary>
        [Required]
        public decimal PayedAmount { get; set; }

        /// <summary>
        /// 订单实际销售总额
        /// </summary>
        [Required]
        public decimal OrderAmount { get; set; }

        /// <summary>
        /// 支付状态
        /// </summary>
        /// <remarks>
        /// 枚举：OrderPayStatus
        /// 1：未付款、2：已付款、3：部分付款、4：部分退款、5：已退款
        /// </remarks>
        [Required]
        public int PayStatus { get; set; }

        /// <summary>
        /// 发货状态
        /// </summary>
        /// <remarks>
        /// 枚举：OrderShipStatus
        /// 1：未发货、2：部分发货、3：已发货、4：部分退货、5：已退货
        /// </remarks>
        [Required]
        public int ShipStatus { get; set; }

        /// <summary>
        /// 订单状态
        /// </summary>
        /// <remarks>
        /// 枚举：OrderStatus
        /// 1：订单正常、2：订单完成、3：订单取消
        /// </remarks>
        [Required]
        public int Status { get; set; }

        /// <summary>
        /// 订单类型
        /// </summary>
        /// <remarks>
        /// 枚举：OrderType
        /// 1：普通、2：拼团、3：团购、4：秒杀、5：积分兑换、6：砍价、
        /// 7：赠品、8：接龙、10：微信交易组件
        /// </remarks>
        [Required]
        public int OrderType { get; set; }

        /// <summary>
        /// 收货方式
        /// </summary>
        /// <remarks>
        /// 枚举：OrderReceiptType
        /// 1：物流快递、2：同城配送、3：门店自提
        /// </remarks>
        [Required]
        public int ReceiptType { get; set; }

        /// <summary>
        /// 支付方式代码
        /// </summary>
        [StringLength(20)]
        public string? PaymentCode { get; set; }

        /// <summary>
        /// 支付时间
        /// </summary>
        public DateTime? PaymentTime { get; set; }

        /// <summary>
        /// 配送方式ID 关联ship.id
        /// </summary>
        [Required]
        public Guid LogisticsId { get; set; }

        /// <summary>
        /// 配送方式名称
        /// </summary>
        [StringLength(50)]
        public string? LogisticsName { get; set; }

        /// <summary>
        /// 配送费用
        /// </summary>
        [Required]
        public decimal CostFreight { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        [Required]
        public Guid UserId { get; set; }

        /// <summary>
        /// 店铺ID
        /// </summary>
        [Required]
        public Guid SellerId { get; set; }

        /// <summary>
        /// 售后状态
        /// </summary>
        /// <remarks>
        /// 枚举：OrderConfirmStatus
        /// 1：未确认收货、2：已确认收货
        /// </remarks>
        [Required]
        public int ConfirmStatus { get; set; }

        /// <summary>
        /// 确认收货时间
        /// </summary>
        public DateTime? ConfirmTime { get; set; }

        /// <summary>
        /// 自提门店ID，null就是不门店自提
        /// </summary>
        public Guid? StoreId { get; set; }

        /// <summary>
        /// 收货地区ID
        /// </summary>
        [Required]
        public long ShipAreaId { get; set; }

        /// <summary>
        /// 显示区域信息
        /// </summary>
        [StringLength(150)]
        public string? DisplayArea { get; set; }

        /// <summary>
        /// 收货详细地址
        /// </summary>
        [StringLength(240)]
        [Required]
        public string ShipAddress { get; set; }

        /// <summary>
        /// 收货人姓名
        /// </summary>
        [StringLength(50)]
        [Required]
        public string ShipName { get; set; }

        /// <summary>
        /// 收货电话
        /// </summary>
        [StringLength(50)]
        [Required]
        public string ShipMobile { get; set; }

        /// <summary>
        /// 商品总重量
        /// </summary>
        [Required]
        public decimal Weight { get; set; }

        /// <summary>
        /// 使用积分
        /// </summary>
        [Required]
        public int Point { get; set; }

        /// <summary>
        /// 积分抵扣金额
        /// </summary>
        [Required]
        public decimal PointMoney { get; set; }

        /// <summary>
        /// 会员优惠
        /// </summary>
        [Required]
        public decimal GradeDiscountAmount { get; set; }

        /// <summary>
        /// 秒杀优惠
        /// </summary>
        public decimal SeckillDiscountAmount { get; set; }

        /// <summary>
        /// 订单优惠金额
        /// </summary>
        [Required]
        public decimal OrderDiscountAmount { get; set; }

        /// <summary>
        /// 商品优惠金额
        /// </summary>
        [Required]
        public decimal GoodsDiscountAmount { get; set; }

        /// <summary>
        /// 优惠券优惠额度
        /// </summary>
        [Required]
        public decimal CouponDiscountAmount { get; set; }

        /// <summary>
        /// 优惠券信息
        /// </summary>
        public string? Coupon { get; set; }

        /// <summary>
        /// 优惠信息
        /// </summary>
        public string? PromotionList { get; set; }

        /// <summary>
        /// 买家备注
        /// </summary>
        [StringLength(255)]
        public string? Memo { get; set; }

        /// <summary>
        /// 下单IP
        /// </summary>
        [StringLength(50)]
        public string? Ip { get; set; }

        /// <summary>
        /// 卖家备注
        /// </summary>
        [StringLength(510)]
        public string? Mark { get; set; }

        /// <summary>
        /// 订单来源
        /// </summary>
        /// <remarks>
        /// 枚举：Source
        /// 1：PC端、2：H5端、3：微信小程序端、4：支付宝小程序端、5：APP端
        /// </remarks>
        [Required]
        public int Source { get; set; }

        /// <summary>
        /// 是否评论
        /// </summary>
        [Required]
        public bool IsComment { get; set; }

        /// <summary>
        /// 关联营销类型对象序列
        /// </summary>
        public Guid? ObjectId { get; set; }

        /// <summary>
        /// 订单明细
        /// </summary>
        public List<OrderItem> OrderItems { get; set; } = new();

        /// <summary>
        /// 退货单
        /// </summary>
        public List<BillAftersales> BillAftersales { get; set; } = new();
    }
}