using System;
using Volo.Abp.Application.Dtos;

namespace Rex.GoodService.StoreClerks
{
    /// <summary>
    /// 修改店铺店员关联Dto
    /// </summary>
    public partial class StoreClerkUpdateDto : EntityDto
    {
        /// <summary>
        /// 店铺ID
        /// </summary>
        public Guid StoreId { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// 并发(控制)戳
        /// </summary>
        public string ConcurrencyStamp { get; set; }
    }
}