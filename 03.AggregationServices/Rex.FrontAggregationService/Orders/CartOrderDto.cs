using Rex.BaseService.Systems.SysUsers;
using Rex.PromotionService.Promotions;
using Volo.Abp.Application.Dtos;

namespace Rex.FrontAggregationService.Orders
{
    /// <summary>
    /// 购物车订单
    /// </summary>
    public class CartOrderDto : EntityDto
    {
        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderNo { get; set; }

        /// <summary>
        /// 购物车列表
        /// </summary>
        public List<ShoppingCartDto> ShoppingCarts { get; set; }

        /// <summary>
        /// 商品总额
        /// </summary>
        public decimal GoodAmount { get; set; }

        /// <summary>
        /// 订单金额
        /// </summary>
        public decimal OrderAmount { get; set; }

        /// <summary>
        /// 会员优惠
        /// </summary>
        public decimal GoodGradeMoney { get; set; }

        /// <summary>
        /// 商品优惠
        /// </summary>
        public decimal GoodPromotionMoney { get; set; }

        /// <summary>
        /// 秒杀优惠
        /// </summary>
        public decimal GoodSeckillMoney { get; set; }

        /// <summary>
        /// 订单优惠
        /// </summary>
        public decimal OrderPromotionMoney { get; set; }

        /// <summary>
        /// 优惠券抵扣
        /// </summary>
        public decimal CouponPromotionMoney { get; set; }

        /// <summary>
        /// 积分抵扣
        /// </summary>
        public decimal PointExchangeMoney { get; set; }

        /// <summary>
        /// 运费
        /// </summary>
        public decimal CostFreight { get; set; }

        /// <summary>
        /// 商品(件)数量
        /// </summary>
        public decimal ProductNums { get; set; }

        /// <summary>
        /// 总重量
        /// </summary>
        public decimal Weight { get; set; }

        /// <summary>
        /// 买家留言
        /// </summary>
        public string? Memo { get; set; }

        /// <summary>
        /// 用户可用积分
        /// </summary>
        public UserPointExchangeDto? PointExchangeItem { get; set; }

        /// <summary>
        /// 用户(可用)优惠券
        /// </summary>
        public List<UserCouponExchangeDto> CouponExchanges { get; set; } = new();
    }
}