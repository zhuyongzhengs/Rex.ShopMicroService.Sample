using Volo.Abp.Application.Dtos;

namespace Rex.OrderService.Logisticss
{
    /// <summary>
    /// 查询物流
    /// </summary>
    public class GetLogisticsInput : PagedAndSortedResultRequestDto
    {
        /// <summary>
        /// 物流公司编码
        /// </summary>
        public string? LogiCode { get; set; }

        /// <summary>
        /// 物流公司名称
        /// </summary>
        public string? LogiName { get; set; }

        /// <summary>
        /// 物流电话
        /// </summary>
        public string? Phone { get; set; }
    }
}