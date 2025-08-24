using System;
using Volo.Abp.Application.Dtos;

namespace Rex.GoodService.Articles
{
    /// <summary>
    /// 修改文章分类Dto
    /// </summary>
    public class ArticleTypeUpdateDto : EntityDto
    {
        /// <summary>
        /// 分类名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 父id
        /// </summary>
        public Guid? ParentId { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }

        /// <summary>
        /// 并发(控制)戳
        /// </summary>
        public string ConcurrencyStamp { get; set; }
    }
}