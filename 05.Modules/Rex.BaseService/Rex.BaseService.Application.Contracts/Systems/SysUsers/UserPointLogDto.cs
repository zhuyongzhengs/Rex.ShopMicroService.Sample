using Rex.Service.Core.Configurations;
using System;
using Volo.Abp.Application.Dtos;

namespace Rex.BaseService.Systems.SysUsers
{
    /// <summary>
    /// 用户积分记录Dto
    /// </summary>
    public class UserPointLogDto : EntityDto<Guid>
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// 显示类型
        /// </summary>
        public string TypeDisplay
        {
            get
            {
                return this.Type.GetDescription<GlobalEnums.UserPointSourceTypes>();
            }
        }

        /// <summary>
        /// 积分数量
        /// </summary>
        public int Num { get; set; }

        /// <summary>
        /// 积分余额
        /// </summary>
        public int Balance { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark { get; set; }

        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime CreationTime { get; set; }
    }
}