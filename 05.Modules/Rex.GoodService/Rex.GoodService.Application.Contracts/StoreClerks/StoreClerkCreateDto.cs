using System;
using Volo.Abp.Application.Dtos;

namespace Rex.GoodService.StoreClerks
{
    /// <summary>
    /// 创建店铺店员关联Dto
    /// </summary>
    public partial class StoreClerkCreateDto : EntityDto
    {
        /// <summary>
        /// 店铺ID
        /// </summary>
        public Guid StoreId { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        public Guid UserId { get; set; }
    }
}