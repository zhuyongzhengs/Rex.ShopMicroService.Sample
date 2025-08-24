using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace Rex.PaymentService.Payments
{
    /// <summary>
    /// 支付单表
    /// </summary>
    public class BillPayment : FullAuditedAggregateRoot<Guid>, IMultiTenant
    {
        /// <summary>
        /// 租户ID
        /// </summary>
        public Guid? TenantId { get; set; }

        /// <summary>
        /// 支付单号
        /// </summary>
        [Required]
        [StringLength(20)]
        public string No { get; set; }

        /// <summary>
        /// 资源编号
        /// </summary>
        [Required]
        public string SourceId { get; set; }

        /// <summary>
        /// 支付金额
        /// </summary>
        [Required]
        public decimal Money { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        [Required]
        public Guid UserId { get; set; }

        /// <summary>
        /// 单据类型
        /// </summary>
        /// <remarks>
        /// 枚举：BillPaymentType
        /// 1：订单、2：充值、3：表单订单、4：表单付款单、5：服务订单
        /// </remarks>
        [Required]
        public int Type { get; set; }

        /// <summary>
        /// 支付状态
        /// </summary>
        /// <remarks>
        /// 枚举：BillPaymentStatus
        /// 1：未支付、2：已支付、3：其他
        /// </remarks>
        [Required]
        public int Status { get; set; }

        /// <summary>
        /// 支付类型编码
        /// </summary>
        [Required]
        [StringLength(50)]
        public string PaymentCode { get; set; }

        /// <summary>
        /// 支付单生成IP
        /// </summary>
        [Required]
        [StringLength(50)]
        public string Ip { get; set; }

        /// <summary>
        /// 支付的时候需要的参数，存的是json格式的一维数组
        /// </summary>
        [StringLength(200)]
        public string? Parameters { get; set; }

        /// <summary>
        /// 支付回调后的状态描述
        /// </summary>
        [StringLength(255)]
        public string? PayedMsg { get; set; }

        /// <summary>
        /// 第三方平台交易流水号
        /// </summary>
        [StringLength(50)]
        public string? TradeNo { get; set; }
    }
}