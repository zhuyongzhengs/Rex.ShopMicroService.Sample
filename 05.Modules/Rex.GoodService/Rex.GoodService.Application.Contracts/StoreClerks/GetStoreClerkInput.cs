using System;
using Volo.Abp.Application.Dtos;

namespace Rex.GoodService.StoreClerks
{
    /// <summary>
    /// 查询店铺店员关联
    /// </summary>
    public partial class GetStoreClerkInput : PagedAndSortedResultRequestDto
    {
        /// <summary>
        /// 店铺ID
        /// </summary>
        public Guid? StoreId { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        /// <remarks>
        /// 多个用“,”隔开
        /// </remarks>
        public string? UserIds { get; set; }
    }
}