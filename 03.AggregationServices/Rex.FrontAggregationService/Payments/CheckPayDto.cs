using System.Text.Json.Nodes;
using Volo.Abp.Application.Dtos;

namespace Rex.FrontAggregationService.Payments
{
    /// <summary>
    /// 支付确认页面Dto
    /// </summary>
    public class CheckPayDto : EntityDto
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// 金额
        /// </summary>
        public decimal Money { get; set; }

        /// <summary>
        /// 支付Rel信息
        /// </summary>
        public List<PayRel> Rel { get; set; } = new();
    }

    /// <summary>
    /// 支付Rel
    /// </summary>
    public class PayRel
    {
        /// <summary>
        /// 资源ID
        /// </summary>
        public Guid SourceId { get; set; }

        /// <summary>
        /// 金额
        /// </summary>
        public decimal Money { get; set; }
    }

    /// <summary>
    /// 支付RelDto
    /// </summary>
    public class PaymentRelDto : EntityDto
    {
        /// <summary>
        /// 资源ID
        /// </summary>
        public Guid[] SourceIds { get; set; }

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

    /// <summary>
    /// 充值参数
    /// </summary>
    public class RechargeParams
    {
        /// <summary>
        /// 充值金额
        /// </summary>
        public decimal Money { get; set; }
    }
}