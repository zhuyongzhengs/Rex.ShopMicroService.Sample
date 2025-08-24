using System;
using Volo.Abp.Application.Dtos;

namespace Rex.PromotionService.Promotions
{
    /// <summary>
    /// 添加促销条件Dto
    /// </summary>
    public class PromotionConditionCreateDto : EntityDto
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
    }
}