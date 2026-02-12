using System;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities.Events.Distributed;

namespace Rex.Service.Core.Events.Goods
{
    /// <summary>
    /// 创建库存操作Eto
    /// </summary>
    public class StockCreateEto : EntityEto
    {
        /// <summary>
        /// 事件名称
        /// </summary>
        public const string EventName = "Create.Stock";

        /// <summary>
        /// 库存信息
        /// </summary>
        public List<StockDetailEto> StockDetails { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark { get; set; }
    }

    /// <summary>
    /// 库存明细
    /// </summary>
    public class StockDetailEto : EntityEto
    {
        /// <summary>
        /// 来源ID
        /// </summary>
        public Guid SourceId { get; set; }

        /// <summary>
        /// 操作类型
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// 操作员ID
        /// </summary>
        public Guid OperatorId { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string? Memo { get; set; }
    }
}