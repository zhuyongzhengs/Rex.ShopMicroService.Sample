using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities;
using Volo.Abp.MultiTenancy;

namespace Rex.GoodService.PageDesigns
{
    /// <summary>
    /// 页面设计项
    /// </summary>
    public class PageDesignItem : Entity<Guid>, IMultiTenant, IHasConcurrencyStamp
    {
        public PageDesignItem()
        { }

        public PageDesignItem(Guid id)
        {
            this.Id = id;
        }

        /// <summary>
        /// 租户ID
        /// </summary>
        public Guid? TenantId { get; set; }

        /// <summary>
        /// 组件编码
        /// </summary>
        [StringLength(50)]
        public string WidgetCode { get; set; }

        /// <summary>
        /// 页面编码
        /// </summary>
        [StringLength(50)]
        public string PageCode { get; set; }

        /// <summary>
        /// 布局位置
        /// </summary>
        [Required]
        public int PositionId { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        [Required]
        public int Sort { get; set; }

        /// <summary>
        /// 组件配置内容
        /// </summary>
        public string Parameters { get; set; }

        /// <summary>
        /// 并发(控制)戳
        /// </summary>
        public string ConcurrencyStamp { get; set; }
    }
}