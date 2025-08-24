using Volo.Abp.Application.Dtos;

namespace Rex.OrderService.Orders
{
    /// <summary>
    /// 查询用户订单
    /// </summary>
    public class GetUserOrderInput : PagedAndSortedResultRequestDto
    {
        /// <summary>
        /// 订单号
        /// </summary>
        public string? No { get; set; }

        /// <summary>
        /// 支付状态
        /// </summary>
        /// <remarks>
        /// 枚举：OrderPayStatus
        /// 1：未付款、2：已付款、3：部分付款、4：部分退款、5：已退款
        /// </remarks>
        public int? PayStatus { get; set; }

        /// <summary>
        /// 发货状态
        /// </summary>
        /// <remarks>
        /// 枚举：OrderShipStatus
        /// 1：未发货、2：部分发货、3：已发货、4：部分退货、5：已退货
        /// </remarks>
        public int? ShipStatus { get; set; }

        /// <summary>
        /// 订单状态
        /// </summary>
        /// <remarks>
        /// 枚举：OrderStatus
        /// 1：订单正常、2：订单完成、3：订单取消
        /// </remarks>
        public int? Status { get; set; }

        /// <summary>
        /// 订单类型
        /// </summary>
        /// <remarks>
        /// 枚举：OrderType
        /// 1：普通、2：拼团、3：团购、4：秒杀、5：积分兑换、6：砍价、
        /// 7：赠品、8：接龙、10：微信交易组件
        /// </remarks>
        public int? OrderType { get; set; }

        /// <summary>
        /// 收货方式
        /// </summary>
        /// <remarks>
        /// 枚举：OrderReceiptType
        /// 1：物流快递、2：同城配送、3：门店自提
        /// </remarks>
        public int? ReceiptType { get; set; }

        /// <summary>
        /// 售后状态
        /// 枚举：OrderConfirmStatus
        /// 1：未确认收货、2：已确认收货
        /// </summary>
        public int? ConfirmStatus { get; set; }

        /// <summary>
        /// 是否评论
        /// </summary>
        public bool? IsComment { get; set; }
    }
}