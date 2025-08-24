using Volo.Abp.Application.Dtos;

namespace Rex.FrontAggregationService.Orders
{
    /// <summary>
    /// 订单确认Dto
    /// </summary>
    public class OrderConfirmDto : EntityDto
    {
        /// <summary>
        /// 购物车ID
        /// </summary>
        public Guid[] CartIds { get; set; }

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
        /// 收货方式：1.物流快递、2.同城配送、3.门店自提
        /// </summary>
        public int ReceiptType { get; set; }

        /// <summary>
        /// 支付方式代码
        /// </summary>
        public string? PaymentCode { get; set; }

        /// <summary>
        /// 区域ID
        /// </summary>
        public int ShipAreaId { get; set; }

        /// <summary>
        /// 优惠劵ID
        /// </summary>
        public Guid[]? CouponIds { get; set; }

        /// <summary>
        /// 是否使用积分
        /// </summary>
        public bool IsUsePoint { get; set; }

        /// <summary>
        /// 用户(收货)地址ID
        /// </summary>
        public Guid? UserShipId { get; set; }

        /// <summary>
        /// 关联营销类型对象序列
        /// </summary>
        public Guid? ObjectId { get; set; }

        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderNo { get; set; }

        /// <summary>
        /// 买家备注
        /// </summary>
        public string? Memo { get; set; }
    }
}