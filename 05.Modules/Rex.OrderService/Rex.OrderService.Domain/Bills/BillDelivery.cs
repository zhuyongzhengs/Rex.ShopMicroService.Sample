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
    /// 发货单表
    /// </summary>
    public class BillDelivery : FullAuditedAggregateRoot<Guid>, IMultiTenant
    {
        /// <summary>
        /// 租户ID
        /// </summary>
        public Guid? TenantId { get; set; }

        /// <summary>
        /// 发货单号
        /// </summary>
        [Required]
        public string No { get; set; }

        /// <summary>
        /// 订单号
        /// </summary>
        [Required]
        public Guid OrderId { get; set; }

        /// <summary>
        /// 订单
        /// </summary>
        [ForeignKey(nameof(OrderId))]
        public Order? Order { get; set; }

        /// <summary>
        /// 物流公司编码
        /// </summary>
        [StringLength(50)]
        public string? LogiCode { get; set; }

        /// <summary>
        /// 物流公司名称
        /// </summary>
        [StringLength(100)]
        public string? LogiName { get; set; }

        /// <summary>
        /// 物流单号
        /// </summary>
        [StringLength(50)]
        public string? LogiNo { get; set; }

        /// <summary>
        /// 快递物流信息
        /// </summary>
        public string? LogiInformation { get; set; }

        /// <summary>
        /// 快递是否不更新
        /// </summary>
        [Required]
        public bool LogiStatus { get; set; }

        /// <summary>
        /// 收货地区ID
        /// </summary>
        [Required]
        public long ShipAreaId { get; set; }

        /// <summary>
        /// 显示区域信息
        /// </summary>
        [StringLength(200)]
        public string? DisplayArea { get; set; }

        /// <summary>
        /// 收货详细地址
        /// </summary>
        [StringLength(200)]
        public string? ShipAddress { get; set; }

        /// <summary>
        /// 收货人姓名
        /// </summary>
        [StringLength(50)]
        public string? ShipName { get; set; }

        /// <summary>
        /// 收货电话
        /// </summary>
        [StringLength(50)]
        public string? ShipMobile { get; set; }

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
        /// 确认收货时间
        /// </summary>
        public DateTime? ConfirmTime { get; set; }

        /// <summary>
        /// 发货单明细
        /// </summary>
        public List<BillDeliveryItem> BillDeliveryItems { get; set; } = new();
    }
}