using System;
using Volo.Abp.Domain.Entities.Events.Distributed;

namespace Rex.Service.Core.Events
{
    /// <summary>
    /// 测试订单发消息给商品
    /// </summary>
    public class OrderToGoodEto : EntityEto<Guid>
    {
        /// <summary>
        /// 事件名称
        /// </summary>
        public const string EventName = "Test.Order.To.Good.Msg";

        /// <summary>
        /// 事件回调名称
        /// </summary>
        public const string EventCallbackName = "Callback:Test.Order.To.Good.Msg";

        /// <summary>
        /// 是否异常
        /// </summary>
        public bool IsException { get; set; }

        /// <summary>
        /// 订单消息
        /// </summary>
        public string OrderMsg { get; set; }

        /// <summary>
        /// 商品消息
        /// </summary>
        public string GoodMsg { get; set; }
    }
}