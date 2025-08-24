namespace Rex.PromotionService.PinTuans
{
    /// <summary>
    /// 拼团商品Dto
    /// </summary>
    public partial class PinTuanGoodDto
    {
        /// <summary>
        /// 商品名称
        /// </summary>
        public string? GoodName { get; set; }

        /// <summary>
        /// 缩略图
        /// </summary>
        public string? GoodImage { get; set; }

        /// <summary>
        /// 图集
        /// </summary>
        public string? GoodImages { get; set; }

        /// <summary>
        /// 销售价
        /// </summary>
        public decimal GoodPrice { get; set; } = 0;
    }
}