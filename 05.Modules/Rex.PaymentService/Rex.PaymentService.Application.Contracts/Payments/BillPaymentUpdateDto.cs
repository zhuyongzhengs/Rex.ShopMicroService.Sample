using System;
using Volo.Abp.Application.Dtos;

namespace Rex.PaymentService.Payments
{
    /// <summary>
    /// 修改支付单Dto
    /// </summary>
    public class BillPaymentUpdateDto : EntityDto
    {
        /// <summary>
        /// 支付单号
        /// </summary>
        public string No { get; set; }

        /// <summary>
        /// 资源编号
        /// </summary>
        public string SourceId { get; set; }

        /// <summary>
        /// 支付金额
        /// </summary>
        public decimal Money { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// 单据类型
        /// </summary>
        /// <remarks>
        /// 枚举：BillPaymentType
        /// 1：订单、2：充值、3：表单订单、4：表单付款单、5：服务订单
        /// </remarks>
        public int Type { get; set; }

        /// <summary>
        /// 支付状态
        /// </summary>
        /// <remarks>
        /// 枚举：BillPaymentStatus
        /// 1：未支付、2：已支付、3：其他
        /// </remarks>
        public int Status { get; set; }

        /// <summary>
        /// 支付类型编码
        /// </summary>
        public string PaymentCode { get; set; }

        /// <summary>
        /// 支付单生成IP
        /// </summary>
        public string Ip { get; set; }

        /// <summary>
        /// 支付的时候需要的参数，存的是json格式的一维数组
        /// </summary>
        public string? Parameters { get; set; }

        /// <summary>
        /// 支付回调后的状态描述
        /// </summary>
        public string? PayedMsg { get; set; }

        /// <summary>
        /// 第三方平台交易流水号
        /// </summary>
        public string? TradeNo { get; set; }

        /// <summary>
        /// 并发(控制)戳
        /// </summary>
        public string ConcurrencyStamp { get; set; }
    }
}