using Rex.BaseService.Systems.SysUsers;
using Rex.Service.Core.Configurations;
using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.MultiTenancy;

namespace Rex.BaseService.Systems.UserBalances
{
    /// <summary>
    /// 用户余额Dto
    /// </summary>
    public class UserBalanceDto : EntityDto<Guid>
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// 用户
        /// </summary>
        public SysUserDto? User { get; set; }

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
                return this.Type.GetDescription<GlobalEnums.UserBalanceType>();
            }
        }

        /// <summary>
        /// 金额
        /// </summary>
        public decimal Money { get; set; }

        /// <summary>
        /// 余额
        /// </summary>
        public decimal Balance { get; set; }

        /// <summary>
        /// 资源id
        /// </summary>
        public string? SourceId { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string? Memo { get; set; }

        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime CreationTime { get; set; }
    }
}