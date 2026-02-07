using Rex.OrderService.Orders;
using Rex.Service.Core.Configurations;
using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;

namespace Rex.OrderService.Bills
{
    /// <summary>
    /// 售后单Dto
    /// </summary>
    public class BillAftersalesDto : EntityDto<Guid>
    {
        /// <summary>
        /// 售后单号
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
        /// 用户ID
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// 售后类型
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// 显示售后类型
        /// </summary>
        public string TypeDisplay
        {
            get
            {
                return this.Type.GetDescription<GlobalEnums.BillAftersalesIsReceive>();
            }
        }

        /// <summary>
        /// 退款金额
        /// </summary>
        public decimal RefundAmount { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 显示审核状态
        /// </summary>
        public string StatusDisplay
        {
            get
            {
                return this.Status.GetDescription<GlobalEnums.BillAftersalesStatus>();
            }
        }

        /// <summary>
        /// 退款原因
        /// </summary>
        public string Reason { get; set; }

        /// <summary>
        /// 卖家备注
        /// </summary>
        public string? Mark { get; set; }

        /// <summary>
        /// 售后单明细
        /// </summary>
        public List<BillAftersalesItemDto> BillAftersalesItems { get; set; } = new();

        /// <summary>
        /// 售后单图片
        /// </summary>
        public List<BillAftersalesImagesDto> BillAftersalesImagess { get; set; } = new();

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