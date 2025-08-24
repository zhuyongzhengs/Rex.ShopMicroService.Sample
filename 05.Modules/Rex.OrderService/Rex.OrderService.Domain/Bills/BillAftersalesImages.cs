using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace Rex.OrderService.Bills
{
    /// <summary>
    /// 退货单图片关联表
    /// </summary>
    public class BillAftersalesImages : FullAuditedAggregateRoot<Guid>, IMultiTenant
    {
        /// <summary>
        /// 租户ID
        /// </summary>
        public Guid? TenantId { get; set; }

        /// <summary>
        /// 售后单id
        /// </summary>
        [Required]
        [StringLength(50)]
        public Guid AftersalesId { get; set; }

        /// <summary>
        /// 售后单
        /// </summary>
        [ForeignKey(nameof(AftersalesId))]
        public BillAftersales? Aftersales { get; set; }

        /// <summary>
        /// 图片地址
        /// </summary>
        [Required]
        [StringLength(255)]
        public string ImageUrl { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        [Display(Name = "排序")]
        [Required]
        public int Sort { get; set; }
    }
}