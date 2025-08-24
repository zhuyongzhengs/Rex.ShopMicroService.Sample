using Volo.Abp.Application.Dtos;

namespace Rex.GoodService.ServiceDescriptions
{
    /// <summary>
    /// 查询商城服务描述
    /// </summary>
    public class GetServiceDescriptionInput : PagedAndSortedResultRequestDto
    {
        /// <summary>
        /// 商城服务描述名称
        /// </summary>
        public string? Title { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        public int? Type { get; set; }

        /// <summary>
        /// 是否展示
        /// </summary>
        public bool? IsShow { get; set; }
    }
}