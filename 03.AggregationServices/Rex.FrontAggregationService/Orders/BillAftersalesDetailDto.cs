using Rex.OrderService.Bills;
using Rex.PaymentService.Payments;

namespace Rex.FrontAggregationService.Orders
{
    /// <summary>
    /// 售后单Dto
    /// </summary>
    public class BillAftersalesDetailDto : BillAftersalesDto
    {
        /// <summary>
        /// 退款单
        /// </summary>
        public BillRefundDto? BillRefund { get; set; }

        /// <summary>
        /// 退货单
        /// </summary>
        public BillReshipDto? BillReship { get; set; }
    }
}