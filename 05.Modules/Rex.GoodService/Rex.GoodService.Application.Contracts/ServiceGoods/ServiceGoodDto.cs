using System;
using Volo.Abp.Application.Dtos;

namespace Rex.GoodService.ServiceGoods
{
    /// <summary>
    /// 服务商品Dto
    /// </summary>
    public class ServiceGoodDto : EntityDto<Guid>
    {
        /// <summary>
        /// 标题名称
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 缩略图
        /// </summary>
        public string Thumbnail { get; set; }

        /// <summary>
        /// 项目概述
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// 项目详细说明
        /// </summary>
        public string ContentBody { get; set; }

        /// <summary>
        /// 允许购买会员级别
        /// </summary>
        public string[] AllowedMemberships { get; set; }

        /// <summary>
        /// 可消费门店
        /// </summary>
        public string[] ConsumableStores { get; set; }

        /// <summary>
        /// 项目状态
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 项目重复购买次数
        /// </summary>
        public int MaxBuyNumber { get; set; }

        /// <summary>
        /// 项目可销售数量
        /// </summary>
        public int Amount { get; set; }

        /// <summary>
        /// 项目开始时间
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// 项目截止时间
        /// </summary>
        public DateTime EndTime { get; set; }

        /// <summary>
        /// 核销有效期类型
        /// </summary>
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
        public int TicketNumber { get; set; }

        /// <summary>
        /// 售价
        /// </summary>
        public decimal Money { get; set; }

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