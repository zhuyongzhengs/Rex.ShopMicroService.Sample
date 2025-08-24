using System;
using Volo.Abp.Application.Dtos;

namespace Rex.GoodService.Products
{
    /// <summary>
    /// 创建货品三级佣金Dto
    /// </summary>
    public class ProductDistributionCreateDto : EntityDto
    {
        /// <summary>
        /// 货品ID
        /// </summary>
        public Guid ProductId { get; set; }

        /// <summary>
        /// 货品货号
        /// </summary>
        public string ProductSn { get; set; }

        /// <summary>
        /// 一级佣金
        /// </summary>
        public decimal LevelOne { get; set; }

        /// <summary>
        /// 二级佣金
        /// </summary>
        public decimal LevelTwo { get; set; }

        /// <summary>
        /// 三级佣金
        /// </summary>
        public decimal LevelThree { get; set; }
    }
}