using Volo.Abp.Application.Dtos;

namespace Rex.FrontAggregationService.Orders
{
    /// <summary>
    /// 用户购物车Dto
    /// </summary>
    public class UserCartDto : EntityDto
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// 商品总金额
        /// </summary>
        public decimal GoodAmount { get; set; }

        /// <summary>
        /// 总金额
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// 订单促销金额
        /// </summary>
        /// <remarks>
        /// 单纯的订单促销的金额
        /// </remarks>
        public decimal OrderPromotionMoney { get; set; } = 0;

        /// <summary>
        /// 商品促销金额
        /// </summary>
        /// <remarks>
        /// 所有的商品促销的总计
        /// </remarks>
        public decimal GoodPromotionMoney { get; set; } = 0;

        /// <summary>
        /// 优惠券优惠金额
        /// </summary>
        public decimal CouponPromotionMoney { get; set; } = 0;

        ///<summary>
        /// 促销列表
        /// </summary>
        public Dictionary<Guid, WxNameTypeDto> PromotionList { get; set; } = new();

        /// <summary>
        /// 运费
        /// </summary>
        public decimal CostFreight { get; set; } = 0;

        /// <summary>
        /// 商品总重
        /// </summary>
        public decimal Weight { get; set; } = 0;

        /// <summary>
        /// 优惠券
        /// </summary>
        public List<string> Coupon { get; set; } = new();

        /// <summary>
        /// 购物车类型
        /// </summary>
        public int Type { get; set; } = 1;

        /// <summary>
        /// 积分
        /// </summary>
        public int Point { get; set; } = 0;

        /// <summary>
        /// 积分可以抵扣多少金额
        /// </summary>
        public int PointExchangeMoney { get; set; } = 0;

        /// <summary>
        /// 购物车商品
        /// </summary>
        public List<CartProductDto> CartProducts { get; set; } = new();
    }
}