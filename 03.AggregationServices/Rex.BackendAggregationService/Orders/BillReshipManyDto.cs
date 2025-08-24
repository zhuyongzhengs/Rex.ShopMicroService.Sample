using Rex.OrderService.Bills;

namespace Rex.BackendAggregationService.Orders
{
    /// <summary>
    /// 退货单
    /// </summary>
    public class BillReshipManyDto : BillReshipDto
    {
        /// <summary>
        /// 用户名称
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderNo { get; set; }

        /// <summary>
        /// 售后单号
        /// </summary>
        public string AftersalesNo { get; set; }
    }
}