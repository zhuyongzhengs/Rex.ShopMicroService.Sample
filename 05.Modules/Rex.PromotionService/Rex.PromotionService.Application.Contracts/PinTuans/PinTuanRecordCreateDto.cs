using System;
using Volo.Abp.Application.Dtos;

namespace Rex.PromotionService.PinTuans
{
    /// <summary>
    /// 创建拼团记录Dto
    /// </summary>
    public class PinTuanRecordCreateDto : EntityDto
    {
        /// <summary>
        /// 团序列
        /// </summary>
        public Guid TeamId { get; set; }

        /// <summary>
        /// 用户序列
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// 规则表序列
        /// </summary>
        public Guid RuleId { get; set; }

        /// <summary>
        /// 商品序列
        /// </summary>
        public Guid GoodId { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 订单序列
        /// </summary>
        public string OrderId { get; set; }

        /// <summary>
        /// 拼团人数Json
        /// </summary>
        public string? Parameters { get; set; }

        /// <summary>
        /// 关闭时间
        /// </summary>
        public DateTime CloseTime { get; set; }
    }
}