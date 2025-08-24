using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;

namespace Rex.PromotionService.PinTuans
{
    /// <summary>
    /// 修改拼团规则Dto
    /// </summary>
    public class PinTuanRuleUpdateDto : EntityDto
    {
        /// <summary>
        /// 活动名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndTime { get; set; }

        /// <summary>
        /// 人数2-10人
        /// </summary>
        public int PeopleNumber { get; set; }

        /// <summary>
        /// 单位分钟
        /// </summary>
        public int SignificantInterval { get; set; }

        /// <summary>
        /// 优惠金额
        /// </summary>
        public decimal DiscountAmount { get; set; }

        /// <summary>
        /// 每人限购数量
        /// </summary>
        public int MaxNums { get; set; }

        /// <summary>
        /// 每个商品活动数量
        /// </summary>
        public int MaxGoodsNums { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }

        /// <summary>
        /// 是否开启
        /// </summary>
        public bool IsStatusOpen { get; set; }

        /// <summary>
        /// 商品ID
        /// </summary>
        public List<Guid> GoodIds { get; set; } = new();

        /// <summary>
        /// 并发(控制)戳
        /// </summary>
        public string ConcurrencyStamp { get; set; }
    }
}