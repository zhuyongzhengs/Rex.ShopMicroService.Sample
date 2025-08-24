using System;
using Volo.Abp.Application.Dtos;

namespace Rex.PaymentService.Payments
{
    /// <summary>
    /// 查询退款单
    /// </summary>
    public class GetBillRefundInput : PagedAndSortedResultRequestDto
    {
        /// <summary>
        /// 退款单号
        /// </summary>
        public string? No { get; set; }

        /// <summary>
        /// 退款单Id
        /// </summary>
        public Guid? AftersalesId { get; set; }

        /// <summary>
        /// 退款单号
        /// </summary>
        public string? AftersalesNo { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        public Guid? UserId { get; set; }

        /// <summary>
        /// 用户名称
        /// </summary>
        public string? UserName { get; set; }

        /// <summary>
        /// 单据类型
        /// </summary>
        /// <remarks>
        /// 枚举：BillRefundType
        /// 1：订单、2：充值、3：表单订单、4：表单付款单、5：服务订单
        /// </remarks>
        public int? Type { get; set; }

        /// <summary>
        /// 退款状态
        /// </summary>
        /// <remarks>
        /// 枚举：BillRefundStatus
        /// 1：未退款、2：已退款、3：其他
        /// </remarks>
        public int? Status { get; set; }

        /// <summary>
        /// 退款支付类型编码
        /// </summary>
        public string? PaymentCode { get; set; }
    }
}