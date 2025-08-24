using System;
using Volo.Abp.Application.Dtos;

namespace Rex.OrderService.Bills
{
    /// <summary>
    /// 查询退货单
    /// </summary>
    public class GetBillReshipInput : PagedAndSortedResultRequestDto
    {
        /// <summary>
        /// 退货单号
        /// </summary>
        public string? No { get; set; }

        /// <summary>
        /// 订单号
        /// </summary>
        public string? OrderNo { get; set; }

        /// <summary>
        /// 售后单号
        /// </summary>
        public string? AftersalesNo { get; set; }

        /// <summary>
        /// 物流单号
        /// </summary>
        public string? LogiNo { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public int? Status { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime? BeginTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? EndTime { get; set; }
    }
}