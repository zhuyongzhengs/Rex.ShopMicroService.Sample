using Volo.Abp.Application.Dtos;

namespace Rex.BackendAggregationService.Orders
{
    /// <summary>
    /// 售后单明细Dto
    /// </summary>
    public class BillAftersalesDtlItemDto : EntityDto<Guid>
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
        /// 总价
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// 购买数量
        /// </summary>
        public int BuyNums { get; set; }

        /// <summary>
        /// 发货数量
        /// </summary>
        public int SendNums { get; set; }

        /// <summary>
        /// 退货数量
        /// </summary>
        public int Nums { get; set; }

        /// <summary>
        /// 货品明细序列号存储
        /// </summary>
        public string? Addon { get; set; }

        /// <summary>
        /// 是否选中
        /// </summary>
        public bool Checked { get; set; }
    }
}