using Volo.Abp.Application.Dtos;

namespace Rex.GoodService.ServiceGoods
{
    /// <summary>
    /// 查询服务商品
    /// </summary>
    public class GetServiceGoodInput : PagedAndSortedResultRequestDto
    {
        /// <summary>
        /// 标题名称
        /// </summary>
        public string? Title { get; set; }

        /// <summary>
        /// 项目状态
        /// </summary>
        public int? Status { get; set; }

        /// <summary>
        /// 核销有效期类型
        /// </summary>
        public int? ValidityType { get; set; }
    }
}