using System;
using Volo.Abp.Application.Dtos;

namespace Rex.GoodService.Goods
{
    /// <summary>
    /// 修改商品参数Dto
    /// </summary>
    public class GoodParamUpdateDto : EntityDto
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

        /// <summary>
        /// 并发(控制)戳
        /// </summary>
        public string ConcurrencyStamp { get; set; }
    }
}