using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace Rex.GoodService.Forms
{
    /// <summary>
    /// 表单项
    /// </summary>
    public class FormItem : FullAuditedAggregateRoot<Guid>, IMultiTenant
    {
        /// <summary>
        /// 租户ID
        /// </summary>
        public Guid? TenantId { get; set; }

        /// <summary>
        /// 字段名称
        /// </summary>
        [StringLength(50)]
        public string Name { get; set; }

        /// <summary>
        /// 字段类型
        /// </summary>
        [StringLength(30)]
        public int? Type { get; set; }

        /// <summary>
        /// 验证类型
        /// </summary>
        [StringLength(30)]
        public int? ValidationType { get; set; }

        /// <summary>
        /// 表单值
        /// </summary>
        [StringLength(255)]
        public string? Value { get; set; }

        /// <summary>
        /// 默认值
        /// </summary>
        [StringLength(255)]
        public string? DefaultValue { get; set; }

        /// <summary>
        /// 表单id
        /// </summary>
        [Required]
        public Guid FormId { get; set; }

        /// <summary>
        /// 表单
        /// </summary>
        [ForeignKey(nameof(FormId))]
        public Form? Form { get; set; }

        /// <summary>
        /// 是否必填
        /// </summary>
        [Required]
        public bool Required { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        [Required]
        public int Sort { get; set; }
    }
}