using System;
using Volo.Abp.Application.Dtos;

namespace Rex.OrderService.Bills
{
    /// <summary>
    /// 创建退货单图片关联Dto
    /// </summary>
    public class BillAftersalesImagesCreateDto : EntityDto
    {
        /// <summary>
        /// 售后单id
        /// </summary>
        public Guid AftersalesId { get; set; }

        /// <summary>
        /// 图片地址
        /// </summary>
        public string ImageUrl { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }
    }
}