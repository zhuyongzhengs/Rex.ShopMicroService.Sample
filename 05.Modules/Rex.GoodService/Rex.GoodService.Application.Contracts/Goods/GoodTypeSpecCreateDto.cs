using System.Collections.Generic;
using Volo.Abp.Application.Dtos;

namespace Rex.GoodService.Goods
{
    /// <summary>
    /// 创建商品参数Dto
    /// </summary>
    public class GoodTypeSpecCreateDto : EntityDto
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
        /// 商品类型规格值
        /// </summary>
        public List<GoodTypeSpecValueCreateDto> SpecValues { get; set; } = new();
    }
}