using Rex.OrderService.Orders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace Rex.OrderService.Bills
{
    /// <summary>
    /// 退货单表
    /// </summary>
    public class BillReship : FullAuditedAggregateRoot<Guid>, IMultiTenant
    {
        /// <summary>
        /// 租户ID
        /// </summary>
        public Guid? TenantId { get; set; }

        /// <summary>
        /// 退货单号
        /// </summary>
        [Required]
        [StringLength(20)]
        public string No { get; set; }

        /// <summary>
        /// 订单序列
        /// </summary>
        [Required]
        [StringLength(50)]
        public Guid OrderId { get; set; }

        /// <summary>
        /// 订单
        /// </summary>
        [ForeignKey(nameof(OrderId))]
        public Order? Order { get; set; }

        /// <summary>
        /// 售后单序列
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
        /// 用户ID
        /// </summary>
        [Required]
        public Guid UserId { get; set; }

        /// <summary>
        /// 物流公司编码
        /// </summary>
        [StringLength(50)]
        public string? LogiCode { get; set; }

        /// <summary>
        /// 物流单号
        /// </summary>
        [StringLength(50)]
        public string? LogiNo { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        [Required]
        public int Status { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [StringLength(255)]
        public string? Memo { get; set; }

        /// <summary>
        /// 退货单明细
        /// </summary>
        public List<BillReshipItem> BillReshipItems { get; set; } = new();
    }
}