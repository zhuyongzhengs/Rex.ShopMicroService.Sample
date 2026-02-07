using System;
using Volo.Abp.Domain.Entities.Events.Distributed;
using Volo.Abp.MultiTenancy;

namespace Rex.Service.Core.Models
{
    /// <summary>
    /// 当前用户信息Eto
    /// </summary>
    public class CurrentSysUserEto : EntityEto<Guid>
    {
        /// <summary>
        /// 登陆账号
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 用户名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 电子邮箱
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 金额
        /// </summary>
        public decimal Money { get; set; }

        /// <summary>
        /// 余额
        /// </summary>
        public decimal Balance { get; set; }

        /// <summary>
        /// 用户昵称
        /// </summary>
        public string NickName { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        public string? PhoneNumber { get; set; }

        /// <summary>
        /// 头像
        /// </summary>
        public string? Avatar { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public Int16? Gender { get; set; }

        /// <summary>
        /// 积分
        /// </summary>
        public int Point { get; set; }

        /// <summary>
        /// 用户等级
        /// </summary>
        public GradeEto? Grade { get; set; }

        /// <summary>
        /// 推荐人
        /// </summary>
        public Guid? ParentId { get; set; }

        /// <summary>
        /// OpenId
        /// </summary>
        public string OpenId { get; set; }

        /// <summary>
        /// 生日
        /// </summary>
        public DateTime? BirthDate { get; set; }
    }
}