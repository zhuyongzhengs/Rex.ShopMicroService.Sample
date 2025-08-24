using System;
using Volo.Abp.Application.Dtos;

namespace Rex.GoodService.Goods
{
    /// <summary>
    /// 商品浏览记录Dto
    /// </summary>
    public class GoodBrowsingDto : EntityDto<Guid>
    {
        /// <summary>
        /// 商品id 关联goods.id
        /// </summary>
        public Guid GoodId { get; set; }

        /// <summary>
        /// 用户id
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// 商品名称
        /// </summary>
        public string GoodName { get; set; }

        /// <summary>
        /// 商品图片
        /// </summary>
        public string? Image { get; set; }

        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime CreationTime { get; set; }
    }
}