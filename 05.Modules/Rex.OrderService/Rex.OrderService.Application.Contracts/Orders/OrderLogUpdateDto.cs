using System;
using Volo.Abp.Application.Dtos;

namespace Rex.OrderService.Orders
{
    /// <summary>
    /// 修改订单记录Dto
    /// </summary>
    public class OrderLogUpdateDto : EntityDto
    {
        /// <summary>
        /// 订单ID
        /// </summary>
        public Guid OrderId { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// 描述介绍
        /// </summary>
        public string? Msg { get; set; }

        /// <summary>
        /// 请求的数据json
        /// </summary>
        public string? Data { get; set; }

        /// <summary>
        /// 并发(控制)戳
        /// </summary>
        public string ConcurrencyStamp { get; set; }
    }
}