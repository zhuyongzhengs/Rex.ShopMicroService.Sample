using System;
using Volo.Abp.Application.Dtos;

namespace Rex.GoodService.Goods
{
    /// <summary>
    /// 修改商品参数Dto
    /// </summary>
    public class GoodGradeUpdateDto : EntityDto
    {
        /// <summary>
        /// 商品id
        /// </summary>
        public Guid GoodId { get; set; }

        /// <summary>
        /// 会员等级id
        /// </summary>
        public Guid? GradeId { get; set; }

        /// <summary>
        /// 会员价
        /// </summary>
        public decimal GradePrice { get; set; }

        /// <summary>
        /// 并发(控制)戳
        /// </summary>
        public string ConcurrencyStamp { get; set; }
    }
}