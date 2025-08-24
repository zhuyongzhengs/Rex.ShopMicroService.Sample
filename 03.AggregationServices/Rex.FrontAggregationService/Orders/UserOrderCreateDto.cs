using Volo.Abp.Application.Dtos;

namespace Rex.FrontAggregationService.Orders
{
    /// <summary>
    /// 创建用户订单Dto
    /// </summary>
    public class UserOrderCreateDto : EntityDto
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
        /// 自提门店ID，null就是不门店自提
        /// </summary>
        public Guid? StoreId { get; set; }

        /// <summary>
        /// 收货地区ID
        /// </summary>
        public long ShipAreaId { get; set; }

        /// <summary>
        /// 使用积分
        /// </summary>
        public int Point { get; set; }

        /// <summary>
        /// 优惠券信息
        /// </summary>
        public string? Coupon { get; set; }

        /// <summary>
        /// 优惠劵码
        /// </summary>
        public List<string> CouponCodes { get; set; } = new();

        /// <summary>
        /// 买家备注
        /// </summary>
        public string? Memo { get; set; }

        /// <summary>
        /// 关联营销类型对象序列
        /// </summary>
        public Guid? ObjectId { get; set; }

        /// <summary>
        /// 用户(收货)地址ID
        /// </summary>
        public Guid? UserShipId { get; set; }

        /// <summary>
        /// 提货人姓名
        /// </summary>
        public string? LadingName { get; set; }

        /// <summary>
        /// 提货人联系方式
        /// </summary>
        public string? LadingMobile { get; set; }
    }
}