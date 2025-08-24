using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;

namespace Rex.OrderService.Bills
{
    /// <summary>
    /// 售后单生成退货单Dto
    /// </summary>
    public class BillAftersalesToReshipCreateDto : EntityDto
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// 订单ID
        /// </summary>
        public Guid OrderId { get; set; }

        /// <summary>
        /// 售后单ID
        /// </summary>
        public Guid AftersalesId { get; set; }

        /// <summary>
        /// 售后单明细
        /// </summary>
        public List<BillAftersalesItemDto> BillAftersalesItems { get; set; } = new();
    }
}