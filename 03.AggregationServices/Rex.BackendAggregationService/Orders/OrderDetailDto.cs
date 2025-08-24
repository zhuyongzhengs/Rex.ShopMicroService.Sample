using Rex.OrderService.Bills;
using Rex.OrderService.Orders;
using Rex.PaymentService.Payments;
using Rex.PromotionService.Promotions;
using Rex.Service.Core.Configurations;
using Volo.Abp.Application.Dtos;

namespace Rex.BackendAggregationService.Orders
{
    /// <summary>
    /// 订单详情Dto
    /// </summary>
    public class OrderDetailDto : EntityDto<Guid>
    {
        /// <summary>
        /// 订单号
        /// </summary>
        public string No { get; set; }

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
        /// 显示订单来源
        /// </summary>
        public string OrderTypeDisplay
        {
            get
            {
                return this.OrderType.GetDescription<GlobalEnums.OrderType>();
            }
        }

        /// <summary>
        /// 订单来源
        /// </summary>
        /// <remarks>
        /// 枚举：Source
        /// 1：PC端、2：H5端、3：微信小程序端、4：支付宝小程序端、5：APP端
        /// </remarks>
        public int Source { get; set; }

        /// <summary>
        /// 显示订单来源
        /// </summary>
        public string SourceDisplay
        {
            get
            {
                return this.Source.GetDescription<GlobalEnums.Source>();
            }
        }

        /// <summary>
        /// 支付方式代码
        /// </summary>
        public string? PaymentCode { get; set; }

        /// <summary>
        /// 订单状态
        /// </summary>
        /// <remarks>
        /// 枚举：OrderStatus
        /// 1：订单正常、2：订单完成、3：订单取消
        /// </remarks>
        public int Status { get; set; }

        /// <summary>
        /// 显示订单状态
        /// </summary>
        public string StatusDisplay
        {
            get
            {
                return this.Status.GetDescription<GlobalEnums.OrderStatus>();
            }
        }

        /// <summary>
        /// 发货状态
        /// </summary>
        /// <remarks>
        /// 枚举：OrderShipStatus
        /// 1：未发货、2：部分发货、3：已发货、4：部分退货、5：已退货
        /// </remarks>
        public int ShipStatus { get; set; }

        /// <summary>
        /// 显示发货状态
        /// </summary>
        public string ShipStatusDisplay
        {
            get
            {
                return this.ShipStatus.GetDescription<GlobalEnums.OrderShipStatus>();
            }
        }

        /// <summary>
        /// 配送方式ID
        /// </summary>
        public Guid? LogisticsId { get; set; }

        /// <summary>
        /// 配送方式名称
        /// </summary>
        public string? LogisticsName { get; set; }

        /// <summary>
        /// 商品总重量
        /// </summary>
        public decimal Weight { get; set; }

        /// <summary>
        /// 商品总价
        /// </summary>
        public decimal GoodAmount { get; set; }

        /// <summary>
        /// 配送费用
        /// </summary>
        public decimal CostFreight { get; set; }

        /// <summary>
        /// 已支付的金额
        /// </summary>
        public decimal PayedAmount { get; set; }

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
        /// 积分抵扣金额
        /// </summary>
        public decimal PointMoney { get; set; }

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
        /// 显示发货状态
        /// </summary>
        public string PayStatusDisplay
        {
            get
            {
                return this.PayStatus.GetDescription<GlobalEnums.OrderPayStatus>();
            }
        }

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
        /// 售后状态
        /// </summary>
        /// <remarks>
        /// 枚举：OrderConfirmStatus
        /// 1：未确认收货、2：已确认收货
        /// </remarks>
        public int ConfirmStatus { get; set; }

        /// <summary>
        /// 显示售后状态
        /// </summary>
        public string ConfirmStatusDisplay
        {
            get
            {
                return this.ConfirmStatus.GetDescription<GlobalEnums.OrderConfirmStatus>();
            }
        }

        /// <summary>
        /// 确认收货时间
        /// </summary>
        public DateTime? ConfirmTime { get; set; }

        /// <summary>
        /// 买家备注
        /// </summary>
        public string? Memo { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// 用户名称
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 用户昵称
        /// </summary>
        public string NickName { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// 用户等级
        /// </summary>
        public string UserGradeName { get; set; }

        /// <summary>
        /// 卖家备注
        /// </summary>
        public string? Mark { get; set; }

        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime? CreationTime { get; set; }

        /// <summary>
        /// 订单明细
        /// </summary>
        public List<OrderItemDto> OrderItems { get; set; } = new();

        /// <summary>
        /// 支付单
        /// </summary>
        public List<BillPaymentDto> BillPayments { get; set; } = new();

        /// <summary>
        /// 退款单
        /// </summary>
        public List<BillRefundDto> BillRefunds { get; set; } = new();

        /// <summary>
        /// 发货单
        /// </summary>
        public List<BillDeliveryDto> BillDeliverys { get; set; } = new();

        /// <summary>
        /// 退货单
        /// </summary>
        public List<BillReshipDto> BillReships { get; set; } = new();

        /// <summary>
        /// 优惠劵
        /// </summary>
        public List<CouponDto> Coupons { get; set; } = new();

        /// <summary>
        /// 订单记录
        /// </summary>
        public List<OrderLogDto> OrderLogs { get; set; } = new();
    }
}