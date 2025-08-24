using Volo.Abp.Application.Dtos;

namespace Rex.GoodService.Brands
{
    /// <summary>
    /// 创建品牌Dto
    /// </summary>
    public class BrandCreateDto : EntityDto
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
    }
}