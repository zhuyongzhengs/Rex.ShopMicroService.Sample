using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace Rex.OrderService.Bills
{
    /// <summary>
    /// 退货单明细表
    /// </summary>
    public class BillReshipItem : FullAuditedAggregateRoot<Guid>, IMultiTenant
    {
        /// <summary>
        /// 租户ID
        /// </summary>
        public Guid? TenantId { get; set; }

        /// <summary>
        /// 退货单id
        /// </summary>
        [Required]
        [StringLength(50)]
        public Guid ReshipId { get; set; }

        /// <summary>
        /// 退货单
        /// </summary>
        [ForeignKey(nameof(ReshipId))]
        public BillReship? Reship { get; set; }

        /// <summary>
        /// 订单明细ID 关联order_items.id
        /// </summary>
        [Required]
        public Guid OrderItemId { get; set; }

        /// <summary>
        /// 商品ID 关联goods.id
        /// </summary>
        [Required]
        public Guid GoodId { get; set; }

        /// <summary>
        /// 货品ID 关联products.id
        /// </summary>
        [Required]
        public Guid ProductId { get; set; }

        /// <summary>
        /// 货品编码
        /// </summary>
        [StringLength(50)]
        [Required]
        public string Sn { get; set; }

        /// <summary>
        /// 商品编码
        /// </summary>
        [StringLength(50)]
        [Required]
        public string Bn { get; set; }

        /// <summary>
        /// 商品名称
        /// </summary>
        [StringLength(200)]
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// 图片
        /// </summary>
        [StringLength(255)]
        public string? ImageUrl { get; set; }

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