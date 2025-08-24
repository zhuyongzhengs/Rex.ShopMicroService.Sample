using Volo.Abp.Application.Dtos;

namespace Rex.FrontAggregationService.Orders
{
    /// <summary>
    /// 下(秒杀)订单Dto
    /// </summary>
    public class PlaceSeckillOrderDto_ : EntityDto
    {
        /// <summary>
        /// 订单请求(ID)键
        /// </summary>
        public string RequestIdKey { get; set; }

        /// <summary>
        /// 单品限流访问量限制Key
        /// </summary>
        public string OrderRateLimitKey { get; set; }

        /// <summary>
        /// 单品限流请求数
        /// </summary>
        public int OrderRateLimitCount { get; set; }

        /// <summary>
        /// 单品限流请求过期时间
        /// </summary>
        public int OrderRateLimitExpire { get; set; }

        /// <summary>
        /// 商品库存Key
        /// </summary>
        public string ProductStockKey { get; set; }

        /// <summary>
        /// 购买(商品)数量
        /// </summary>
        public int Nums { get; set; }

        /// <summary>
        /// 用户购买(商品)限制
        /// </summary>
        /// <remarks>
        /// 0：表示不限制
        /// </remarks>
        public int UserMaxNums { get; set; }

        /// <summary>
        /// 购买商品总量Key
        /// </summary>
        public string TotalBuyNumsKey { get; set; }

        /// <summary>
        /// 商品总量
        /// </summary>
        /// <remarks>
        /// 0：表示不限制，以商品库存为准
        /// </remarks>
        public int MaxGoodNums { get; set; }

        /// <summary>
        /// 用户购买数量Key
        /// </summary>
        public string UserBuyNumsKey { get; set; }
    }
}