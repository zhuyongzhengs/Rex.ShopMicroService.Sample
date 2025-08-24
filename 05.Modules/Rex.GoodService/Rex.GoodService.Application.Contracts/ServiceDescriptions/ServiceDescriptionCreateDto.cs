using Volo.Abp.Application.Dtos;

namespace Rex.GoodService.ServiceDescriptions
{
    /// <summary>
    /// 创建商城服务描述Dto
    /// </summary>
    public class ServiceDescriptionCreateDto : EntityDto
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 是否展示
        /// </summary>
        public bool IsShow { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }
    }
}