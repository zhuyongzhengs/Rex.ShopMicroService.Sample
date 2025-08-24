using System;
using Volo.Abp.Application.Dtos;

namespace Rex.OrderService.Orders
{
    /// <summary>
    /// 修改订单Dto
    /// </summary>
    public class OrderUpdateDto : EntityDto<Guid>
    {
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
        /// 用户ID
        /// </summary>
        public Guid UserId { get; set; }

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
        /// 并发(控制)戳
        /// </summary>
        public string ConcurrencyStamp { get; set; }
    }
}