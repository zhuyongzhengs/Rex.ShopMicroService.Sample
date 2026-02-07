using System;
using Volo.Abp.Application.Dtos;

namespace Rex.PromotionService.Promotions
{
    /// <summary>
    /// 促销活动记录Dto
    /// </summary>
    public class PromotionRecordDto : EntityDto<Guid>
    {
        /// <summary>
        /// 促销序列
        /// </summary>
        public Guid PromotionId { get; set; }

        /// <summary>
        /// 用户Id
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// 商品id
        /// </summary>
        public Guid GoodId { get; set; }

        /// <summary>
        /// 货品id
        /// </summary>
        public Guid ProductId { get; set; }

        /// <summary>
        /// 订单id
        /// </summary>
        public Guid OrderId { get; set; }

        /// <summary>
        /// 3团购/4促销
        /// </summary>
        public int Type { get; set; }

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