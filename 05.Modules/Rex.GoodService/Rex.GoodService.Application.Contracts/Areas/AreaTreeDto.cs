using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;
using Volo.Abp.MultiTenancy;

namespace Rex.GoodService.Areas
{
    /// <summary>
    /// 区域树形Dto
    /// </summary>
    public class AreaTreeDto : EntityDto<long>
    {
        /// <summary>
        /// 父级ID
        /// </summary>
        public long? ParentId { get; set; }

        /// <summary>
        /// 地区深度
        /// </summary>
        public int? Depth { get; set; }

        /// <summary>
        /// 地区名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 邮编
        /// </summary>
        public string? PostalCode { get; set; }

        /// <summary>
        /// 地区排序
        /// </summary>
        public int Sort { get; set; }

        /// <summary>
        /// 叶子节点的标志
        /// </summary>
        public bool Leaf
        {
            get
            {
                return this.Depth.HasValue && this.Depth.Value > 2;
            }
        }

        /// <summary>
        /// 并发(控制)戳
        /// </summary>
        public string ConcurrencyStamp { get; set; }

        /// <summary>
        /// 区域下级
        /// </summary>
        public List<AreaTreeDto> Children { get; set; } = new();
    }
}