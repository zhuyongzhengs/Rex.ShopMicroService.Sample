using Volo.Abp.Domain.Entities.Events.Distributed;

namespace Rex.Service.Core.Events.Payments
{
    /// <summary>
    /// 用户余额支付Eto
    /// </summary>
    public class UserBalancePaymentEto : EntityEto
    {
        /// <summary>
        /// 事件名称
        /// </summary>
        public const string EventName = "User.Balance.Payment";

        /// <summary>
        /// 支付单号
        /// </summary>
        public string BillPaymentNo { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark { get; set; }
    }
}