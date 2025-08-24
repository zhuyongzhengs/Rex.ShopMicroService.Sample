using Volo.Abp.Application.Dtos;

namespace Rex.GoodService.Labels
{
    /// <summary>
    /// 查询标签
    /// </summary>
    public class GetLabelInput : PagedAndSortedResultRequestDto
    {
        /// <summary>
        /// 标签名称
        /// </summary>
        public string? Name { get; set; }
    }
}