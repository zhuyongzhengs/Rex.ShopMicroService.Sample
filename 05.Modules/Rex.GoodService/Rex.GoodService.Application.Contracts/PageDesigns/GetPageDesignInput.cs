using Volo.Abp.Application.Dtos;

namespace Rex.GoodService.PageDesigns
{
    /// <summary>
    /// 查询页面设计
    /// </summary>
    public class GetPageDesignInput : PagedAndSortedResultRequestDto
    {
        /// <summary>
        /// 可视化区域编码
        /// </summary>
        public string? Code { get; set; }

        /// <summary>
        /// 可编辑区域名称
        /// </summary>
        public string? Name { get; set; }
    }
}