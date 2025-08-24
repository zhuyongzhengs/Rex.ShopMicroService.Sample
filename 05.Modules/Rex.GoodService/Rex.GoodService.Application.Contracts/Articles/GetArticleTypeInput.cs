using System;
using Volo.Abp.Application.Dtos;

namespace Rex.GoodService.Articles
{
    /// <summary>
    /// 查询文章分类
    /// </summary>
    public class GetArticleTypeInput : PagedAndSortedResultRequestDto
    {
        /// <summary>
        /// 分类名称
        /// </summary>
        public string? Name { get; set; }
    }
}