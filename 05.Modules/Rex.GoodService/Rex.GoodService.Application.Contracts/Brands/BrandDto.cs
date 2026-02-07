using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.MultiTenancy;

namespace Rex.GoodService.Brands
{
    /// <summary>
    /// 品牌Dto
    /// </summary>
    public class BrandDto : EntityDto<Guid>
    {
        /// <summary>
        /// 品牌名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 品牌LOGO
        /// </summary>
        public string LogoImageUrl { get; set; }

        /// <summary>
        /// 品牌排序
        /// </summary>
        public int? Sort { get; set; }

        /// <summary>
        /// 是否显示
        /// </summary>
        public bool IsShow { get; set; }

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