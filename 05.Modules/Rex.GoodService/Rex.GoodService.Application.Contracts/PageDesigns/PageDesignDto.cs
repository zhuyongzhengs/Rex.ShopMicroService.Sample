using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.MultiTenancy;

namespace Rex.GoodService.PageDesigns
{
    /// <summary>
    /// 页面设计Dto
    /// </summary>
    public class PageDesignDto : EntityDto<Guid>, IMultiTenant
    {
        /// <summary>
        /// 租户ID
        /// </summary>
        public Guid? TenantId { get; set; }

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
        /// 并发(控制)戳
        /// </summary>
        public string ConcurrencyStamp { get; set; }

        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime? CreationTime { get; set; }
    }
}