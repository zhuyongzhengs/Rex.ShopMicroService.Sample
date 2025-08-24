using Rex.BaseService.Systems.SysUsers;
using Rex.BaseService.Systems.UserGrades;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace Rex.BaseService.Systems.UserWeChats
{
    /// <summary>
    /// 微信用户
    /// </summary>
    public class UserWeChat : FullAuditedAggregateRoot<Guid>, IMultiTenant
    {
        /// <summary>
        /// 租户ID
        /// </summary>
        public Guid? TenantId { get; set; }

        /// <summary>
        /// 第三方登录类型
        /// </summary>
        public Int32? Type { get; set; }

        /// <summary>
        /// 关联用户表
        /// </summary>
        [Required]
        public Guid? UserId { get; set; }

        /// <summary>
        /// 系统用户
        /// </summary>
        [ForeignKey(nameof(UserId))]
        public SysUser? SysUser { get; set; }

        /// <summary>
        /// OpenId
        /// </summary>
        [StringLength(50)]
        public string OpenId { get; set; }

        /// <summary>
        /// 缓存key
        /// </summary>
        [StringLength(255)]
        public string SessionKey { get; set; }

        /// <summary>
        /// UnionId
        /// </summary>
        [StringLength(50)]
        public string? UnionId { get; set; }

        /// <summary>
        /// 头像
        /// </summary>
        [StringLength(1000)]
        public string? Avatar { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>
        [StringLength(50)]
        public string NickName { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public Int16? Gender { get; set; }

        /// <summary>
        /// 城市
        /// </summary>
        [StringLength(80)]
        public string? City { get; set; }

        /// <summary>
        /// 省
        /// </summary>
        [StringLength(80)]
        public string? Province { get; set; }

        /// <summary>
        /// 国家
        /// </summary>
        [StringLength(80)]
        public string? Country { get; set; }

        /// <summary>
        /// 手机号码国家编码
        /// </summary>
        [StringLength(20)]
        public string? CountryCode { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        [StringLength(20)]
        public string? Mobile { get; set; }

        /// <summary>
        /// 生日
        /// </summary>
        public DateTime? BirthDate { get; set; }
    }
}