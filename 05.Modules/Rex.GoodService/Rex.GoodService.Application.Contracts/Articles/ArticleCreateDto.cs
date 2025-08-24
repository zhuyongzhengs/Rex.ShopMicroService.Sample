using System;
using Volo.Abp.Application.Dtos;

namespace Rex.GoodService.Articles
{
    /// <summary>
    /// 创建文章Dto
    /// </summary>
    public class ArticleCreateDto : EntityDto
    {
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 简介
        /// </summary>
        public string? Brief { get; set; }

        /// <summary>
        /// 封面图
        /// </summary>
        public string? CoverImage { get; set; }

        /// <summary>
        /// 文章内容
        /// </summary>
        public string ContentBody { get; set; }

        /// <summary>
        /// 分类id
        /// </summary>
        public Guid TypeId { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }

        /// <summary>
        /// 是否发布
        /// </summary>
        public bool IsPub { get; set; }

        /// <summary>
        /// 访问量
        /// </summary>
        public int Pv { get; set; }
    }
}