using System;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities.Events.Distributed;

namespace Rex.Service.Core.Events.Orders
{
    /// <summary>
    /// 用户订单创建Eto
    /// </summary>
    public class OrderCreateEto : EntityEto
    {
        /// <summary>
        /// 事件名称
        /// </summary>
        public const string EventName = "User.Order.Create";

        /// <summary>
        /// 订单号
        /// </summary>
        public string No { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// 商品总价
        /// </summary>
        public decimal GoodAmount { get; set; }

        /// <summary>
        /// 已支付的金额
        /// </summary>
        public decimal PayedAmount { get; set; }

        /// <summary>
        /// 订单实际销售总额
        /// </summary>
        public decimal OrderAmount { get; set; }

        /// <summary>
        /// 支付状态
        /// </summary>
        /// <remarks>
        /// 枚举：OrderPayStatus
        /// 1：未付款、2：已付款、3：部分付款、4：部分退款、5：已退款
        /// </remarks>
        public int PayStatus { get; set; }

        /// <summary>
        /// 发货状态
        /// </summary>
        /// <remarks>
        /// 枚举：OrderShipStatus
        /// 1：未发货、2：部分发货、3：已发货、4：部分退货、5：已退货
        /// </remarks>
        public int ShipStatus { get; set; }

        /// <summary>
        /// 订单状态
        /// </summary>
        /// <remarks>
        /// 枚举：OrderStatus
        /// 1：订单正常、2：订单完成、3：订单取消
        /// </remarks>
        public int Status { get; set; }

        /// <summary>
        /// 订单类型
        /// </summary>
        /// <remarks>
        /// 枚举：OrderType
        /// 1：普通、2：拼团、3：团购、4：秒杀、5：积分兑换、6：砍价、
        /// 7：赠品、8：接龙、10：微信交易组件
        /// </remarks>
        public int OrderType { get; set; }

        /// <summary>
        /// 收货方式
        /// </summary>
        /// <remarks>
        /// 枚举：OrderReceiptType
        /// 1：物流快递、2：同城配送、3：门店自提
        /// </remarks>
        public int ReceiptType { get; set; }

        /// <summary>
        /// 支付方式代码
        /// </summary>
        public string? PaymentCode { get; set; }

        /// <summary>
        /// 支付时间
        /// </summary>
        public DateTime? PaymentTime { get; set; }

        /// <summary>
        /// 配送方式ID 关联ship.id
        /// </summary>
        public Guid? LogisticsId { get; set; }

        /// <summary>
        /// 配送方式名称
        /// </summary>
        public string? LogisticsName { get; set; }

        /// <summary>
        /// 配送费用
        /// </summary>
        public decimal CostFreight { get; set; }

        /// <summary>
        /// 店铺ID
        /// </summary>
        public Guid SellerId { get; set; }

        /// <summary>
        /// 售后状态
        /// </summary>
        /// <remarks>
        /// 枚举：OrderConfirmStatus
        /// 1：未确认收货、2：已确认收货
        /// </remarks>
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
        public long ShipAreaId { get; set; }

        /// <summary>
        /// 显示区域信息
        /// </summary>
        public string? DisplayArea { get; set; }

        /// <summary>
        /// 收货详细地址
        /// </summary>
        public string ShipAddress { get; set; }

        /// <summary>
        /// 收货人姓名
        /// </summary>
        public string ShipName { get; set; }

        /// <summary>
        /// 收货电话
        /// </summary>
        public string ShipMobile { get; set; }

        /// <summary>
        /// 商品总重量
        /// </summary>
        public decimal Weight { get; set; }

        /// <summary>
        /// 使用积分
        /// </summary>
        public int Point { get; set; }

        /// <summary>
        /// 积分抵扣金额
        /// </summary>
        public decimal PointMoney { get; set; }

        /// <summary>
        /// 会员优惠
        /// </summary>
        public decimal GradeDiscountAmount { get; set; }

        /// <summary>
        /// 秒杀优惠
        /// </summary>
        public decimal SeckillDiscountAmount { get; set; }

        /// <summary>
        /// 订单优惠金额
        /// </summary>
        public decimal OrderDiscountAmount { get; set; }

        /// <summary>
        /// 商品优惠金额
        /// </summary>
        public decimal GoodsDiscountAmount { get; set; }

        /// <summary>
        /// 优惠券优惠额度
        /// </summary>
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
        public string? Memo { get; set; }

        /// <summary>
        /// 下单IP
        /// </summary>
        public string? Ip { get; set; }

        /// <summary>
        /// 卖家备注
        /// </summary>
        public string? Mark { get; set; }

        /// <summary>
        /// 订单来源
        /// </summary>
        /// <remarks>
        /// 枚举：Source
        /// 1：PC端、2：H5端、3：微信小程序端、4：支付宝小程序端、5：APP端
        /// </remarks>
        public int Source { get; set; }

        /// <summary>
        /// 是否评论
        /// </summary>
        public bool IsComment { get; set; }

        /// <summary>
        /// 关联营销类型对象序列
        /// </summary>
        public Guid? ObjectId { get; set; }

        /// <summary>
        /// 是否使用积分
        /// </summary>
        public bool IsUsePoint { get; set; }

        /// <summary>
        /// 购物车ID
        /// </summary>
        public Guid[] CartIds { get; set; }

        /// <summary>
        /// 订单明细
        /// </summary>
        public List<OrderItemCreateEto> OrderItems { get; set; }
    }

    /// <summary>
    /// 创建订单明细Dto
    /// </summary>
    public class OrderItemCreateEto : EntityEto
    {
        /// <summary>
        /// 订单ID 关联order.id
        /// </summary>
        public Guid OrderId { get; set; }

        /// <summary>
        /// 商品ID 关联goods.id
        /// </summary>
        public Guid GoodId { get; set; }

        /// <summary>
        /// 货品ID 关联products.id
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
        /// 货品明细序列号存储
        /// </summary>
        public string? Addon { get; set; }
    }
}