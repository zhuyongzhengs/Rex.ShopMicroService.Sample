using Rex.GoodService.Articles;
using Rex.GoodService.Brands;
using Rex.GoodService.Goods;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;

namespace Rex.GoodService.PageDesigns
{
    /// <summary>
    /// 版面设计Dto
    /// </summary>
    public class LayoutDesignDto : EntityDto
    {
        /// <summary>
        /// 页面设计
        /// </summary>
        public PageDesignDto PageDesign { get; set; }

        /// <summary>
        /// 品牌
        /// </summary>
        public List<BrandDto> Brands { get; set; } = new();

        /// <summary>
        /// 商品分类
        /// </summary>
        public List<GoodCategoryTreeDto> GoodCategoryTrees { get; set; } = new();

        /// <summary>
        /// 文章分类
        /// </summary>
        public List<ArticleTypeDto> ArticleTypes { get; set; } = new();
    }
}