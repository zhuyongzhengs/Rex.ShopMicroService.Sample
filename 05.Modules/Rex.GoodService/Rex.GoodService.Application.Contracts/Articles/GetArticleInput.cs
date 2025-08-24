using System;
using Volo.Abp.Application.Dtos;

namespace Rex.GoodService.Articles
{
    /// <summary>
    /// 查询文章
    /// </summary>
    public class GetArticleInput : PagedAndSortedResultRequestDto
    {
        /// <summary>
        /// 标题
        /// </summary>
        public string? Title { get; set; }

        /// <summary>
        /// 分类id
        /// </summary>
        public Guid? TypeId { get; set; }

        /// <summary>
        /// 是否发布
        /// </summary>
        public bool? IsPub { get; set; }
    }
}