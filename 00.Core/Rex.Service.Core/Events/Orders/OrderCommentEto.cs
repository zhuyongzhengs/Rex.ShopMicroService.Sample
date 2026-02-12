using System;
using Volo.Abp.Domain.Entities.Events.Distributed;

namespace Rex.Service.Core.Events.Orders
{
    /// <summary>
    /// 订单评价
    /// </summary>
    public class OrderCommentEto : EntityEto
    {
        /// <summary>
        /// 事件名称
        /// </summary>
        public const string EventName = "Update.Order.Comment";

        /// <summary>
        /// 订单ID
        /// </summary>
        public Guid OrderId { get; set; }

        /// <summary>
        /// 评价类型
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// 是否评价
        /// </summary>
        public bool IsComment { get; set; }
    }
}