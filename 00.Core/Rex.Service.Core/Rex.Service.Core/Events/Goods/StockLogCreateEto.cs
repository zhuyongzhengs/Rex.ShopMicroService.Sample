using System;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities.Events.Distributed;

namespace Rex.Service.Core.Events.Goods
{
    /// <summary>
    /// 创建库存记录Eto
    /// </summary>
    public class StockLogCreateEto : EntityEto
    {
        /// <summary>
        /// 事件名称
        /// </summary>
        public const string EventName = "Create.Stock.Log";

        /// <summary>
        /// 库存记录信息
        /// </summary>
        public List<StockLogDetailEto> StockLogDetails { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark { get; set; }
    }

    /// <summary>
    /// 库存记录明细
    /// </summary>
    public class StockLogDetailEto : EntityEto
    {
        /// <summary>
        /// 来源ID
        /// </summary>
        public Guid SourceId { get; set; }

        /// <summary>
        /// 商品ID
        /// </summary>
        public Guid GoodId { get; set; }

        /// <summary>
        /// 货品ID
        /// </summary>
        public Guid ProductId { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public int Nums { get; set; }

        /// <summary>
        /// 货品编码
        /// </summary>
        public string Sn { get; set; }

        /// <summary>
        /// 商品条码
        /// </summary>
        public string Bn { get; set; }

        /// <summary>
        /// 商品名称
        /// </summary>
        public string GoodName { get; set; }

        /// <summary>
        /// 货品明细序列
        /// </summary>
        public string? SpesDesc { get; set; }
    }
}