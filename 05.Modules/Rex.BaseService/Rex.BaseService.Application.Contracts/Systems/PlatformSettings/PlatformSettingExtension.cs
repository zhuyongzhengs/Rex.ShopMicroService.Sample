using Rex.Service.Setting;
using System.Collections.Generic;
using System.Linq;

namespace Rex.BaseService.Systems.PlatformSettings
{
    /// <summary>
    /// 平台设置Dto扩展
    /// </summary>
    public static class PlatformSettingExtension
    {
        /// <summary>
        /// 合并所有设置
        /// </summary>
        /// <param name="platformSetting">平台设置</param>
        /// <returns></returns>
        public static List<SettingValueDo> MergeAllSettings(this PlatformSettingDto platformSetting)
        {
            return new List<List<SettingValueDo>>
            {
                platformSetting.SpecialSwitchs,
                platformSetting.PlatformSettings,
                platformSetting.ShareSettings,
                platformSetting.UsersSettings,
                platformSetting.GoodsSettings,
                platformSetting.OrderSettings,
                platformSetting.PointsSettings,
                platformSetting.CashSettings,
                platformSetting.InviteFriendSettings,
                platformSetting.FileStorageSettings,
                platformSetting.WeChatMiniPrograms,
                platformSetting.WeChatPays,
                platformSetting.OtherSettings
            }.SelectMany(x => x).ToList();
        }
    }
}