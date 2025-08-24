using Volo.Abp.Application.Dtos;

namespace Rex.GoodService.Goods
{
    /// <summary>
    /// 查询商品分类
    /// </summary>
    public class GetGoodCategoryInput : PagedAndSortedResultRequestDto
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// 是否显示
        /// </summary>
        public bool? IsShow { get; set; }
    }
}