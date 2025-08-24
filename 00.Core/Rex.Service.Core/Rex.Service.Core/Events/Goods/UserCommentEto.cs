using System;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities.Events.Distributed;

namespace Rex.Service.Core.Events.Goods
{
    /// <summary>
    /// 用户评价Eto
    /// </summary>
    public class UserCommentEto : EntityEto
    {
        /// <summary>
        /// 事件名称
        /// </summary>
        public const string EventName = "User.Comment";

        /// <summary>
        /// 评价信息
        /// </summary>
        public List<UserCommentDetailEto> UserCommentDetails { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark { get; set; }
    }

    /// <summary>
    /// 用户评价明细
    /// </summary>
    public class UserCommentDetailEto : EntityEto
    {
        /// <summary>
        /// 订单ID
        /// </summary>
        public Guid OrderId { get; set; }

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
        public decimal Amount { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        public Guid UserId { get; set; }

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