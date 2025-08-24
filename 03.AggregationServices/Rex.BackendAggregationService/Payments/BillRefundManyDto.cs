using Rex.PaymentService.Payments;

namespace Rex.BackendAggregationService.Payments
{
    /// <summary>
    /// 退款单Dto
    /// </summary>
    public class BillRefundManyDto : BillRefundDto
    {
        /// <summary>
        /// 售后单号
        /// </summary>
        public string? BillAftersalesNo { get; set; }

        /// <summary>
        /// 关联单号
        /// </summary>
        public string? SourceNo { get; set; }

        /// <summary>
        /// 用户名称
        /// </summary>
        public string? UserName { get; set; }
    }
}