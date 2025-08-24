using System;
using Volo.Abp.Identity;

namespace Rex.BaseService.Systems.SysUsers
{
    /// <summary>
    /// 创建系统用户Dto
    /// </summary>
    public class SysUserCreateDto : IdentityUserCreateDto
    {
        /// <summary>
        /// 头像
        /// </summary>
        public string? Avatar { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public Int16? Gender { get; set; }

        /// <summary>
        /// 用户等级ID
        /// </summary>
        public Guid? GradeId { get; set; }

        /// <summary>
        /// 推荐人
        /// </summary>
        public Guid? ParentId { get; set; }
    }
}