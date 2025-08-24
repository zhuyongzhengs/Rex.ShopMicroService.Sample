using System;
using Volo.Abp.Application.Dtos;

namespace Rex.GoodService.Forms
{
    /// <summary>
    /// 创建表单项Dto
    /// </summary>
    public class FormItemCreateDto : EntityDto
    {
        /// <summary>
        /// 租户ID
        /// </summary>
        public Guid? TenantId { get; set; }

        /// <summary>
        /// 字段名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 字段类型
        /// </summary>
        public int? Type { get; set; }

        /// <summary>
        /// 验证类型
        /// </summary>
        public int? ValidationType { get; set; }

        /// <summary>
        /// 表单值
        /// </summary>
        public string? Value { get; set; }

        /// <summary>
        /// 默认值
        /// </summary>
        public string? DefaultValue { get; set; }

        /// <summary>
        /// 表单id
        /// </summary>
        public Guid? FormId { get; set; }

        /// <summary>
        /// 是否必填
        /// </summary>
        public bool Required { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }
    }
}