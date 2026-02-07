using System;
using Volo.Abp.Application.Dtos;

namespace Rex.GoodService.Stocks
{
    /// <summary>
    /// 库存操作Dto
    /// </summary>
    public class StockDto : EntityDto<Guid>
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

        /// <summary>
        /// 并发(控制)戳
        /// </summary>
        public string ConcurrencyStamp { get; set; }

        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime? CreationTime { get; set; }
    }
}