using Rex.Service.Core.Configurations;
using System;
using Volo.Abp.Application.Dtos;

namespace Rex.OrderService.Orders
{
    /// <summary>
    /// 订单记录Dto
    /// </summary>
    public class OrderLogDto : EntityDto<Guid>
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
        /// 显示类型
        /// </summary>
        public string TypeDisplay
        {
            get
            {
                return this.Type.GetDescription<GlobalEnums.OrderLogType>();
            }
        }

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

        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime? CreationTime { get; set; }
    }
}