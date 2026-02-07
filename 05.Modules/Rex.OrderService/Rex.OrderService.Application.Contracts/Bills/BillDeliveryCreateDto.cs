using System;
using Volo.Abp.Application.Dtos;

namespace Rex.OrderService.Bills
{
    /// <summary>
    /// 添加发货单Dto
    /// </summary>
    public class BillDeliveryCreateDto : EntityDto
    {
        /// <summary>
        /// 发货单号
        /// </summary>
        public string No { get; set; }

        /// <summary>
        /// 订单号
        /// </summary>
        public Guid OrderId { get; set; }

        /// <summary>
        /// 物流公司编码
        /// </summary>
        public string? LogiCode { get; set; }

        /// <summary>
        /// 物流单号
        /// </summary>
        public string? LogiNo { get; set; }

        /// <summary>
        /// 快递物流信息
        /// </summary>
        public string? LogiInformation { get; set; }

        /// <summary>
        /// 快递是否不更新
        /// </summary>
        public bool LogiStatus { get; set; }

        /// <summary>
        /// 收货地区ID
        /// </summary>
        public long ShipAreaId { get; set; }

        /// <summary>
        /// 收货详细地址
        /// </summary>
        public string? ShipAddress { get; set; }

        /// <summary>
        /// 收货人姓名
        /// </summary>
        public string? ShipName { get; set; }

        /// <summary>
        /// 收货电话
        /// </summary>
        public string? ShipMobile { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string? Memo { get; set; }

        /// <summary>
        /// 确认收货时间
        /// </summary>
        public DateTime? ConfirmTime { get; set; }
    }
}