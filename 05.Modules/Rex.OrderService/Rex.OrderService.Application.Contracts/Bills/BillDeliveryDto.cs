using Rex.OrderService.Orders;
using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;

namespace Rex.OrderService.Bills
{
    /// <summary>
    /// 发货单Dto
    /// </summary>
    public class BillDeliveryDto : EntityDto<Guid>
    {
        /// <summary>
        /// 发货单号
        /// </summary>
        public string No { get; set; }

        /// <summary>
        /// 订单ID
        /// </summary>
        public Guid OrderId { get; set; }

        /// <summary>
        /// 订单
        /// </summary>
        public OrderDto? Order { get; set; }

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
        /// 显示区域信息
        /// </summary>
        public string? DisplayArea { get; set; }

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

        /// <summary>
        /// 发货单明细
        /// </summary>
        public List<BillDeliveryItemDto> BillDeliveryItems { get; set; } = new();

        /// <summary>
        /// 并发(控制)戳
        /// </summary>
        public string ConcurrencyStamp { get; set; }

        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime? CreationTime { get; set; }
    }
}