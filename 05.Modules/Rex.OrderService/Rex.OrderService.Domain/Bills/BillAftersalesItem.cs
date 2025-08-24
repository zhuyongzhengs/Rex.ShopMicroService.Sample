using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace Rex.OrderService.Bills
{
    /// <summary>
    /// 售后单明细表
    /// </summary>
    public class BillAftersalesItem : FullAuditedAggregateRoot<Guid>, IMultiTenant
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
        /// 订单明细ID
        /// </summary>
        [Required]
        public Guid OrderItemId { get; set; }

        /// <summary>
        /// 商品ID
        /// </summary>
        [Required]
        public Guid GoodId { get; set; }

        /// <summary>
        /// 货品ID\
        /// </summary>
        [Required]
        public Guid ProductId { get; set; }

        /// <summary>
        /// 货品编码
        /// </summary>
        [StringLength(30)]
        public string? Sn { get; set; }

        /// <summary>
        /// 商品编码
        /// </summary>
        [StringLength(30)]
        public string? Bn { get; set; }

        /// <summary>
        /// 商品名称
        /// </summary>
        [StringLength(200)]
        public string? Name { get; set; }

        /// <summary>
        /// 图片
        /// </summary>
        [Required]
        [StringLength(255)]
        public string ImageUrl { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        [Required]
        public int Nums { get; set; }

        /// <summary>
        /// 货品明细序列号存储
        /// </summary>
        public string? Addon { get; set; }
    }
}