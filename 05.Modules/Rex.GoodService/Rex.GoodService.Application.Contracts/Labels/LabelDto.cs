using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.MultiTenancy;

namespace Rex.GoodService.Labels
{
    /// <summary>
    /// 标签Dto
    /// </summary>
    public partial class LabelDto : EntityDto<Guid>, IMultiTenant
    {
        /// <summary>
        /// 租户ID
        /// </summary>
        public Guid? TenantId { get; set; }

        /// <summary>
        /// 标签名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 标签样式
        /// </summary>
        public string Style { get; set; }

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