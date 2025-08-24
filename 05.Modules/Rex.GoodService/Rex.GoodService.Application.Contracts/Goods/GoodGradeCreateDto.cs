using System;
using Volo.Abp.Application.Dtos;

namespace Rex.GoodService.Goods
{
    /// <summary>
    /// 创建商品会员价Dto
    /// </summary>
    public class GoodGradeCreateDto : EntityDto
    {
        /// <summary>
        /// 商品id
        /// </summary>
        public Guid? GoodId { get; set; }

        /// <summary>
        /// 会员等级id
        /// </summary>
        public Guid GradeId { get; set; }

        /// <summary>
        /// 会员价
        /// </summary>
        public decimal GradePrice { get; set; }
    }
}