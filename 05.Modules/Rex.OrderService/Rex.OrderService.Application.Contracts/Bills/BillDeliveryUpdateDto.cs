using Volo.Abp.Application.Dtos;

namespace Rex.OrderService.Bills
{
    /// <summary>
    /// 修改发货单Dto
    /// </summary>
    public class BillDeliveryUpdateDto : EntityDto
    {
        /// <summary>
        /// 物流公司编码
        /// </summary>
        public string? LogiCode { get; set; }

        /// <summary>
        /// 物流公司名称
        /// </summary>
        public string? LogiName { get; set; }

        /// <summary>
        /// 物流单号
        /// </summary>
        public string? LogiNo { get; set; }

        /// <summary>
        /// 收货地区ID
        /// </summary>
        public long ShipAreaId { get; set; }

        /// <summary>
        /// 收货详细地址
        /// </summary>
        public string? ShipAddress { get; set; }

        /// <summary>
        /// 显示区域信息
        /// </summary>
        public string? DisplayArea { get; set; }

        /// <summary>
        /// 收货人姓名
        /// </summary>
        public string? ShipName { get; set; }

        /// <summary>
        /// 收货电话
        /// </summary>
        public string? ShipMobile { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string? Memo { get; set; }

        /// <summary>
        /// 并发(控制)戳
        /// </summary>
        //public string ConcurrencyStamp { get; set; }
    }
}