using System;
using Volo.Abp.Domain.Entities.Events.Distributed;

namespace Rex.Service.Core.Models
{
    /// <summary>
    /// 等级Eto
    /// </summary>
    public class GradeEto : EntityEto<Guid>
    {
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }
    }
}