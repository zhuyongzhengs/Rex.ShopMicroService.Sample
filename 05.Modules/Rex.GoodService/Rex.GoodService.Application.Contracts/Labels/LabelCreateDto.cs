using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;

namespace Rex.GoodService.Labels
{
    /// <summary>
    /// 创建标签Dto
    /// </summary>
    public partial class LabelCreateDto : EntityDto
    {
        /// <summary>
        /// 标签名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 标签样式
        /// </summary>
        public string Style { get; set; }
    }
}