using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;

namespace Rex.GoodService.Goods
{
    /// <summary>
    /// 商品上架修改Dto
    /// </summary>
    public class GoodMarketableUpdateDto : EntityDto
    {
        /// <summary>
        /// 商品ID
        /// </summary>
        public List<Guid> GoodIds { get; set; }

        /// <summary>
        /// 是否上架
        /// </summary>
        public bool IsMarketable { get; set; }
    }
}