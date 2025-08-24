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
    /// 售后单表
    /// </summary>
    public class BillAftersales : FullAuditedAggregateRoot<Guid>, IMultiTenant
    {
        /// <summary>
        /// 租户ID
        /// </summary>
        public Guid? TenantId { get; set; }

        /// <summary>
        /// 售后单号
        /// </summary>
        [Required]
        public string No { get; set; }

        /// <summary>
        /// 订单ID
        /// </summary>
        [Required]
        public Guid OrderId { get; set; }

        /// <summary>
        /// 订单
        /// </summary>
        [ForeignKey(nameof(OrderId))]
        public Order? Order { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        [Required]
        public Guid UserId { get; set; }

        /// <summary>
        /// 售后类型
        /// </summary>
        [Required]
        public int Type { get; set; }

        /// <summary>
        /// 退款金额
        /// </summary>
        [Required]
        public decimal RefundAmount { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        [Required]
        public int Status { get; set; }

        /// <summary>
        /// 退款原因
        /// </summary>
        [Required]
        [StringLength(255)]
        public string Reason { get; set; }

        /// <summary>
        /// 卖家备注
        /// </summary>
        [StringLength(255)]
        public string? Mark { get; set; }

        /// <summary>
        /// 售后单明细
        /// </summary>
        public List<BillAftersalesItem> BillAftersalesItems { get; set; } = new();

        /// <summary>
        /// 售后单图片
        /// </summary>
        public List<BillAftersalesImages> BillAftersalesImagess { get; set; } = new();
    }
}