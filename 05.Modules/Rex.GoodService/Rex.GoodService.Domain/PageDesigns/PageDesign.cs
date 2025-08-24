using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace Rex.GoodService.PageDesigns
{
    /// <summary>
    /// 页面设计
    /// </summary>
    public class PageDesign : FullAuditedAggregateRoot<Guid>, IMultiTenant
    {
        /// <summary>
        /// 租户ID
        /// </summary>
        public Guid? TenantId { get; set; }

        /// <summary>
        /// 可视化区域编码
        /// </summary>
        [StringLength(50)]
        public string Code { get; set; }

        /// <summary>
        /// 可编辑区域名称
        /// </summary>
        [StringLength(100)]
        public string Name { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        [StringLength(255)]
        public string? Description { get; set; }

        /// <summary>
        /// 布局样式编码：1、手机端  2、PC端
        /// </summary>
        public int? Layout { get; set; }

        /// <summary>
        /// 是否默认
        /// </summary>
        /// <remarks>
        /// 1、是  2、否
        /// </remarks>
        public int? Type { get; set; }
    }
}