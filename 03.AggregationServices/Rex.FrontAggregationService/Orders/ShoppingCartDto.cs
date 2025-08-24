using Volo.Abp.Application.Dtos;

namespace Rex.FrontAggregationService.Orders
{
    /// <summary>
    /// 购物车Dto
    /// </summary>
    public class ShoppingCartDto : EntityDto<Guid>
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// 商品ID
        /// </summary>
        public Guid GoodId { get; set; }

        /// <summary>
        /// 商品名称
        /// </summary>
        public string GoodName { get; set; }

        /// <summary>
        /// 货品ID
        /// </summary>
        public Guid ProductId { get; set; }

        /// <summary>
        /// 商品条码
        /// </summary>
        public string BarCode { get; set; }

        /// <summary>
        /// 货品编码
        /// </summary>
        public string Sn { get; set; }

        /// <summary>
        /// 货品价格
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// 总价
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// 库存
        /// </summary>
        public int Stock { get; set; }

        /// <summary>
        /// 库存是否充足
        /// </summary>
        public bool IsStockSufficient { get; set; }

        /// <summary>
        /// 规格图片
        /// </summary>
        public string? Images { get; set; }

        /// <summary>
        /// 规格值
        /// </summary>
        public string? SpesDesc { get; set; }

        /// <summary>
        /// 货品数量
        /// </summary>
        public int Nums { get; set; }

        /// <summary>
        /// 总重量
        /// </summary>
        public decimal Weight { get; set; }

        /// <summary>
        /// 购物车类型
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// 关联对象ID
        /// </summary>
        public Guid? ObjectId { get; set; }
    }
}