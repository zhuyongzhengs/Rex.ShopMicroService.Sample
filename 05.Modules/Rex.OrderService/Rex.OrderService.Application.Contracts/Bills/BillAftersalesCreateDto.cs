using System;
using Volo.Abp.Application.Dtos;

namespace Rex.OrderService.Bills
{
    /// <summary>
    /// 创建售后单Dto
    /// </summary>
    public class BillAftersalesCreateDto : EntityDto
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
        /// 订单明细ID
        /// </summary>
        public Guid[] OrderItemIds { get; set; }

        /// <summary>
        /// 图片凭证
        /// </summary>
        public string[]? Images { get; set; }

        /// <summary>
        /// 退款原因
        /// </summary>
        public string Reason { get; set; }
    }
}