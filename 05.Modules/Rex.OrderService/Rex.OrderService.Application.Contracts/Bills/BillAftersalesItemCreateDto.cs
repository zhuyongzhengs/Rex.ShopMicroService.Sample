using System;
using Volo.Abp.Application.Dtos;

namespace Rex.OrderService.Bills
{
    /// <summary>
    /// 创建售后单明细Dto
    /// </summary>
    public class BillAftersalesItemCreateDto : EntityDto
    {
        /// <summary>
        /// 售后单id
        /// </summary>
        public Guid AftersalesId { get; set; }

        /// <summary>
        /// 订单明细ID
        /// </summary>
        public Guid OrderItemId { get; set; }

        /// <summary>
        /// 商品ID
        /// </summary>
        public Guid GoodId { get; set; }

        /// <summary>
        /// 货品ID\
        /// </summary>
        public Guid ProductId { get; set; }

        /// <summary>
        /// 货品编码
        /// </summary>
        public string? Sn { get; set; }

        /// <summary>
        /// 商品编码
        /// </summary>
        public string? Bn { get; set; }

        /// <summary>
        /// 商品名称
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// 图片
        /// </summary>
        public string ImageUrl { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public int Nums { get; set; }

        /// <summary>
        /// 货品明细序列号存储
        /// </summary>
        public string? Addon { get; set; }
    }
}