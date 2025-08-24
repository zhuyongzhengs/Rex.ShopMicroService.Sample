using System;
using Volo.Abp.Application.Dtos;

namespace Rex.GoodService.Products
{
    /// <summary>
    /// 创建货品Dto
    /// </summary>
    public partial class ProductCreateDto : EntityDto
    {
        /// <summary>
        /// 商品序列
        /// </summary>
        public Guid? GoodId { get; set; }

        /// <summary>
        /// 商品条码
        /// </summary>
        public string BarCode { get; set; }

        /// <summary>
        /// 货品编码
        /// </summary>
        public string Sn { get; set; }

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
        /// 是否上架
        /// </summary>
        public bool Marketable { get; set; }

        /// <summary>
        /// 重量(千克)
        /// </summary>
        public decimal Weight { get; set; }

        /// <summary>
        /// 库存
        /// </summary>
        public int Stock { get; set; }

        /// <summary>
        /// 冻结库存
        /// </summary>
        public int FreezeStock { get; set; }

        /// <summary>
        /// 规格值
        /// </summary>
        public string? SpesDesc { get; set; }

        /// <summary>
        /// 是否默认货品
        /// </summary>
        public bool IsDefault { get; set; }

        /// <summary>
        /// 规格图片
        /// </summary>
        public string? Images { get; set; }

        /// <summary>
        /// 一级佣金
        /// </summary>
        public decimal LevelOne { get; set; } = 0;

        /// <summary>
        /// 二级佣金
        /// </summary>
        public decimal LevelTwo { get; set; } = 0;

        /// <summary>
        /// 三级佣金
        /// </summary>
        public decimal LevelThree { get; set; } = 0;
    }
}