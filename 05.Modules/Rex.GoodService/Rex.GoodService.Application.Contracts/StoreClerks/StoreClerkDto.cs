using Rex.GoodService.Stores;
using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.MultiTenancy;

namespace Rex.GoodService.StoreClerks
{
    /// <summary>
    /// 店铺店员关联Dto
    /// </summary>
    public partial class StoreClerkDto : EntityDto<Guid>, IMultiTenant
    {
        /// <summary>
        /// 租户ID
        /// </summary>
        public Guid? TenantId { get; set; }

        /// <summary>
        /// 店铺ID
        /// </summary>
        public Guid StoreId { get; set; }

        /// <summary>
        /// 店铺
        /// </summary>
        public StoreDto Store { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        public Guid UserId { get; set; }

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