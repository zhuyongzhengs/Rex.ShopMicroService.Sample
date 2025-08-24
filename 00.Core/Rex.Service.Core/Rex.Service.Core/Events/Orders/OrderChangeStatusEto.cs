using System;
using Volo.Abp.Domain.Entities.Events.Distributed;

namespace Rex.Service.Core.Events.Orders
{
    /// <summary>
    /// 订单状态变更Eto
    /// </summary>
    public class OrderChangeStatusEto : EntityEto
    {
        /// <summary>
        /// 事件名称
        /// </summary>
        public const string EventName = "Change.Order.Status";

        /// <summary>
        /// 订单ID
        /// </summary>
        public Guid[] OrderIds { get; set; }

        /// <summary>
        /// 支付编码
        /// </summary>
        public string PaymentCode { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }
    }
}