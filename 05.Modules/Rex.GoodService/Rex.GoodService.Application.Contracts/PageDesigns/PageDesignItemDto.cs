using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.MultiTenancy;

namespace Rex.GoodService.PageDesigns
{
    /// <summary>
    /// 页面设计项Dto
    /// </summary>
    public class PageDesignItemDto : EntityDto<Guid>, IMultiTenant
    {
        /// <summary>
        /// 租户ID
        /// </summary>
        public Guid? TenantId { get; set; }

        /// <summary>
        /// 组件编码
        /// </summary>
        public string WidgetCode { get; set; }

        /// <summary>
        /// 页面编码
        /// </summary>
        public string PageCode { get; set; }

        /// <summary>
        /// 布局位置
        /// </summary>
        public int PositionId { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }

        /// <summary>
        /// 组件配置内容
        /// </summary>
        public object Parameters { get; set; }

        /// <summary>
        /// 并发(控制)戳
        /// </summary>
        public string? ConcurrencyStamp { get; set; }
    }
}