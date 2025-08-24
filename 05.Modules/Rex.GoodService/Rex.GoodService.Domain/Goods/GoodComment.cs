using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace Rex.GoodService.Goods
{
    /// <summary>
    /// 商品评价表
    /// </summary>
    public class GoodComment : FullAuditedAggregateRoot<Guid>, IMultiTenant
    {
        /// <summary>
        /// 租户ID
        /// </summary>
        public Guid? TenantId { get; set; }

        /// <summary>
        /// 父级评价ID
        /// </summary>
        public Guid? CommentId { get; set; }

        /// <summary>
        /// 评价1-5星
        /// </summary>
        [Required]
        public int Score { get; set; }

        /// <summary>
        /// 评价用户ID
        /// </summary>
        [Required]
        public Guid UserId { get; set; }

        /// <summary>
        /// 用户名称
        /// </summary>
        [StringLength(100)]
        public string? UserName { get; set; }

        /// <summary>
        /// 用户头像
        /// </summary>
        [StringLength(1000)]
        public string? Avatar { get; set; }

        /// <summary>
        /// 商品ID
        /// </summary>
        [Required]
        public Guid GoodId { get; set; }

        /// <summary>
        /// 评价订单ID
        /// </summary>
        [Required]
        [StringLength(50)]
        public Guid OrderId { get; set; }

        /// <summary>
        /// 货品规格序列号存储
        /// </summary>
        public string? Addon { get; set; }

        /// <summary>
        /// 评价图片逗号分隔最多五张
        /// </summary>
        public string? Images { get; set; }

        /// <summary>
        /// 评价内容
        /// </summary>
        public string? ContentBody { get; set; }

        /// <summary>
        /// 商家回复
        /// </summary>
        public string? SellerContent { get; set; }

        /// <summary>
        /// 前台显示
        /// </summary>
        [Required]
        public bool IsDisplay { get; set; }
    }
}