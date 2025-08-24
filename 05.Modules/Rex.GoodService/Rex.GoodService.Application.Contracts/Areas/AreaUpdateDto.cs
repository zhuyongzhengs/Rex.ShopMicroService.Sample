using Volo.Abp.Application.Dtos;

namespace Rex.GoodService.Areas
{
    /// <summary>
    /// 修改区域Dto
    /// </summary>
    public class AreaUpdateDto : EntityDto
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

        /// <summary>
        /// 并发(控制)戳
        /// </summary>
        public string ConcurrencyStamp { get; set; }
    }
}