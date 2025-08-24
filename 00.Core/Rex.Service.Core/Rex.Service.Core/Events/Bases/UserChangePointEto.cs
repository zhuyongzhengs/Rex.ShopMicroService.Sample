using System;
using Volo.Abp.Domain.Entities.Events.Distributed;

namespace Rex.Service.Core.Events.Bases
{
    /// <summary>
    /// 用户积分变更Eto
    /// </summary>
    public class UserChangePointEto : EntityEto
    {
        /// <summary>
        /// 事件名称
        /// </summary>
        public const string EventName = "User.Change.Point";

        /// <summary>
        /// 用户ID
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// 积分类型
        /// </summary>
        public int PointType { get; set; }

        /// <summary>
        /// 积分
        /// </summary>
        public int Point { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark { get; set; }
    }
}