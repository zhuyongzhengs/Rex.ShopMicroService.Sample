using System.Collections.Generic;

namespace Rex.GoodService.Goods
{
    /// <summary>
    /// 商品分类树形Dto
    /// </summary>
    public class GoodCategoryTreeDto : GoodCategoryDto
    {
        /// <summary>
        /// 商品分类下级
        /// </summary>
        public List<GoodCategoryTreeDto> Children { get; set; } = new();
    }
}