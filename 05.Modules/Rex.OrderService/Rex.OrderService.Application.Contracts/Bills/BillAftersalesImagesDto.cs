using System;
using Volo.Abp.Application.Dtos;

namespace Rex.OrderService.Bills
{
    /// <summary>
    /// 退货单图片关联Dto
    /// </summary>
    public class BillAftersalesImagesDto : EntityDto<Guid>
    {
        /// <summary>
        /// 售后单id
        /// </summary>
        public Guid AftersalesId { get; set; }

        /// <summary>
        /// 售后单
        /// </summary>
        public BillAftersalesDto? Aftersales { get; set; }

        /// <summary>
        /// 图片地址
        /// </summary>
        public string ImageUrl { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }

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