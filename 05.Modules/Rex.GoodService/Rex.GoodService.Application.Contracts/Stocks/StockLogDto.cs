using System;
using Volo.Abp.Application.Dtos;

namespace Rex.GoodService.Stocks
{
    /// <summary>
    /// 库存记录Dto
    /// </summary>
    public class StockLogDto : EntityDto<Guid>
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