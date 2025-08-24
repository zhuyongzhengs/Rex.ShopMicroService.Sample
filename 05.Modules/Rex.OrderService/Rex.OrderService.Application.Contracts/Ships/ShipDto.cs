using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;
using Volo.Abp.MultiTenancy;

namespace Rex.OrderService.Ships
{
    /// <summary>
    /// 配送方式Dto
    /// </summary>
    public class ShipDto : EntityDto<Guid>, IMultiTenant
    {
        /// <summary>
        /// 租户ID
        /// </summary>
        public Guid? TenantId { get; set; }

        /// <summary>
        /// 配送方式名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 是否货到付款
        /// </summary>
        public bool IsCashOnDelivery { get; set; }

        /// <summary>
        /// 首重
        /// </summary>
        public int FirstUnit { get; set; }

        /// <summary>
        /// 续重
        /// </summary>
        public int ContinueUnit { get; set; }

        /// <summary>
        /// 是否按地区设置配送费用
        /// </summary>
        public bool IsdefaultAreaFee { get; set; }

        /// <summary>
        /// 地区类型
        /// </summary>
        public int AreaType { get; set; }

        /// <summary>
        /// 首重费用
        /// </summary>
        public decimal FirstunitPrice { get; set; }

        /// <summary>
        /// 续重费用
        /// </summary>
        public decimal ContinueunitPrice { get; set; }

        /// <summary>
        /// 配送费用计算表达式
        /// </summary>
        public string? Exp { get; set; }

        /// <summary>
        /// 物流公司名称
        /// </summary>
        public string? LogiName { get; set; }

        /// <summary>
        /// 物流公司编码
        /// </summary>
        public string? LogiCode { get; set; }

        /// <summary>
        /// 是否默认
        /// </summary>
        public bool IsDefault { get; set; }

        /// <summary>
        /// 配送方式排序
        /// </summary>
        public int Sort { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        /// <remarks>
        /// 1：正常、2：停用
        /// </remarks>
        public int Status { get; set; }

        /// <summary>
        /// 是否包邮
        /// </summary>
        public bool IsfreePostage { get; set; }

        /// <summary>
        /// 地区配送费用
        /// </summary>
        public List<AreaFeeDto> AreaFees { get; set; } = new();

        /// <summary>
        /// 商品总额满多少免运费
        /// </summary>
        public decimal GoodsMoney { get; set; }

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