using Volo.Abp.Application.Dtos;

namespace Rex.GoodService.Brands
{
    /// <summary>
    /// 查询品牌
    /// </summary>
    public class GetBrandInput : PagedAndSortedResultRequestDto
    {
        /// <summary>
        /// 品牌名称
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// 是否显示
        /// </summary>
        public bool? IsShow { get; set; }
    }
}