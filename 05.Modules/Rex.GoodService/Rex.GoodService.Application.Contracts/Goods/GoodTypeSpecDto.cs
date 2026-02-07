using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;

namespace Rex.GoodService.Goods
{
    /// <summary>
    /// 商品类型规格Dto
    /// </summary>
    public class GoodTypeSpecDto : EntityDto<Guid>
    {
        /// <summary>
        /// 规格名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 规格排序
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

        /// <summary>
        /// 商品类型规格值
        /// </summary>
        public List<GoodTypeSpecValueDto> SpecValues { get; set; } = new();
    }
}