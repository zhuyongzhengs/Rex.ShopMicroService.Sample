using Volo.Abp.Application.Dtos;

namespace Rex.BackendAggregationService.Payments
{
    /// <summary>
    /// 更新支付单Dto
    /// </summary>
    public class PaymentBillUpdateDto : EntityDto
    {
        /// <summary>
        /// 支付单号
        /// </summary>
        public string BillPaymentNo { get; set; }

        /// <summary>
        /// 支付状态
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 支付类型编码
        /// </summary>
        public string PaymentCode { get; set; }

        /// <summary>
        /// 支付金额
        /// </summary>
        public decimal Money { get; set; }

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