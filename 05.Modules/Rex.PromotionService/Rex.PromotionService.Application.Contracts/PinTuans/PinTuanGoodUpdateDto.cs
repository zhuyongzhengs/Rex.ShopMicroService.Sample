using System;
using Volo.Abp.Application.Dtos;

namespace Rex.PromotionService.PinTuans
{
    /// <summary>
    /// 修改拼团商品Dto
    /// </summary>
    public partial class PinTuanGoodUpdateDto : EntityDto
    {
        /// <summary>
        /// 规则表序列
        /// </summary>
        public Guid RuleId { get; set; }

        /// <summary>
        /// 商品序列
        /// </summary>
        public Guid GoodId { get; set; }

        /// <summary>
        /// 并发(控制)戳
        /// </summary>
        public string ConcurrencyStamp { get; set; }
    }
}