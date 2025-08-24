using Rex.GoodService.Labels;
using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;

namespace Rex.GoodService.Goods
{
    /// <summary>
    /// 批量创建标签Dto
    /// </summary>
    public class GoodLabelManyDto : EntityDto
    {
        /// <summary>
        /// 商品ID
        /// </summary>
        public List<Guid> GoodIds { get; set; }

        /// <summary>
        /// (新增)标签
        /// </summary>
        public List<LabelCreateDto> Labels { get; set; } = new();
    }
}