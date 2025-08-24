using Rex.Service.Setting;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;

namespace Rex.BaseService.Systems.PlatformSettings
{
    /// <summary>
    /// 平台设置Dto
    /// </summary>
    public class PlatformSettingDto : EntityDto
    {
        /// <summary>
        /// 特殊开关
        /// </summary>
        public List<SettingValueDo> SpecialSwitchs { get; set; } = new();

        /// <summary>
        /// 平台设置
        /// </summary>
        public List<SettingValueDo> PlatformSettings { get; set; } = new();

        /// <summary>
        /// 分享设置
        /// </summary>
        public List<SettingValueDo> ShareSettings { get; set; } = new();

        /// <summary>
        /// 会员设置
        /// </summary>
        public List<SettingValueDo> UsersSettings { get; set; } = new();

        /// <summary>
        /// 商品设置
        /// </summary>
        public List<SettingValueDo> GoodsSettings { get; set; } = new();

        /// <summary>
        /// 订单设置
        /// </summary>
        public List<SettingValueDo> OrderSettings { get; set; } = new();

        /// <summary>
        /// 积分设置
        /// </summary>
        public List<SettingValueDo> PointsSettings { get; set; } = new();

        /// <summary>
        /// 提现设置
        /// </summary>
        public List<SettingValueDo> CashSettings { get; set; } = new();

        /// <summary>
        /// 邀请好友设置
        /// </summary>
        public List<SettingValueDo> InviteFriendSettings { get; set; } = new();

        /// <summary>
        /// 附件设置
        /// </summary>
        public List<SettingValueDo> FileStorageSettings { get; set; } = new();

        /// <summary>
        /// 微信小程序
        /// </summary>
        public List<SettingValueDo> WeChatMiniPrograms { get; set; } = new();

        /// <summary>
        /// 微信支付
        /// </summary>
        public List<SettingValueDo> WeChatPays { get; set; } = new();

        /// <summary>
        /// 其它设置
        /// </summary>
        public List<SettingValueDo> OtherSettings { get; set; } = new();
    }
}