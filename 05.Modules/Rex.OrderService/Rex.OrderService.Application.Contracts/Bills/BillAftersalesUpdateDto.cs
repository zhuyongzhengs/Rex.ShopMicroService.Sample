using System;
using Volo.Abp.Application.Dtos;

namespace Rex.OrderService.Bills
{
    /// <summary>
    /// 修改售后单Dto
    /// </summary>
    public class BillAftersalesUpdateDto : EntityDto
    {
        /// <summary>
        /// 订单ID
        /// </summary>
        public Guid OrderId { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// 售后类型
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// 退款金额
        /// </summary>
        public decimal RefundAmount { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 退款原因
        /// </summary>
        public string Reason { get; set; }

        /// <summary>
        /// 卖家备注
        /// </summary>
        public string? Mark { get; set; }

        /// <summary>
        /// 并发(控制)戳
        /// </summary>
        public string ConcurrencyStamp { get; set; }
    }
}