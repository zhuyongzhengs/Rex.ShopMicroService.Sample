using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace Rex.GoodService.Articles
{
    /// <summary>
    /// 文章
    /// </summary>
    public class Article : FullAuditedAggregateRoot<Guid>, IMultiTenant
    {
        /// <summary>
        /// 租户ID
        /// </summary>
        public Guid? TenantId { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        [Required]
        [StringLength(200)]
        public string Title { get; set; }

        /// <summary>
        /// 简介
        /// </summary>
        [StringLength(150)]
        public string? Brief { get; set; }

        /// <summary>
        /// 封面图
        /// </summary>
        [StringLength(255)]
        public string? CoverImage { get; set; }

        /// <summary>
        /// 文章内容
        /// </summary>
        [Required]
        public string ContentBody { get; set; }

        /// <summary>
        /// 分类id
        /// </summary>
        [Required]
        public Guid TypeId { get; set; }

        /// <summary>
        /// 文章分类
        /// </summary>
        [ForeignKey(nameof(TypeId))]
        public ArticleType? ArticleType { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        [Required]
        public int Sort { get; set; }

        /// <summary>
        /// 是否发布
        /// </summary>
        [Required]
        public bool IsPub { get; set; }

        /// <summary>
        /// 访问量
        /// </summary>
        [Required]
        public int Pv { get; set; }
    }
}