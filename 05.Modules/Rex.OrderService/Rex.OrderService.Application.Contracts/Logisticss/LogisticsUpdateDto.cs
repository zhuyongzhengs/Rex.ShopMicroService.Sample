﻿using System;
using Volo.Abp.Application.Dtos;

namespace Rex.OrderService.Logisticss
{
    /// <summary>
    /// 修改物流Dto
    /// </summary>
    public class LogisticsUpdateDto : EntityDto
    {
        /// <summary>
        /// 租户ID
        /// </summary>
        public Guid? TenantId { get; set; }

        /// <summary>
        /// 物流公司编码
        /// </summary>
        public string LogiCode { get; set; }

        /// <summary>
        /// 物流公司名称
        /// </summary>
        public string LogiName { get; set; }

        /// <summary>
        /// 物流logo
        /// </summary>
        public string? ImgUrl { get; set; }

        /// <summary>
        /// 物流电话
        /// </summary>
        public string? Phone { get; set; }

        /// <summary>
        /// 物流网址
        /// </summary>
        public string? Url { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }

        /// <summary>
        /// 并发(控制)戳
        /// </summary>
        public string ConcurrencyStamp { get; set; }
    }
}