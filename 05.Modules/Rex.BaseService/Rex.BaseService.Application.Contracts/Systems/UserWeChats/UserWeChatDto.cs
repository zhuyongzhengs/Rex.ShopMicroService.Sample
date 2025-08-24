using Rex.BaseService.Systems.SysUsers;
using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.MultiTenancy;
using static Rex.Service.Core.Configurations.GlobalEnums;

namespace Rex.BaseService.Systems.UserWeChats
{
    /// <summary>
    /// 微信用户Dto
    /// </summary>
    public class UserWeChatDto : EntityDto<Guid>, IMultiTenant
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
        /// 关联系统用户ID
        /// </summary>
        public Guid? UserId { get; set; }

        /// <summary>
        /// 关联系统用户
        /// </summary>
        public SysUserDto? SysUser { get; set; }

        /// <summary>
        /// 关联用户名称
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// OpenId
        /// </summary>
        public string OpenId { get; set; }

        /// <summary>
        /// 缓存key
        /// </summary>
        public string SessionKey { get; set; }

        /// <summary>
        /// UnionId
        /// </summary>
        public string? UnionId { get; set; }

        /// <summary>
        /// 头像
        /// </summary>
        public string Avatar { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>
        public string NickName { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public Int32? Gender { get; set; }

        /// <summary>
        /// 城市
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// 省
        /// </summary>
        public string Province { get; set; }

        /// <summary>
        /// 国家
        /// </summary>
        public string Country { get; set; }

        /// <summary>
        /// 手机号码国家编码
        /// </summary>
        public string CountryCode { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        public string Mobile { get; set; }

        /// <summary>
        /// 并发(控制)戳
        /// </summary>
        public string ConcurrencyStamp { get; set; }

        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime? CreationTime { get; set; }
    }
}