using Rex.FrontAggregationService.Goods;
using Rex.GoodService.Goods;
using Volo.Abp.Application.Dtos;

namespace Rex.FrontAggregationService.Orders
{
    /// <summary>
    /// 购物车货品Dto
    /// </summary>
    public class CartProductDto : EntityDto<Guid>
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// 货品ID
        /// </summary>
        public Guid ProductId { get; set; }

        /// <summary>
        /// 商品数量
        /// </summary>
        public int Nums { get; set; } = 1;

        /// <summary>
        /// 是否收藏
        /// </summary>
        public bool IsCollection { get; set; }

        /// <summary>
        /// 是否选中
        /// </summary>
        public bool IsChecked { get; set; }

        /// <summary>
        /// 订单类型
        /// </summary>
        /// <remarks>
        /// 枚举：OrderType
        /// 1：普通、2：拼团、3：团购、4：秒杀、5：积分兑换、6：砍价、
        /// 7：赠品、8：接龙、10：微信交易组件
        /// </remarks>
        public int Type { get; set; } = 1;

        /// <summary>
        /// 重量
        /// </summary>
        public decimal Weight { get; set; } = 0;

        /// <summary>
        /// 货品信息
        /// </summary>
        public ProductInfoDto Product { get; set; } = new();

        /// <summary>
        /// 商品信息
        /// </summary>
        public GoodDto Good { get; set; } = new();
    }
}