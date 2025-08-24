using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.MultiTenancy;

namespace Rex.GoodService.Articles
{
    /// <summary>
    /// 文章Dto
    /// </summary>
    public class ArticleDto : EntityDto<Guid>, IMultiTenant
    {
        /// <summary>
        /// 租户ID
        /// </summary>
        public Guid? TenantId { get; set; }

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
        /// 文章分类
        /// </summary>
        public ArticleTypeDto? ArticleType { get; set; }

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

        /// <summary>
        /// 并发(控制)戳
        /// </summary>
        public string ConcurrencyStamp { get; set; }

        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime? CreationTime { get; set; }
    }
}