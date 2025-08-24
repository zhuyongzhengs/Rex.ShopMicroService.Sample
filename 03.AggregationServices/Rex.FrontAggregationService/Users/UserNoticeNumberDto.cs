using Volo.Abp.Application.Dtos;

namespace Rex.FrontAggregationService.Users
{
    /// <summary>
    /// 用户通知数量Dto
    /// </summary>
    public class UserNoticeNumberDto : EntityDto
    {
        /// <summary>
        /// 浏览记录
        /// </summary>
        public long TotalGoodBrowsing { get; set; }

        /// <summary>
        /// 优惠劵
        /// </summary>
        public long TotalCoupon { get; set; }

        /// <summary>
        /// 商品收藏
        /// </summary>
        public long TotalGoodCollection { get; set; }

        /// <summary>
        /// 售后单
        /// </summary>
        public long TotalBillAftersale { get; set; }

        /// <summary>
        /// 待付款
        /// </summary>
        public long TotalPendingPayment { get; set; }

        /// <summary>
        /// 待发货
        /// </summary>
        public long TotalPendingShipment { get; set; }

        /// <summary>
        /// 待收货
        /// </summary>
        public long TotalPendingDelivery { get; set; }

        /// <summary>
        /// 待评价
        /// </summary>
        public long TotalPendingEvaluate { get; set; }
    }
}