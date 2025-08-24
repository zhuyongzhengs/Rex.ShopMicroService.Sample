using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;

namespace Rex.GoodService.Goods
{
    /// <summary>
    /// 商品价格修改Dto
    /// </summary>
    public class GoodPriceUpdateDto : EntityDto
    {
        /// <summary>
        /// 商品ID
        /// </summary>
        public List<Guid> GoodIds { get; set; }

        /// <summary>
        /// 变更方式：+、-、*、=
        /// </summary>
        public string ModifyType { get; set; }

        /// <summary>
        /// 变更类型
        /// </summary>
        public string PriceType { get; set; }

        /// <summary>
        /// 金额或倍数
        /// </summary>
        public decimal PriceValue { get; set; }
    }
}