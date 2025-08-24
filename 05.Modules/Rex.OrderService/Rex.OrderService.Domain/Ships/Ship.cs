using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace Rex.OrderService.Ships
{
    /// <summary>
    /// 配送方式表
    /// </summary>
    public class Ship : FullAuditedAggregateRoot<Guid>, IMultiTenant
    {
        /// <summary>
        /// 租户ID
        /// </summary>
        public Guid? TenantId { get; set; }

        /// <summary>
        /// 配送方式名称
        /// </summary>
        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        /// <summary>
        /// 是否货到付款
        /// </summary>
        [Required]
        public bool IsCashOnDelivery { get; set; }

        /// <summary>
        /// 首重
        /// </summary>
        [Required]
        public int FirstUnit { get; set; }

        /// <summary>
        /// 续重
        /// </summary>
        [Required]
        public int ContinueUnit { get; set; }

        /// <summary>
        /// 是否按地区设置配送费用
        /// </summary>
        [Required]
        public bool IsdefaultAreaFee { get; set; }

        /// <summary>
        /// 地区类型
        /// </summary>
        [Required]
        public int AreaType { get; set; }

        /// <summary>
        /// 首重费用
        /// </summary>
        [Required]
        public decimal FirstunitPrice { get; set; }

        /// <summary>
        /// 续重费用
        /// </summary>
        [Required]
        public decimal ContinueunitPrice { get; set; }

        /// <summary>
        /// 配送费用计算表达式
        /// </summary>
        public string? Exp { get; set; }

        /// <summary>
        /// 物流公司名称
        /// </summary>
        [StringLength(50)]
        public string? LogiName { get; set; }

        /// <summary>
        /// 物流公司编码
        /// </summary>
        [StringLength(50)]
        public string? LogiCode { get; set; }

        /// <summary>
        /// 是否默认
        /// </summary>
        [Required]
        public bool IsDefault { get; set; }

        /// <summary>
        /// 配送方式排序
        /// </summary>
        [Required]
        public int Sort { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        /// <remarks>
        /// 1：正常、2：停用
        /// </remarks>
        [Required]
        public int Status { get; set; }

        /// <summary>
        /// 是否包邮
        /// </summary>
        [Required]
        public bool IsfreePostage { get; set; }

        /// <summary>
        /// 地区配送费用
        /// </summary>
        public string? AreaFee { get; set; }

        /// <summary>
        /// 商品总额满多少免运费
        /// </summary>
        [Required]
        public decimal GoodsMoney { get; set; }
    }
}