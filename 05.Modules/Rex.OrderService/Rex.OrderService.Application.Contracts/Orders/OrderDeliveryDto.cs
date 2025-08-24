using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;

namespace Rex.OrderService.Orders
{
    /// <summary>
    /// 订单发货Dto
    /// </summary>
    public class OrderDeliveryDto : EntityDto<Guid>
    {
        /// <summary>
        /// 订单号
        /// </summary>
        public string No { get; set; }

        /// <summary>
        /// 配送方式ID
        /// </summary>
        public Guid? LogisticsId { get; set; }

        /// <summary>
        /// 配送方式名称
        /// </summary>
        public string? LogisticsName { get; set; }

        /// <summary>
        /// 商品总重量
        /// </summary>
        public decimal Weight { get; set; }

        /// <summary>
        /// 配送费用
        /// </summary>
        public decimal CostFreight { get; set; }

        /// <summary>
        /// 买家备注
        /// </summary>
        public string? Memo { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// 用户名称
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 用户昵称
        /// </summary>
        public string NickName { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// 用户等级
        /// </summary>
        public string UserGradeName { get; set; }

        /// <summary>
        /// 卖家备注
        /// </summary>
        public string? Mark { get; set; }

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

        /// <summary>
        /// 物流公司编码
        /// </summary>
        public string LogiCode { get; set; }

        /// <summary>
        /// 物流公司名称
        /// </summary>
        public string LogiName { get; set; }

        /// <summary>
        /// 物流单号
        /// </summary>
        public string LogiNo { get; set; }

        /// <summary>
        /// 发货备注
        /// </summary>
        public string? DeliveryMemo { get; set; }

        /// <summary>
        /// 订单明细
        /// </summary>
        public List<OrderItemDto> OrderItems { get; set; } = new();
    }
}