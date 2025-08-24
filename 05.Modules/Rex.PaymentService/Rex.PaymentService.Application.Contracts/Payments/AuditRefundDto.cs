using Volo.Abp.Application.Dtos;

namespace Rex.PaymentService.Payments
{
    /// <summary>
    /// 审核退款Dto
    /// </summary>
    public class AuditRefundDto : EntityDto
    {
        /// <summary>
        /// 退款状态
        /// </summary>
        /// <remarks>
        /// 枚举：BillRefundStatus
        /// 1：未退款、2：已退款、3：其他
        /// </remarks>
        public int Status { get; set; }

        /// <summary>
        /// 退款支付类型编码
        /// </summary>
        public string PaymentCode { get; set; }
    }
}