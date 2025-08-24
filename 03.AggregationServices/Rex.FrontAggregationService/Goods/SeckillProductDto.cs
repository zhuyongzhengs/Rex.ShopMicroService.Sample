using Volo.Abp.Application.Dtos;

namespace Rex.FrontAggregationService.Goods
{
    /// <summary>
    /// 秒杀货品信息
    /// </summary>
    public class SeckillProductDto : EntityDto<Guid>
    {
        /// <summary>
        /// 商品ID
        /// </summary>
        public Guid? GoodId { get; set; }

        /// <summary>
        /// 商品条码
        /// </summary>
        public string? BarCode { get; set; }

        /// <summary>
        /// 货品编码
        /// </summary>
        public string Sn { get; set; }

        /// <summary>
        /// 货品价格
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// 货品市场价
        /// </summary>
        public decimal MktPrice { get; set; }

        /// <summary>
        /// 是否上架
        /// </summary>
        public bool Marketable { get; set; }

        /// <summary>
        /// 重量(千克)
        /// </summary>
        public decimal Weight { get; set; }

        /// <summary>
        /// 库存
        /// </summary>
        public int Stock { get; set; }

        /// <summary>
        /// 冻结库存
        /// </summary>
        public int FreezeStock { get; set; }

        /// <summary>
        /// 规格值
        /// </summary>
        public string? SpesDesc { get; set; }

        /// <summary>
        /// 是否默认货品
        /// </summary>
        public bool IsDefault { get; set; }

        /// <summary>
        /// 规格图片
        /// </summary>
        public string? Images { get; set; }
    }
}