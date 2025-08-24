using Volo.Abp.Application.Dtos;

namespace Rex.GoodService.Goods
{
    /// <summary>
    /// 商品数量Dto
    /// </summary>
    public class GoodCountDto : EntityDto
    {
        /// <summary>
        /// 全部商品数量
        /// </summary>
        public long AllCount { get; set; }

        /// <summary>
        /// 上架数量
        /// </summary>
        public long UpMarketableCount { get; set; }

        /// <summary>
        /// 下架数量
        /// </summary>
        public long DownMarketableCount { get; set; }

        /// <summary>
        /// 库存报警数量
        /// </summary>
        public long StockWarningCount { get; set; }
    }
}