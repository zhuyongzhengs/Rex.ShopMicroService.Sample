using System;
using System.Collections.Generic;

namespace Rex.OrderService.Bills
{
    /// <summary>
    /// 根据订单明细ID获取售后单明细
    /// </summary>
    public class GetBillAftersalesItemToOrderItemIdInput
    {
        /// <summary>
        /// 订单明细ID
        /// </summary>
        public List<Guid> OrderItemIds { get; set; }

        /// <summary>
        /// 售后单状态
        /// </summary>
        public List<int>? Status { get; set; }
    }
}