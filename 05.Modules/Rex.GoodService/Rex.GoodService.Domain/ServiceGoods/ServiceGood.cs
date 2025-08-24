using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace Rex.GoodService.ServiceGoods
{
    /// <summary>
    /// 服务商品
    /// </summary>
    public class ServiceGood : FullAuditedAggregateRoot<Guid>, IMultiTenant
    {
        /// <summary>
        /// 租户ID
        /// </summary>
        public Guid? TenantId { get; set; }

        /// <summary>
        /// 标题名称
        /// </summary>
        [Required]
        [StringLength(50)]
        public string Title { get; set; }

        /// <summary>
        /// 缩略图
        /// </summary>
        [Required]
        [StringLength(255)]
        public string Thumbnail { get; set; }

        /// <summary>
        /// 项目概述
        /// </summary>
        [StringLength(255)]
        public string? Description { get; set; }

        /// <summary>
        /// 项目详细说明
        /// </summary>
        [Required]
        public string ContentBody { get; set; }

        /// <summary>
        /// 允许购买会员级别
        /// </summary>
        [Required]
        [StringLength(255)]
        public string AllowedMembership { get; set; }

        /// <summary>
        /// 可消费门店
        /// </summary>
        [Required]
        [StringLength(255)]
        public string ConsumableStore { get; set; }

        /// <summary>
        /// 项目状态
        /// </summary>
        [Required]
        public int Status { get; set; }

        /// <summary>
        /// 项目重复购买次数
        /// </summary>
        [Required]
        public int MaxBuyNumber { get; set; }

        /// <summary>
        /// 项目可销售数量
        /// </summary>
        [Required]
        public int Amount { get; set; }

        /// <summary>
        /// 项目开始时间
        /// </summary>
        [Required]
        public DateTime StartTime { get; set; }

        /// <summary>
        /// 项目截止时间
        /// </summary>
        [Required]
        public DateTime EndTime { get; set; }

        /// <summary>
        /// 核销有效期类型
        /// </summary>
        [Required]
        public int ValidityType { get; set; }

        /// <summary>
        /// 核销开始时间
        /// </summary>
        public DateTime? ValidityStartTime { get; set; }

        /// <summary>
        /// 核销结束时间
        /// </summary>
        public DateTime? ValidityEndTime { get; set; }

        /// <summary>
        /// 核销服务券数量
        /// </summary>
        [Required]
        public int TicketNumber { get; set; }

        /// <summary>
        /// 售价
        /// </summary>
        [Required]
        public decimal Money { get; set; }
    }
}