using System;
using Volo.Abp.Application.Dtos;

namespace Rex.PaymentService.Payments
{
    /// <summary>
    /// 修改支付单状态Dto
    /// </summary>
    public class BillPaymentStatusUpdateDto : EntityDto
    {
        /// <summary>
        /// 支付金额
        /// </summary>
        public decimal Money { get; set; }

        /// <summary>
        /// 支付状态
        /// </summary>
        /// <remarks>
        /// 枚举：BillPaymentStatus
        /// 1：未支付、2：已支付、3：其他
        /// </remarks>
        public int Status { get; set; }

        /// <summary>
        /// 支付类型编码
        /// </summary>
        public string PaymentCode { get; set; }

        /// <summary>
        /// 支付回调后的状态描述
        /// </summary>
        public string? PayedMsg { get; set; }

        /// <summary>
        /// 第三方平台交易流水号
        /// </summary>
        public string? TradeNo { get; set; }
    }
}