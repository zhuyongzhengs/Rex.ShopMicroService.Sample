using System;
using Volo.Abp.Application.Dtos;

namespace Rex.PromotionService.Promotions
{
    /// <summary>
    /// 促销结果Dto
    /// </summary>
    public class PromotionResultDto : EntityDto<Guid>
    {
        /// <summary>
        /// 促销ID
        /// </summary>
        public Guid? PromotionId { get; set; }

        /// <summary>
        /// 促销条件编码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 支付配置参数序列号存储
        /// </summary>
        public string Parameters { get; set; }

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