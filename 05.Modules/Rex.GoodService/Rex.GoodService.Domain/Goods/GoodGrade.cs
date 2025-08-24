using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace Rex.GoodService.Goods
{
    /// <summary>
    /// 商品会员价表
    /// </summary>
    public partial class GoodGrade : FullAuditedAggregateRoot<Guid>, IMultiTenant
    {
        /// <summary>
        /// 租户ID
        /// </summary>
        public Guid? TenantId { get; set; }

        /// <summary>
        /// 商品id
        /// </summary>
        [Required]
        public Guid GoodId { get; set; }

        /// <summary>
        /// 会员等级id
        /// </summary>
        [Required]
        public Guid GradeId { get; set; }

        /// <summary>
        /// 会员价
        /// </summary>
        [Required]
        public decimal GradePrice { get; set; }
    }
}