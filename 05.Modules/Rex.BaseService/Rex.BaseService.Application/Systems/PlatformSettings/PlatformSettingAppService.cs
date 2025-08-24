using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Localization;
using Rex.BaseService.Localization;
using Rex.Service.Permission.BaseServices;
using Rex.Service.Setting;
using Rex.Service.Setting.BaseServices;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Services;
using Volo.Abp.SettingManagement;
using Volo.Abp.Settings;

namespace Rex.BaseService.Systems.PlatformSettings
{
    /// <summary>
    /// 平台设置服务
    /// </summary>
    [RemoteService]
    [Authorize(BaseServicePermissions.PlatformSettings.Default)]
    public class PlatformSettingAppService : ApplicationService, IPlatformSettingAppService
    {
        /// <summary>
        /// 设置管理
        /// </summary>
        public ISettingManager SettingManager { get; set; }

        /// <summary>
        /// 本地化
        /// </summary>
        public IStringLocalizer<BaseServiceResource> Localizer { set; get; }

        /// <summary>
        /// 保存平台设置
        /// </summary>
        /// <param name="platformSetting">平台设置Dto</param>
        /// <returns></returns>
        public async Task UpdateAsync(PlatformSettingDto platformSetting)
        {
            if (platformSetting.SpecialSwitchs.Any()) await SetPlatformSettingAsync(platformSetting.SpecialSwitchs);
            if (platformSetting.PlatformSettings.Any()) await SetPlatformSettingAsync(platformSetting.PlatformSettings);
            if (platformSetting.ShareSettings.Any()) await SetPlatformSettingAsync(platformSetting.ShareSettings);
            if (platformSetting.UsersSettings.Any()) await SetPlatformSettingAsync(platformSetting.UsersSettings);
            if (platformSetting.GoodsSettings.Any()) await SetPlatformSettingAsync(platformSetting.GoodsSettings);
            if (platformSetting.OrderSettings.Any()) await SetPlatformSettingAsync(platformSetting.OrderSettings);
            if (platformSetting.PointsSettings.Any()) await SetPlatformSettingAsync(platformSetting.PointsSettings);
            if (platformSetting.CashSettings.Any()) await SetPlatformSettingAsync(platformSetting.CashSettings);
            if (platformSetting.InviteFriendSettings.Any()) await SetPlatformSettingAsync(platformSetting.InviteFriendSettings);
            if (platformSetting.FileStorageSettings.Any()) await SetPlatformSettingAsync(platformSetting.FileStorageSettings);
            if (platformSetting.WeChatMiniPrograms.Any()) await SetPlatformSettingAsync(platformSetting.WeChatMiniPrograms);
            if (platformSetting.WeChatPays.Any()) await SetPlatformSettingAsync(platformSetting.WeChatPays);
            if (platformSetting.OtherSettings.Any()) await SetPlatformSettingAsync(platformSetting.OtherSettings);
        }

        /// <summary>
        /// 设置平台值
        /// </summary>
        /// <param name="settingValues">平台值</param>
        /// <returns></returns>
        private async Task SetPlatformSettingAsync(List<SettingValueDo> settingValues)
        {
            foreach (var settingVal in settingValues)
            {
                await SettingManager.SetForCurrentTenantAsync(settingVal.Name, settingVal.Value);
            }
        }

        /// <summary>
        /// 获取平台设置
        /// </summary>
        /// <returns></returns>
        public async Task<PlatformSettingDto> GetAsync()
        {
            PlatformSettingDto platformSetting = new PlatformSettingDto();
            List<SettingValue> currentTenantSettingValues = await SettingManager.GetAllForCurrentTenantAsync();
            List<SettingValueDo> settingValues = ObjectMapper.Map<List<SettingValue>, List<SettingValueDo>>(currentTenantSettingValues);
            foreach (var setting in settingValues)
            {
                setting.Label = Localizer[$"DisplayName:{setting.Name}"];
            }

            #region 特殊开关

            platformSetting.SpecialSwitchs = settingValues.Where(p => p.Name.StartsWith(BaseServiceSettings.SpecialSwitch.Default)).ToList();

            #endregion 特殊开关

            #region 平台设置

            platformSetting.PlatformSettings = settingValues.Where(p => p.Name.StartsWith(BaseServiceSettings.PlatformSetting.Default)).ToList();

            #endregion 平台设置

            #region 分享设置

            platformSetting.ShareSettings = settingValues.Where(p => p.Name.StartsWith(BaseServiceSettings.ShareSetting.Default)).ToList();

            #endregion 分享设置

            #region 会员设置

            platformSetting.UsersSettings = settingValues.Where(p => p.Name.StartsWith(BaseServiceSettings.UsersSetting.Default)).ToList();

            #endregion 会员设置

            #region 商品设置

            platformSetting.GoodsSettings = settingValues.Where(p => p.Name.StartsWith(BaseServiceSettings.GoodsSetting.Default)).ToList();

            #endregion 商品设置

            #region 订单设置

            platformSetting.OrderSettings = settingValues.Where(p => p.Name.StartsWith(BaseServiceSettings.OrderSetting.Default)).ToList();

            #endregion 订单设置

            #region 积分设置

            platformSetting.PointsSettings = settingValues.Where(p => p.Name.StartsWith(BaseServiceSettings.PointsSetting.Default)).ToList();

            #endregion 积分设置

            #region 提现设置

            platformSetting.CashSettings = settingValues.Where(p => p.Name.StartsWith(BaseServiceSettings.CashSetting.Default)).ToList();

            #endregion 提现设置

            #region 邀请好友设置

            platformSetting.InviteFriendSettings = settingValues.Where(p => p.Name.StartsWith(BaseServiceSettings.InviteFriendSetting.Default)).ToList();

            #endregion 邀请好友设置

            #region 附件设置

            platformSetting.FileStorageSettings = settingValues.Where(p => p.Name.StartsWith(BaseServiceSettings.FileStorageSetting.Default)).ToList();

            #endregion 附件设置

            #region 微信小程序

            platformSetting.WeChatMiniPrograms = settingValues.Where(p => p.Name.StartsWith("EasyAbp.Abp.WeChat.MiniProgram")).ToList();

            #endregion 微信小程序

            #region 微信支付

            platformSetting.WeChatPays = settingValues.Where(p => p.Name.StartsWith("EasyAbp.Abp.WeChat.Pay")).ToList();

            #endregion 微信支付

            #region 其它设置

            platformSetting.OtherSettings = settingValues.Where(p => p.Name.StartsWith(BaseServiceSettings.OtherSetting.Default)).ToList();

            #endregion 其它设置

            return platformSetting;
        }
    }
}