using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;
using static Rex.Service.Core.Configurations.GlobalEnums;

namespace Rex.BaseService.Systems.UserWeChats
{
    /// <summary>
    /// 修改微信用户Dto
    /// </summary>
    public class UserWeChatUpdateDto : EntityDto
    {
        /// <summary>
        /// 第三方登录类型
        /// </summary>
        public Int32? Type { get; set; }

        /// <summary>
        /// 关联用户表
        /// </summary>
        public Guid? UserId { get; set; }

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
        [StringLength(50)]
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
    }
}