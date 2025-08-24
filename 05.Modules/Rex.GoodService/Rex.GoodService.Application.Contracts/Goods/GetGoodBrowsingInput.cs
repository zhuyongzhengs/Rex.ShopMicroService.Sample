using Volo.Abp.Application.Dtos;

namespace Rex.GoodService.Goods
{
    /// <summary>
    /// 查询(用户)商品浏览记录
    /// </summary>
    public class GetGoodBrowsingInput : PagedAndSortedResultRequestDto
    {
        /// <summary>
        /// 商品名称
        /// </summary>
        public string? GoodName { get; set; }
    }
}