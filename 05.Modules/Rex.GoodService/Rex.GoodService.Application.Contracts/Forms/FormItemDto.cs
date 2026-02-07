using System;
using Volo.Abp.Application.Dtos;

namespace Rex.GoodService.Forms
{
    /// <summary>
    /// 表单项Dto
    /// </summary>
    public class FormItemDto : EntityDto<Guid>
    {
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
        public Guid FormId { get; set; }

        /// <summary>
        /// 表单
        /// </summary>
        public FormDto? Form { get; set; }

        /// <summary>
        /// 是否必填
        /// </summary>
        public bool Required { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }

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