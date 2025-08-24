using Rex.PaymentService.Payments;
using Volo.Abp.Application.Dtos;

namespace Rex.PaymentService.Commons
{
    /// <summary>
    /// 微信支付参数(响应)结果
    /// </summary>
    public class WeChatPayParameterResultDto : EntityDto
    {
        /// <summary>
        /// 支付单明细
        /// </summary>
        public BillPaymentDetailDto Detail { get; set; }

        /// <summary>
        /// 微信支付参数
        /// </summary>
        public JsSdkWeChatPayParameterDto PayParameter { get; set; }
    }
}