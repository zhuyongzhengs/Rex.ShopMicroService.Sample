using System;

namespace Rex.PromotionService.PinTuans
{
    /// <summary>
    /// 拼团规则Dto
    /// </summary>
    public partial class PinTuanRuleDto
    {
        /// <summary>
        /// 判断拼团状态
        /// </summary>
        public int PinTuanStartStatus { get; set; } = 0;

        /// <summary>
        /// 剩余时间
        /// </summary>
        public int LastTime { get; set; }
    }
}