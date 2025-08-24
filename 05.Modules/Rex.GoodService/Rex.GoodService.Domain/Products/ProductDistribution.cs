using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace Rex.GoodService.Products
{
    /// <summary>
    /// 货品三级佣金表
    /// </summary>
    public partial class ProductDistribution : FullAuditedAggregateRoot<Guid>, IMultiTenant
    {
        /// <summary>
        /// 租户ID
        /// </summary>
        public Guid? TenantId { get; set; }

        /// <summary>
        /// 货品ID
        /// </summary>
        [Required]
        public Guid ProductId { get; set; }

        /*
        /// <summary>
        /// 货品信息
        /// </summary>
        [ForeignKey(nameof(ProductId))]
        public Product Product { get; set; }
        */

        /// <summary>
        /// 货品货号
        /// </summary>
        [Required]
        [StringLength(50)]
        public string ProductSn { get; set; }

        /// <summary>
        /// 一级佣金
        /// </summary>
        [Required]
        public decimal LevelOne { get; set; }

        /// <summary>
        /// 二级佣金
        /// </summary>
        [Required]
        public decimal LevelTwo { get; set; }

        /// <summary>
        /// 三级佣金
        /// </summary>
        [Required]
        public decimal LevelThree { get; set; }
    }
}