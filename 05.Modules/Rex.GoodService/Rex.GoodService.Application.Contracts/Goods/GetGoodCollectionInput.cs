using Volo.Abp.Application.Dtos;

namespace Rex.GoodService.Goods
{
    /// <summary>
    /// 查询(用户)商品收藏
    /// </summary>
    public class GetGoodCollectionInput : PagedAndSortedResultRequestDto
    {
        /// <summary>
        /// 商品名称
        /// </summary>
        public string? GoodName { get; set; }
    }
}