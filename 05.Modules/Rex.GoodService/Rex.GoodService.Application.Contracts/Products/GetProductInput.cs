using Volo.Abp.Application.Dtos;

namespace Rex.GoodService.Products
{
    /// <summary>
    /// 查询货品
    /// </summary>
    public class GetProductInput : PagedAndSortedResultRequestDto
    {
        /// <summary>
        /// 商品条码
        /// </summary>
        public string? BarCode { get; set; }

        /// <summary>
        /// 货品编码
        /// </summary>
        public string? Sn { get; set; }
    }
}