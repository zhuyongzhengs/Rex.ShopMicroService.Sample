using Volo.Abp.Application.Dtos;

namespace Rex.GoodService.Notices
{
    /// <summary>
    /// 修改公告Dto
    /// </summary>
    public class NoticeUpdateDto : EntityDto
    {
        /// <summary>
        /// 公告标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 公告内容
        /// </summary>
        public string ContentBody { get; set; }

        /// <summary>
        /// 公告类型
        /// </summary>
        public int? Type { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int? Sort { get; set; }

        /// <summary>
        /// 并发(控制)戳
        /// </summary>
        public string ConcurrencyStamp { get; set; }
    }
}