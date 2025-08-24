using System;
using Volo.Abp.Application.Dtos;

namespace Rex.GoodService.Products
{
    /// <summary>
    /// 商品库存调整Dto
    /// </summary>
    public class ProductDetailUpdateDto : EntityDto<Guid>
    {
        /// <summary>
        /// 货品库存
        /// </summary>
        public int Stock { get; set; }

        /// <summary>
        /// 货品价格
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// 货品成本价
        /// </summary>
        public decimal CostPrice { get; set; }

        /// <summary>
        /// 货品市场价
        /// </summary>
        public decimal MktPrice { get; set; }

        /// <summary>
        /// 重量(千克)
        /// </summary>
        public decimal Weight { get; set; }

        /// <summary>
        /// 是否默认货品
        /// </summary>
        public bool IsDefault { get; set; }
    }
}