using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace Rex.PaymentService.Payments
{
    /// <summary>
    /// 退款单表
    /// </summary>
    public class BillRefund : FullAuditedAggregateRoot<Guid>, IMultiTenant
    {
        public BillRefund()
        {
            // TODO
        }

        public BillRefund(Guid id)
        {
            this.Id = id;
        }

        /// <summary>
        /// 租户ID
        /// </summary>
        public Guid? TenantId { get; set; }

        /// <summary>
        /// 退款单ID
        /// </summary>
        [Required]
        [StringLength(20)]
        public string No { get; set; }

        /// <summary>
        /// 退货单Id
        /// </summary>
        [Required]
        [StringLength(50)]
        public Guid BillAftersalesId { get; set; }

        /// <summary>
        /// 退款金额
        /// </summary>
        [Required]
        public decimal Money { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        [Required]
        public Guid UserId { get; set; }

        /// <summary>
        /// 资源Id，根据type不同而关联不同的表
        /// </summary>
        [Required]
        [StringLength(1000)]
        public string SourceId { get; set; }

        /// <summary>
        /// 资源类型1=订单,2充值单
        /// </summary>
        [Required]
        public int Type { get; set; }

        /// <summary>
        /// 退款支付类型编码 默认原路返回 关联支付单表支付编码
        /// </summary>
        [StringLength(50)]
        public string? PaymentCode { get; set; }

        /// <summary>
        /// 第三方平台交易流水号
        /// </summary>
        [StringLength(50)]
        public string? TradeNo { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        [Required]
        public int Status { get; set; }

        /// <summary>
        /// 退款失败原因
        /// </summary>
        public string? Memo { get; set; }
    }
}