using Rex.Service.Core.Configurations;
using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.MultiTenancy;

namespace Rex.PaymentService.Payments
{
    /// <summary>
    /// 退款单Dto
    /// </summary>
    public class BillRefundDto : EntityDto<Guid>, IMultiTenant
    {
        /// <summary>
        /// 租户ID
        /// </summary>
        public Guid? TenantId { get; set; }

        /// <summary>
        /// 退款单号
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
        /// 显示类型
        /// </summary>
        public string TypeDisplay
        {
            get
            {
                return this.Type.GetDescription<GlobalEnums.BillRefundType>();
            }
        }

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
        /// 显示状态
        /// </summary>
        public string StatusDisplay
        {
            get
            {
                return this.Status.GetDescription<GlobalEnums.BillRefundStatus>();
            }
        }

        /// <summary>
        /// 退款失败原因
        /// </summary>
        public string Memo { get; set; }

        /// <summary>
        /// 并发(控制)戳
        /// </summary>
        public string ConcurrencyStamp { get; set; }

        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime? CreationTime { get; set; }
    }
}