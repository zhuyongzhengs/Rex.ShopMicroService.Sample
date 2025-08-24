using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;

namespace Rex.GoodService.Goods
{
    /// <summary>
    /// 生成商品类型规格Dto
    /// </summary>
    public class GoodTypeSpecGenerateDto : EntityDto<Guid>
    {
        /// <summary>
        /// 规格名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 商品类型规格值
        /// </summary>
        public List<GoodTypeSpecValueGenerateDto> SpecValues { get; set; } = new();
    }

    /// <summary>
    /// 商品类型规格值
    /// </summary>
    public class GoodTypeSpecValueGenerateDto : EntityDto<Guid>
    {
        public Guid SpecId { get; set; }

        /// <summary>
        /// 属性值
        /// </summary>
        public string Value { get; set; }
    }

    /// <summary>
    /// 商品类型规格
    /// </summary>
    public class GoodTypeSpecValueModel
    {
        /// <summary>
        /// 规格名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 属性值
        /// </summary>
        public string Value { get; set; }
    }
}