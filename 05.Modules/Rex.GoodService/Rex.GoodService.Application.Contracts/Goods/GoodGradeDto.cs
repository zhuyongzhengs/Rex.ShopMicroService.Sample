using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.MultiTenancy;

namespace Rex.GoodService.Goods
{
    /// <summary>
    /// 商品会员价Dto
    /// </summary>
    public class GoodGradeDto : EntityDto<Guid>, IMultiTenant
    {
        /// <summary>
        /// 租户ID
        /// </summary>
        public Guid? TenantId { get; set; }

        /// <summary>
        /// 商品id
        /// </summary>
        public Guid? GoodId { get; set; }

        /// <summary>
        /// 会员等级id
        /// </summary>
        public Guid GradeId { get; set; }

        /// <summary>
        /// 会员等级标题
        /// </summary>
        public string? GradeTitle { get; set; }

        /// <summary>
        /// 会员价
        /// </summary>
        public decimal GradePrice { get; set; }

        /// <summary>
        /// 并发(控制)戳
        /// </summary>
        public string? ConcurrencyStamp { get; set; }

        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime? CreationTime { get; set; }
    }
}