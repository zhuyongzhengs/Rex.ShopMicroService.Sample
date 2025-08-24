using Volo.Abp.Application.Dtos;

namespace Rex.OrderService.Orders
{
    /// <summary>
    /// 订单状态数量Dto
    /// </summary>
    public class OrderStatusQuantityDto : EntityDto
    {
        /// <summary>
        /// 全部
        /// </summary>
        public int All { get; set; }

        /// <summary>
        /// 待付款
        /// </summary>
        public int PendingPayment { get; set; }

        /// <summary>
        /// 待发货
        /// </summary>
        public int PendingShipment { get; set; }

        /// <summary>
        /// 待收货
        /// </summary>
        public int PendingDelivery { get; set; }

        /// <summary>
        /// 待评价
        /// </summary>
        public int PendingEvaluate { get; set; }

        /// <summary>
        /// 已取消
        /// </summary>
        public int Cancelled { get; set; }

        /// <summary>
        /// 已完成
        /// </summary>
        public int Completed { get; set; }
    }
}