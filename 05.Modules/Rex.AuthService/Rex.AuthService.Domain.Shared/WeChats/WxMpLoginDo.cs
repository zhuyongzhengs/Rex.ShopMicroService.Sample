using System;

namespace Rex.AuthService.WeChats
{
    /// <summary>
    /// 微信小程序登录
    /// </summary>
    [Serializable]
    public class WxMpLoginDo
    {
        /// <summary>
        /// 用户唯一标识
        /// </summary>
        public string OpenId { get; set; }

        /// <summary>
        /// 会话密钥
        /// </summary>
        public string SessionKey { get; set; }

        /// <summary>
        /// 用户在开放平台的唯一标识符，若当前小程序已绑定到微信开放平台帐号下会返回
        /// </summary>
        public string Unionid { get; set; }

        /// <summary>
        /// 邀请码
        /// </summary>
        public string InviteCode { get; set; }

        /// <summary>
        /// 国家编码
        /// </summary>
        public string CountryCode { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        public string PhoneNumber { get; set; }
    }
}