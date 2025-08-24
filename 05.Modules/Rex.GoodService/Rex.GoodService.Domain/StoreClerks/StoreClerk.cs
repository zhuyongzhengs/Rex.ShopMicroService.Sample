using Rex.GoodService.Stores;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace Rex.GoodService.StoreClerks
{
    /// <summary>
    /// 店铺店员关联表
    /// </summary>
    public class StoreClerk : FullAuditedAggregateRoot<Guid>, IMultiTenant
    {
        /// <summary>
        /// 租户ID
        /// </summary>
        public Guid? TenantId { get; set; }

        /// <summary>
        /// 店铺ID
        /// </summary>
        [Required]
        public Guid StoreId { get; set; }

        /// <summary>
        /// 店铺
        /// </summary>
        [ForeignKey(nameof(StoreId))]
        public Store? Store { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        [Required]
        public Guid UserId { get; set; }
    }
}