using System;
using Volo.Abp.Domain.Entities.Events.Distributed;

namespace Rex.Service.Core.Events.Bases
{
    /// <summary>
    /// 变更用户余额Eto
    /// </summary>
    public class ChangeUserBalanceEto : EntityEto
    {
        /// <summary>
        /// 事件名称
        /// </summary>
        public const string EventName = "Change.User.Balance";

        /// <summary>
        /// 用户ID
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// 金额
        /// </summary>
        public decimal Money { get; set; }

        /// <summary>
        /// 关联ID
        /// </summary>
        public string SourceId { get; set; }

        /// <summary>
        /// 服务费金额(提现)
        /// </summary>
        public decimal? CateMoney { get; set; }
    }
}