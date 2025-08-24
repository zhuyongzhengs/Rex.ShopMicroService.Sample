using System;
using Volo.Abp.Application.Dtos;

namespace Rex.PaymentService.Payments
{
    /// <summary>
    /// 修改退款单Dto
    /// </summary>
    public class BillRefundUpdateDto : EntityDto
    {
        /// <summary>
        /// 退款单ID
        /// </summary>
        public string No { get; set; }

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
        /// 退款支付类型编码
        /// </summary>
        public string PaymentCode { get; set; }

        /// <summary>
        /// 第三方平台交易流水号
        /// </summary>
        public string TradeNo { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 退款失败原因
        /// </summary>
        public string Memo { get; set; }

        /// <summary>
        /// 并发(控制)戳
        /// </summary>
        public string ConcurrencyStamp { get; set; }
    }
}