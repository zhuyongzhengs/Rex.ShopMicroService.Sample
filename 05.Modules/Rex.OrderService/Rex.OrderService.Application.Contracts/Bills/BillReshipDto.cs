using Rex.OrderService.Orders;
using Rex.Service.Core.Configurations;
using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;

namespace Rex.OrderService.Bills
{
    /// <summary>
    /// 退货单Dto
    /// </summary>
    public class BillReshipDto : EntityDto<Guid>
    {
        /// <summary>
        /// 退货单号
        /// </summary>
        public string No { get; set; }

        /// <summary>
        /// 订单序列
        /// </summary>
        public Guid OrderId { get; set; }

        /// <summary>
        /// 订单
        /// </summary>
        public OrderDto? Order { get; set; }

        /// <summary>
        /// 售后单序列
        /// </summary>
        public Guid AftersalesId { get; set; }

        /// <summary>
        /// 售后单
        /// </summary>
        public BillAftersalesDto? Aftersales { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// 物流公司编码
        /// </summary>
        public string? LogiCode { get; set; }

        /// <summary>
        /// 物流单号
        /// </summary>
        public string? LogiNo { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 显示状态
        /// </summary>
        public string StatusDisplay
        {
            get
            {
                return this.Status.GetDescription<GlobalEnums.BillReshipStatus>();
            }
        }

        /// <summary>
        /// 备注
        /// </summary>
        public string? Memo { get; set; }

        /// <summary>
        /// 退货单明细
        /// </summary>
        public List<BillReshipItemDto> BillReshipItems { get; set; } = new();

        /// <summary>
        /// 并发(控制)戳
        /// </summary>
        public string ConcurrencyStamp { get; set; }

        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime? CreationTime { get; set; }

        /// <summary>
        /// 最后修改日期
        /// </summary>
        public DateTime? LastModificationTime { get; set; }
    }
}