using System;
using Volo.Abp.Domain.Entities.Events.Distributed;

namespace Rex.Service.Core.Events.Orders
{
    /// <summary>
    /// 下单(取消)回滚Eto
    /// </summary>
    public class PlaceOrderCallbackEto : EntityEto<Guid>
    {
        /// <summary>
        /// 事件名称
        /// </summary>
        public const string EventName = "Place.Order.Callback";

        /// <summary>
        /// 说明
        /// </summary>
        public string? Explain { get; set; }
    }
}