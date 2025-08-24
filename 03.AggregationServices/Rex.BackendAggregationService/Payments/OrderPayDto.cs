using Volo.Abp.Application.Dtos;

namespace Rex.BackendAggregationService.Payments
{
    /// <summary>
    /// 订单支付 Dto
    /// </summary>
    public class OrderPayDto : EntityDto
    {
        /// <summary>
        /// 订单ID
        /// </summary>
        public Guid[] OrderIds { get; set; }

        /// <summary>
        /// 支付方式编码
        /// </summary>
        public string PaymentCode { get; set; }

        /// <summary>
        /// 付款单类型
        /// </summary>
        /// <remarks>
        /// 枚举：BillPaymentType
        /// 1：订单、2：充值、3：表单订单、4：表单付款码、5：服务订单
        /// </remarks>
        public int PaymentType { get; set; }
    }
}