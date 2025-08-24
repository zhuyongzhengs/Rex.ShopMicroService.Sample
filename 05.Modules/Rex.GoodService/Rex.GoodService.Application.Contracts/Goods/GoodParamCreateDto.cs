using System;
using Volo.Abp.Application.Dtos;

namespace Rex.GoodService.Goods
{
    /// <summary>
    /// 创建商品参数Dto
    /// </summary>
    public class GoodParamCreateDto : EntityDto
    {
        /// <summary>
        /// 参数名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 参数值
        /// </summary>
        public string[]? Values { get; set; }

        /// <summary>
        /// 参数类型
        /// </summary>
        public string? Type { get; set; }
    }
}