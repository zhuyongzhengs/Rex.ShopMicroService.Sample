using Volo.Abp.Application.Dtos;

namespace Rex.GoodService.PageDesigns
{
    /// <summary>
    /// 修改页面设计Dto
    /// </summary>
    public class PageDesignUpdateDto : EntityDto
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
        public string? Description { get; set; }

        /// <summary>
        /// 布局样式编码：1、手机端
        /// </summary>
        public int? Layout { get; set; }

        /// <summary>
        /// 是否默认
        /// </summary>
        /// <remarks>
        /// 1、是  2、否
        /// </remarks>
        public int? Type { get; set; }

        /// <summary>
        /// 并发(控制)戳
        /// </summary>
        public string ConcurrencyStamp { get; set; }
    }
}