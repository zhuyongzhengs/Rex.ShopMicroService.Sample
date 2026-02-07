using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace Rex.GoodService.Products
{
    /// <summary>
    /// 货品表
    /// </summary>
    public partial class Product : FullAuditedAggregateRoot<Guid>, IMultiTenant
    {
        public Product()
        {
        }

        public Product(Guid id)
        {
            Id = id;
        }

        /// <summary>
        /// 租户ID
        /// </summary>
        public Guid? TenantId { get; set; }

        /// <summary>
        /// 商品ID
        /// </summary>
        [Required]
        public Guid GoodId { get; set; }

        /*
        /// <summary>
        /// 商品
        /// </summary>
        [ForeignKey(nameof(GoodId))]
        public Good Good { get; set; }
        */

        /// <summary>
        /// 商品条码
        /// </summary>
        [StringLength(128)]
        public string BarCode { get; set; }

        /// <summary>
        /// 货品编码
        /// </summary>
        [StringLength(30)]
        public string Sn { get; set; }

        /// <summary>
        /// 货品价格
        /// </summary>
        [Required]
        public decimal Price { get; set; }

        /// <summary>
        /// 货品成本价
        /// </summary>
        [Required]
        public decimal CostPrice { get; set; }

        /// <summary>
        /// 货品市场价
        /// </summary>
        [Required]
        public decimal MktPrice { get; set; }

        /// <summary>
        /// 是否上架
        /// </summary>
        [Required]
        public bool Marketable { get; set; }

        /// <summary>
        /// 重量(千克)
        /// </summary>
        [Required]
        public decimal Weight { get; set; }

        /// <summary>
        /// 库存
        /// </summary>
        [Required]
        public int Stock { get; set; }

        /// <summary>
        /// 冻结库存
        /// </summary>
        [Required]
        public int FreezeStock { get; set; }

        /// <summary>
        /// 规格值
        /// </summary>
        public string? SpesDesc { get; set; }

        /// <summary>
        /// 是否默认货品
        /// </summary>
        [Required]
        public bool IsDefault { get; set; }

        /// <summary>
        /// 规格图片
        /// </summary>
        public string? Images { get; set; }

        /// <summary>
        /// 货品分佣
        /// </summary>
        public ProductDistribution? ProductDistribution { get; set; }
    }
}