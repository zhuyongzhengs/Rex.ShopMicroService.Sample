using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;

namespace Rex.GoodService.Goods
{
    /// <summary>
    /// 商品库存修改Dto
    /// </summary>
    public class GoodStockUpdateDto : EntityDto
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
        /// 库存或倍数
        /// </summary>
        public int StockValue { get; set; }
    }
}