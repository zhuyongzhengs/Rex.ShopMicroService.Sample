using Rex.OrderService.Bills;

namespace Rex.BackendAggregationService.Orders
{
    /// <summary>
    /// 售后单Dto
    /// </summary>
    public class BillAftersalesManyDto : BillAftersalesDto
    {
        /// <summary>
        /// 用户名称
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderNo { get; set; }
    }
}