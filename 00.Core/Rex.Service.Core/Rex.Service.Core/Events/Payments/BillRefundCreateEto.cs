using System;
using Volo.Abp.Domain.Entities.Events.Distributed;

namespace Rex.Service.Core.Events.Payments
{
    /// <summary>
    /// 创建退款单Eto
    /// </summary>
    public class BillRefundCreateEto : EntityEto
    {
        /// <summary>
        /// 事件名称
        /// </summary>
        public const string EventName = "Create.Bill.Refund";

        /// <summary>
        /// 退货单Id
        /// </summary>
        public Guid BillAftersalesId { get; set; }

        /// <summary>
        /// 退款金额
        /// </summary>
        public decimal Money { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// 资源Id，根据type不同而关联不同的表
        /// </summary>
        public string SourceId { get; set; }

        /// <summary>
        /// 资源类型1=订单,2充值单
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public int Status { get; set; }
    }
}