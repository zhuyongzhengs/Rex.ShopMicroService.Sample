using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.MultiTenancy;

namespace Rex.PromotionService.PinTuans
{
    /// <summary>
    /// 拼团记录Dto
    /// </summary>
    public partial class PinTuanRecordDto : EntityDto<Guid>, IMultiTenant
    {
        /// <summary>
        /// 租户ID
        /// </summary>
        public Guid? TenantId { get; set; }

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