using Volo.Abp.Application.Dtos;

namespace Rex.GoodService.Stores
{
    /// <summary>
    /// 查询门店
    /// </summary>
    public class GetStoreInput : PagedAndSortedResultRequestDto
    {
        /// <summary>
        /// 门店名称
        /// </summary>
        public string? StoreName { get; set; }
    }
}