using System;
using Volo.Abp.Application.Dtos;

namespace Rex.PaymentService.Payments
{
    /// <summary>
    /// 支付单明细Dto
    /// </summary>
    public class BillPaymentDetailDto : EntityDto<Guid>
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
        /// 单据类型
        /// </summary>
        /// <remarks>
        /// 枚举：BillPaymentType
        /// 1：订单、2：充值、3：表单订单、4：表单付款单、5：服务订单
        /// </remarks>
        public int Type { get; set; }

        /// <summary>
        /// 支付类型编码
        /// </summary>
        public string PaymentCode { get; set; }
    }
}