using System;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities.Events.Distributed;

namespace Rex.Service.Core.Events.Goods
{
    /// <summary>
    /// [订单]扣减库存Eto
    /// </summary>
    public class OrderDeductionProductStockEto : EntityEto<Guid>
    {
        /// <summary>
        /// 事件名称
        /// </summary>
        public const string EventName = "Order.Deduction.Product.Stock";

        /// <summary>
        /// 库存信息
        /// </summary>
        public List<ProductDeductionStockEto> StockDatas { get; set; }
    }

    /// <summary>
    /// 货品扣减Eto
    /// </summary>
    [Serializable]
    public class ProductDeductionStockEto : EntityEto
    {
        /// <summary>
        /// 货品ID
        /// </summary>
        public Guid ProductId { get; set; }

        /// <summary>
        /// 扣减数量
        /// </summary>
        public int Nums { get; set; }
    }
}