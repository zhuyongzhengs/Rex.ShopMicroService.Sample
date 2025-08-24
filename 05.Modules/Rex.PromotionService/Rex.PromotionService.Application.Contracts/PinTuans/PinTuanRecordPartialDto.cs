using System;
using System.Collections.Generic;

namespace Rex.PromotionService.PinTuans
{
    /// <summary>
    /// 拼团记录Dto
    /// </summary>
    public partial class PinTuanRecordDto
    {
        /// <summary>
        /// 用户头像
        /// </summary>
        public string UserAvatar { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>
        public string NickName { get; set; }

        /// <summary>
        /// 参与队员信息
        /// </summary>
        public List<PinTuanRecordTeamDto> Teams { get; set; }

        /// <summary>
        /// 参与数量
        /// </summary>
        public int TeamNums { get; set; }

        /// <summary>
        /// 参与人数计算
        /// </summary>
        public int PeopleNumber { get; set; } = 0;

        /// <summary>
        /// 剩余时间
        /// </summary>
        public int LastTime { get; set; }

        /// <summary>
        /// 是否过期
        /// </summary>
        public bool IsOverdue { get; set; } = false;

        /// <summary>
        /// 活动名称
        /// </summary>
        public string RuleName { get; set; }

        /// <summary>
        /// 商品名称
        /// </summary>
        public string GoodName { get; set; }
    }

    /// <summary>
    /// 参与拼团人员
    /// </summary>
    public class PinTuanRecordTeamDto
    {
        /// <summary>
        /// 用户头像
        /// </summary>
        public string UserAvatar { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>
        public string NickName { get; set; }

        /// <summary>
        /// 记录编号
        /// </summary>
        public Guid RecordId { get; set; }

        /// <summary>
        /// 拼团队伍编号
        /// </summary>
        public Guid TeamId { get; set; }

        /// <summary>
        /// 用户编号
        /// </summary>
        public Guid UserId { get; set; }
    }
}