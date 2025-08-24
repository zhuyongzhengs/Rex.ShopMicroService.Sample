using System;
using Volo.Abp.Application.Dtos;

namespace Rex.PromotionService.PinTuans
{
    /// <summary>
    /// 查询拼团记录
    /// </summary>
    public class GetPinTuanRecordInput : PagedAndSortedResultRequestDto
    {
        /// <summary>
        /// 团序列
        /// </summary>
        public Guid? TeamId { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public int? Status { get; set; }

        /// <summary>
        /// 订单序列
        /// </summary>
        public string? OrderId { get; set; }
    }
}