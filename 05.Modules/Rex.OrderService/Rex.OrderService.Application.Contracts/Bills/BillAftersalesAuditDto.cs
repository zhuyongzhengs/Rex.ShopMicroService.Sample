using System;
using Volo.Abp.Application.Dtos;

namespace Rex.OrderService.Bills
{
    /// <summary>
    /// 审核售后单Dto
    /// </summary>
    public class BillAftersalesAuditDto : EntityDto
    {
        /// <summary>
        /// 订单明细ID
        /// </summary>
        public Guid[] OrderItemIds { get; set; }

        /// <summary>
        /// 退款金额
        /// </summary>
        public decimal RefundAmount { get; set; }

        /// <summary>
        /// 售后类型
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 卖家备注
        /// </summary>
        public string? Mark { get; set; }
    }
}