using Volo.Abp.Application.Dtos;

namespace Rex.GoodService.Areas
{
    /// <summary>
    /// 查询区域
    /// </summary>
    public class GetAreaInput : PagedAndSortedResultRequestDto
    {
        /// <summary>
        /// 父级ID
        /// </summary>
        public long? ParentId { get; set; }

        /// <summary>
        /// 地区名称
        /// </summary>
        public string? Name { get; set; }
    }
}