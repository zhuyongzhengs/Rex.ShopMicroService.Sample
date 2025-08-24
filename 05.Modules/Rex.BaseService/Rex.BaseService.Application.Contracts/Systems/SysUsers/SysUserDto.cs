using Rex.BaseService.Systems.UserGrades;
using System;
using Volo.Abp.Identity;

namespace Rex.BaseService.Systems.SysUsers
{
    /// <summary>
    /// 系统用户Dto
    /// </summary>
    public class SysUserDto : IdentityUserDto
    {
        /// <summary>
        /// 头像
        /// </summary>
        public string Avatar { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public Int16? Gender { get; set; }

        /// <summary>
        /// 余额
        /// </summary>
        public decimal Balance { get; set; }

        /// <summary>
        /// 积分
        /// </summary>
        public int Point { get; set; }

        /// <summary>
        /// 用户等级ID
        /// </summary>
        public Guid? GradeId { get; set; }

        /// <summary>
        /// 用户等级
        /// </summary>
        public UserGradeDto? Grade { get; set; }

        /// <summary>
        /// 推荐人
        /// </summary>
        public Guid? ParentId { get; set; }

        /// <summary>
        /// 生日
        /// </summary>
        public DateTime? BirthDate { get; set; }
    }
}