using System;
using Volo.Abp.Application.Dtos;

namespace Rex.GoodService.Goods
{
    /// <summary>
    /// 修改商品类型规格值Dto
    /// </summary>
    public partial class GoodTypeSpecValueUpdateDto : EntityDto<Guid>
    {
        /// <summary>
        /// 属性ID 关联goods_type_spec.id
        /// </summary>
        public Guid SpecId { get; set; }

        /// <summary>
        /// 属性值
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }
    }
}