using Rex.OrderService.Bills;
using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;

namespace Rex.OrderService.Orders
{
    /// <summary>
    /// 用户订单Dto
    /// </summary>
    public class UserOrderDto : EntityDto<Guid>
    {
        /// <summary>
        /// 订单号
        /// </summary>
        public string No { get; set; }

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
        /// 售后状态
        /// 枚举：OrderConfirmStatus
        /// 1：未确认收货、2：已确认收货
        /// </summary>
        public int ConfirmStatus { get; set; }

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
        /// 支付方式代码
        /// </summary>
        public string? PaymentCode { get; set; }

        /// <summary>
        /// 支付时间
        /// </summary>
        public DateTime? PaymentTime { get; set; }

        /// <summary>
        /// 是否评论
        /// </summary>
        public bool IsComment { get; set; }

        /// <summary>
        /// 是否能发起售后
        /// </summary>
        public bool IsAftersalesStatus { get; set; }

        /// <summary>
        /// 配送费用
        /// </summary>
        public decimal CostFreight { get; set; }

        /// <summary>
        /// 积分抵扣金额
        /// </summary>
        public decimal PointMoney { get; set; }

        /// <summary>
        /// 会员优惠
        /// </summary>
        public decimal GradeDiscountAmount { get; set; }

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
        /// 创建日期
        /// </summary>
        public DateTime? CreationTime { get; set; }

        /// <summary>
        /// 商品(件)数量
        /// </summary>
        public decimal ProductNums { get; set; }

        /// <summary>
        /// 订单明细
        /// </summary>
        public List<UserOrderItemDto> OrderItems { get; set; } = new();

        /// <summary>
        /// 售后单
        /// </summary>
        public List<BillAftersalesDto>? Aftersales { get; set; }
    }
}