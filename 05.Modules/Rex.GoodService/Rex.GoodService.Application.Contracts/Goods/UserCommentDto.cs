using System;
using Volo.Abp.Application.Dtos;

namespace Rex.GoodService.Goods
{
    /// <summary>
    /// 用户评价Dto
    /// </summary>
    public class UserCommentDto : EntityDto
    {
        /// <summary>
        /// 商品链接
        /// </summary>
        public string ImageUrl { get; set; }

        /// <summary>
        /// 商品名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 商品数量
        /// </summary>
        public int Nums { get; set; }

        /// <summary>
        /// 商品单价
        /// </summary>
        public int Amount { get; set; }

        /// <summary>
        /// 用户ID
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
        /// 货品规格序列号存储
        /// </summary>
        public string? Addon { get; set; }

        /// <summary>
        /// 评价1-5星
        /// </summary>
        public int Score { get; set; }

        /// <summary>
        /// 图片
        /// </summary>
        public string? Images { get; set; }

        /// <summary>
        /// 评价内容
        /// </summary>
        public string? ContentBody { get; set; }
    }
}