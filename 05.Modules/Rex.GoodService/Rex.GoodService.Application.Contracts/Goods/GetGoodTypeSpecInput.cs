using Volo.Abp.Application.Dtos;

namespace Rex.GoodService.Goods
{
    /// <summary>
    /// 查询商品类型规格
    /// </summary>
    public class GetGoodTypeSpecInput : PagedAndSortedResultRequestDto
    {
        /// <summary>
        /// 参数名称
        /// </summary>
        public string? Name { get; set; }
    }
}