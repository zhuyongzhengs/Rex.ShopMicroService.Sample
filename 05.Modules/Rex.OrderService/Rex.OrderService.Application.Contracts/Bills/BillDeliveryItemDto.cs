using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.MultiTenancy;

namespace Rex.OrderService.Bills
{
    /// <summary>
    /// 发货单明细
    /// </summary>
    public class BillDeliveryItemDto : EntityDto<Guid>, IMultiTenant
    {
        /// <summary>
        /// 租户ID
        /// </summary>
        public Guid? TenantId { get; set; }

        /// <summary>
        /// 订单编号
        /// </summary>
        public Guid OrderId { get; set; }

        /// <summary>
        /// 发货单ID
        /// </summary>
        public Guid DeliveryId { get; set; }

        /// <summary>
        /// 发货单
        /// </summary>
        public BillDeliveryDto? BillDelivery { get; set; }

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
        public string Sn { get; set; }

        /// <summary>
        /// 商品编码
        /// </summary>
        public string Bn { get; set; }

        /// <summary>
        /// 商品名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 发货数量
        /// </summary>
        public int Nums { get; set; }

        /// <summary>
        /// 重量
        /// </summary>
        public decimal Weight { get; set; }

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