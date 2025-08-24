using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Volo.Abp.Domain.Entities;
using Volo.Abp.MultiTenancy;

namespace Rex.GoodService.Goods
{
    /// <summary>
    /// 商品类型属性值表
    /// </summary>
    public partial class GoodTypeSpecValue : Entity<Guid>, IMultiTenant
    {
        /// <summary>
        /// 租户ID
        /// </summary>
        public Guid? TenantId { get; set; }

        /// <summary>
        /// 属性ID 关联goods_type_spec.id
        /// </summary>
        [Required]
        public Guid SpecId { get; set; }

        /// <summary>
        /// 商品类型属性
        /// </summary>
        [ForeignKey(nameof(SpecId))]
        public GoodTypeSpec GoodTypeSpec { get; set; }

        /// <summary>
        /// 属性值
        /// </summary>
        [Required]
        [StringLength(255)]
        public string Value { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        [Required]
        public int Sort { get; set; }
    }
}