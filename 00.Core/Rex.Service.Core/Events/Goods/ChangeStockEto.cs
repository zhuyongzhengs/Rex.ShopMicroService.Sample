using System;
using Volo.Abp.Domain.Entities.Events.Distributed;

namespace Rex.Service.Core.Events.Goods
{
    /// <summary>
    /// 库存变更Eto
    /// </summary>
    public class ChangeStockEto : EntityEto
    {
        /// <summary>
        /// 事件名称
        /// </summary>
        public const string EventName = "Change.Product.Stock";

        /// <summary>
        /// 货品ID
        /// </summary>
        public Guid ProductId { get; set; }

        /// <summary>
        /// 库存变更类型
        /// </summary>
        /// <remarks>
        /// 枚举：OrderChangeStockType
        /// 1：下单、2：发货、3：退款、4：退货、5：取消订单、6：完成订单
        /// </remarks>
        public int ChangeStockType { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public int Nums { get; set; }
    }
}