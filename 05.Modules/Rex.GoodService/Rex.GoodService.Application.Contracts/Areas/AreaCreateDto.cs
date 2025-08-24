using Volo.Abp.Application.Dtos;

namespace Rex.GoodService.Areas
{
    /// <summary>
    /// 创建区域DtoDto
    /// </summary>
    public class AreaCreateDto : EntityDto
    {
        /// <summary>
        /// 父级ID
        /// </summary>
        public long? ParentId { get; set; }

        /// <summary>
        /// 地区深度
        /// </summary>
        public int? Depth { get; set; }

        /// <summary>
        /// 地区名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 邮编
        /// </summary>
        public string? PostalCode { get; set; }

        /// <summary>
        /// 地区排序
        /// </summary>
        public int Sort { get; set; }
    }
}