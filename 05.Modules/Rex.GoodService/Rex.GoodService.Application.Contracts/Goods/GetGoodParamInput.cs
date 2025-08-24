using Volo.Abp.Application.Dtos;

namespace Rex.GoodService.Goods
{
    /// <summary>
    /// 查询商品参数
    /// </summary>
    public class GetGoodParamInput : PagedAndSortedResultRequestDto
    {
        /// <summary>
        /// 参数名称
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// 参数类型
        /// </summary>
        public string? Type { get; set; }
    }
}