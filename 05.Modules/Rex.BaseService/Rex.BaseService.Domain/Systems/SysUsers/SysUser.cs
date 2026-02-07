using Rex.BaseService.Systems.UserGrades;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Volo.Abp.Identity;
using static Rex.Service.Permission.BaseServices.BaseServicePermissions;

namespace Rex.BaseService.Systems.SysUsers
{
    /// <summary>
    /// 系统用户
    /// </summary>
    public class SysUser : IdentityUser
    {
        public SysUser()
        {
        }

        public SysUser(Guid id, string userName, string email, bool isActive)
        {
            Id = id;
            UserName = userName;
            NormalizedUserName = userName.ToUpperInvariant();
            Email = email;
            NormalizedEmail = email.ToUpperInvariant();
            IsActive = isActive;
            SecurityStamp = Guid.NewGuid().ToString();
            ConcurrencyStamp = Guid.NewGuid().ToString("N");
        }

        /// <summary>
        /// 鉴别器
        /// </summary>
        [StringLength(50), DefaultValue("")]
        public string? Discriminator { get; set; } = nameof(SysUser);

        /// <summary>
        /// 头像
        /// </summary>
        [StringLength(1000)]
        public string Avatar { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public Int16? Gender { get; set; }

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
        [ForeignKey(nameof(GradeId))]
        public UserGrade? Grade { get; set; }

        /// <summary>
        /// 余额
        /// </summary>
        public decimal Balance { get; set; }

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