using System.Text.Json.Nodes;
using Volo.Abp.Application.Dtos;

namespace Rex.FrontAggregationService.Payments
{
    /// <summary>
    /// 用户支付Dto
    /// </summary>
    public class UserPayDto : EntityDto
    {
        /// <summary>
        /// 资源ID
        /// </summary>
        public Guid[] SourceIds { get; set; }

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

        /// <summary>
        /// 附件参数对象
        /// </summary>
        public JsonObject Params { get; set; } = new JsonObject();
    }
}