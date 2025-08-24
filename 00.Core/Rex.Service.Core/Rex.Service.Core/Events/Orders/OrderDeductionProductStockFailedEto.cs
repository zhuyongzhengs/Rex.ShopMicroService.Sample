using System;
using Volo.Abp.Domain.Entities.Events.Distributed;

namespace Rex.Service.Core.Events.Orders
{
    /// <summary>
    /// [订单]扣减库存失败Eto
    /// </summary>
    /// <remarks>
    /// 扣减库存失败：取消订单
    /// </remarks>
    public class OrderDeductionProductStockFailedEto : EntityEto<Guid>
    {
        /// <summary>
        /// 事件名称
        /// </summary>
        public const string EventName = "Order.Deduction.Product.Stock.Failed";

        /// <summary>
        /// 失败消息
        /// </summary>
        public string FailMsg { get; set; }
    }
}