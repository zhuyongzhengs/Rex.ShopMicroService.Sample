using Rex.GoodService.Goods;

namespace Rex.BackendAggregationService.Goods
{
    /// <summary>
    /// 商品评价Dto
    /// </summary>
    public class GoodCommentManyDto : GoodCommentDto
    {
        /// <summary>
        /// 用户头像
        /// </summary>
        public string? Avatar { get; set; }

        /// <summary>
        /// 用户名称
        /// </summary>
        public string? UserName { get; set; }

        /// <summary>
        /// 用户手机号码
        /// </summary>
        public string? PhoneNumber { get; set; }

        /// <summary>
        /// 商品名称
        /// </summary>
        public string? GoodName { get; set; }

        /// <summary>
        /// 订单号
        /// </summary>
        public string? OrderNo { get; set; }

        /// <summary>
        /// 收货人姓名
        /// </summary>
        public string? ShipName { get; set; }

        /// <summary>
        /// 收货人联系方式
        /// </summary>
        public string? ShipMobile { get; set; }
    }
}