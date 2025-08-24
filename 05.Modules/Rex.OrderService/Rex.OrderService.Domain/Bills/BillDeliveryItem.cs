using Rex.OrderService.Orders;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace Rex.OrderService.Bills
{
    /// <summary>
    /// 发货单明细
    /// </summary>
    public class BillDeliveryItem : FullAuditedAggregateRoot<Guid>, IMultiTenant
    {
        /// <summary>
        /// 租户ID
        /// </summary>
        public Guid? TenantId { get; set; }

        /// <summary>
        /// 订单编号
        /// </summary>
        [Required]
        public Guid OrderId { get; set; }

        /// <summary>
        /// 发货单ID
        /// </summary>
        [Required]
        public Guid DeliveryId { get; set; }

        /// <summary>
        /// 发货单
        /// </summary>
        [ForeignKey(nameof(DeliveryId))]
        public BillDelivery? BillDelivery { get; set; }

        /// <summary>
        /// 商品ID
        /// </summary>
        [Required]
        public Guid GoodId { get; set; }

        /// <summary>
        /// 货品ID
        /// </summary>
        [Required]
        public Guid ProductId { get; set; }

        /// <summary>
        /// 货品编码
        /// </summary>
        [Required]
        [StringLength(30)]
        public string Sn { get; set; }

        /// <summary>
        /// 商品编码
        /// </summary>
        [Required]
        [StringLength(30)]
        public string Bn { get; set; }

        /// <summary>
        /// 商品名称
        /// </summary>
        [Display(Name = "商品名称")]
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// 发货数量
        /// </summary>
        [Required]
        public int Nums { get; set; }

        /// <summary>
        /// 重量
        /// </summary>
        [Required]
        public decimal Weight { get; set; }

        /// <summary>
        /// 货品明细序列号存储
        /// </summary>
        public string? Addon { get; set; }
    }
}