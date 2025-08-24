using Volo.Abp.Application.Dtos;

namespace Rex.GoodService.Notices
{
    /// <summary>
    /// 创建公告Dto
    /// </summary>
    public partial class NoticeCreateDto : EntityDto
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
    }
}