using Volo.Abp.Application.Dtos;

namespace Rex.OrderService.Orders
{
    /// <summary>
    /// 修改收货信息订单Dto
    /// </summary>
    public class OrderShipUpdateDto : EntityDto
    {
        /// <summary>
        /// 收货地区ID
        /// </summary>
        public long ShipAreaId { get; set; }

        /// <summary>
        /// 显示区域信息
        /// </summary>
        public string? DisplayArea { get; set; }

        /// <summary>
        /// 收货详细地址
        /// </summary>
        public string ShipAddress { get; set; }

        /// <summary>
        /// 收货人姓名
        /// </summary>
        public string ShipName { get; set; }

        /// <summary>
        /// 收货电话
        /// </summary>
        public string ShipMobile { get; set; }
    }
}