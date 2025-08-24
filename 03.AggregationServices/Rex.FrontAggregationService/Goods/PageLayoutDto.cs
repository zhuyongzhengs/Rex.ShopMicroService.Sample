using Rex.GoodService.PageDesigns;
using Volo.Abp.Application.Dtos;

namespace Rex.FrontAggregationService.Goods
{
    /// <summary>
    /// 页面布局Dto
    /// </summary>
    public class PageLayoutDto : EntityDto<Guid>
    {
        /// <summary>
        /// 可视化区域编码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 可编辑区域名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string? Desc { get; set; }

        /// <summary>
        /// 布局样式编码
        /// </summary>
        /// <remarks>
        /// 1、手机端
        /// 2、PC端
        /// </remarks>
        public int? Layout { get; set; }

        /// <summary>
        /// 是否默认
        /// </summary>
        /// <remarks>
        /// 1、是  2、否
        /// </remarks>
        public int? Type { get; set; }

        /// <summary>
        /// 页面布局项
        /// </summary>
        public List<PageDesignItemDto> Items { get; set; } = new();
    }
}