using System;
using Volo.Abp.Application.Dtos;

namespace Rex.BaseService.Systems.SysUsers
{
    /// <summary>
    /// 用户资料修改
    /// </summary>
    public class SysUserInformationUpdateDto : EntityDto
    {
        /// <summary>
        /// 昵称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 头像
        /// </summary>
        public string? Avatar { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public Int16? Gender { get; set; }

        /// <summary>
        /// 生日
        /// </summary>
        public DateTime? BirthDate { get; set; }
    }
}