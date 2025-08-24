using Volo.Abp.Application.Dtos;

namespace Rex.GoodService.Notices
{
    /// <summary>
    /// 查询公告
    /// </summary>
    public class GetNoticeInput : PagedAndSortedResultRequestDto
    {
        /// <summary>
        /// 公告标题
        /// </summary>
        public string? Title { get; set; }
    }
}