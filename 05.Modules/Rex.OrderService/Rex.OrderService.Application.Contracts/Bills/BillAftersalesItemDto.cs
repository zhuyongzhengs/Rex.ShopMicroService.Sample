using System;
using Volo.Abp.Application.Dtos;

namespace Rex.OrderService.Bills
{
    /// <summary>
    /// 售后单明细Dto
    /// </summary>
    public class BillAftersalesItemDto : EntityDto<Guid>
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
        /// 订单明细ID
        /// </summary>
        public Guid OrderItemId { get; set; }

        /// <summary>
        /// 商品ID
        /// </summary>
        public Guid GoodId { get; set; }

        /// <summary>
        /// 货品ID
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