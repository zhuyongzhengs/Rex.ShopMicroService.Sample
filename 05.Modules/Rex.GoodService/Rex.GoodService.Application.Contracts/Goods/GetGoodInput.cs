using System;
using Volo.Abp.Application.Dtos;

namespace Rex.GoodService.Goods
{
    /// <summary>
    /// 查询商品
    /// </summary>
    public class GetGoodInput : PagedAndSortedResultRequestDto
    {
        /// <summary>
        /// 商品名称
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// 商品分类
        /// </summary>
        public Guid? GoodCategoryId { get; set; }

        /// <summary>
        /// 品牌
        /// </summary>
        public Guid? BrandId { get; set; }

        /// <summary>
        /// 是否上架
        /// </summary>
        public bool? IsMarketable { get; set; }

        /// <summary>
        /// 是否库存报警
        /// </summary>
        public bool? IsStockWarn { get; set; }

        /// <summary>
        /// 库存警告数
        /// </summary>
        public int? GoodStockWarn { get; set; }
    }
}