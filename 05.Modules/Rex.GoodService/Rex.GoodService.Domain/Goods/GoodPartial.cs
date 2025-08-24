using System.ComponentModel.DataAnnotations.Schema;

namespace Rex.GoodService.Goods
{
    /// <summary>
    /// 商品
    /// </summary>
    public partial class Good
    {
        /// <summary>
        /// 销售价
        /// </summary>
        [NotMapped]
        public decimal Price { get; set; } = 0;

        /// <summary>
        /// 成本价
        /// </summary>
        [NotMapped]
        public decimal CostPrice { get; set; } = 0;

        /// <summary>
        /// 市场价
        /// </summary>
        [NotMapped]
        public decimal MktPrice { get; set; } = 0;
    }
}