using Volo.Abp.Application.Dtos;

namespace Rex.OrderService.Ships
{
    /// <summary>
    /// 查询配送方式
    /// </summary>
    public class GetShipInput : PagedAndSortedResultRequestDto
    {
        /// <summary>
        /// 配送方式名称
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// 物料公司编码
        /// </summary>
        public string? LogiCode { get; set; }

        /// <summary>
        /// 物料公司名称
        /// </summary>
        public string? LogiName { get; set; }
    }
}