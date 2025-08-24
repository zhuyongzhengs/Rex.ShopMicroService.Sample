using System;
using Volo.Abp.Application.Dtos;

namespace Rex.OrderService.Bills
{
    /// <summary>
    /// 查询用户售后单
    /// </summary>
    public class GetUserBillAftersalesInput : PagedAndSortedResultRequestDto
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// 售后单号
        /// </summary>
        public string? No { get; set; }

        /// <summary>
        /// 订单号
        /// </summary>
        public string? OrderNo { get; set; }

        /// <summary>
        /// 售后类型
        /// </summary>
        public int? Type { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public int? Status { get; set; }
    }
}