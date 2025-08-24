using System;
using Volo.Abp.Application.Dtos;

namespace Rex.GoodService.Goods
{
    /// <summary>
    /// 商品评价Dto
    /// </summary>
    public class GoodCommentDto : EntityDto<Guid>
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
        public int Score { get; set; }

        /// <summary>
        /// 评价用户ID
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// 用户名称
        /// </summary>
        public string? UserName { get; set; }

        /// <summary>
        /// 用户头像
        /// </summary>
        public string? Avatar { get; set; }

        /// <summary>
        /// 商品ID
        /// </summary>
        public Guid GoodId { get; set; }

        /// <summary>
        /// 评价订单ID
        /// </summary>
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
        public bool IsDisplay { get; set; }

        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime? CreationTime { get; set; }
    }
}