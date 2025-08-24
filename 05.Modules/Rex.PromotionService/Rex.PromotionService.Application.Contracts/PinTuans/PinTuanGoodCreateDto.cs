using System;
using Volo.Abp.Application.Dtos;

namespace Rex.PromotionService.PinTuans
{
    /// <summary>
    /// 创建拼团商品Dto
    /// </summary>
    public partial class PinTuanGoodCreateDto : EntityDto
    {
        /// <summary>
        /// 规则表序列
        /// </summary>
        public Guid RuleId { get; set; }

        /// <summary>
        /// 商品序列
        /// </summary>
        public Guid GoodId { get; set; }
    }
}