using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;

namespace Rex.GoodService.Goods
{
    /// <summary>
    /// 查询商品评价
    /// </summary>
    public class GetGoodCommentInput : PagedAndSortedResultRequestDto
    {
        /// <summary>
        /// 评价1-5星
        /// </summary>
        public int? Score { get; set; }

        /// <summary>
        /// 评价用户ID
        /// </summary>
        public Guid? UserId { get; set; }

        /// <summary>
        /// 商品ID
        /// </summary>
        public Guid? GoodId { get; set; }

        /// <summary>
        /// 评价订单ID
        /// </summary>
        public Guid? OrderId { get; set; }

        /// <summary>
        /// 前台显示
        /// </summary>
        public bool? IsDisplay { get; set; }
    }
}