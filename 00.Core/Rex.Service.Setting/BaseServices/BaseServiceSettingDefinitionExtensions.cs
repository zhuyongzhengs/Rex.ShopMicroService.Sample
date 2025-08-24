using System;
using Volo.Abp.Localization;
using Volo.Abp.Settings;

namespace Rex.Service.Setting.BaseServices
{
    /// <summary>
    /// Base设置定义
    /// </summary>
    public static class BaseServiceSettingDefinitionExtensions
    {
        /// <summary>
        /// 添加平台设置
        /// </summary>
        /// <param name="localizableStr">本地化</param>
        public static void AddBasePlatformSettingDefine(this ISettingDefinitionContext context, Func<string, LocalizableString> localizableStr)
        {
            context.Add(
            /* 特殊开关 */
            new SettingDefinition(
                BaseServiceSettings.SpecialSwitch.ShowStore,
                false.ToString(),
                localizableStr.Invoke("DisplayName:BaseService.SpecialSwitch.ShowStore"),
                localizableStr.Invoke("Description:BaseService.SpecialSwitch.ShowStore"),
                true
                ),
            new SettingDefinition(
                BaseServiceSettings.SpecialSwitch.ShowCharge,
                false.ToString(),
                localizableStr.Invoke("DisplayName:BaseService.SpecialSwitch.ShowCharge"),
                localizableStr.Invoke("Description:BaseService.SpecialSwitch.ShowCharge"),
                true
                ),

            /* 平台设置 */
            new SettingDefinition(
                BaseServiceSettings.PlatformSetting.ShopName,
                "Rex商城管理平台",
                localizableStr.Invoke("DisplayName:BaseService.PlatformSetting.ShopName"),
                localizableStr.Invoke("Description:BaseService.PlatformSetting.ShopName"),
                true
                ),
            new SettingDefinition(
                BaseServiceSettings.PlatformSetting.ShopDesc,
                "平台描述会展示在前台及微信分享描述",
                localizableStr.Invoke("DisplayName:BaseService.PlatformSetting.ShopDesc"),
                localizableStr.Invoke("Description:BaseService.PlatformSetting.ShopDesc"),
                true
                ),
            new SettingDefinition(
                BaseServiceSettings.PlatformSetting.ShopBeiAn,
                string.Empty,
                localizableStr.Invoke("DisplayName:BaseService.PlatformSetting.ShopBeiAn"),
                localizableStr.Invoke("Description:BaseService.PlatformSetting.ShopBeiAn"),
                true
                ),
            new SettingDefinition(
                BaseServiceSettings.PlatformSetting.AboutArticle,
                string.Empty,
                localizableStr.Invoke("DisplayName:BaseService.PlatformSetting.AboutArticle"),
                localizableStr.Invoke("Description:BaseService.PlatformSetting.AboutArticle"),
                true
                ),
            new SettingDefinition(
                BaseServiceSettings.PlatformSetting.UserAgreement,
                string.Empty,
                localizableStr.Invoke("DisplayName:BaseService.PlatformSetting.UserAgreement"),
                localizableStr.Invoke("Description:BaseService.PlatformSetting.UserAgreement"),
                true
                ),
            new SettingDefinition(
                BaseServiceSettings.PlatformSetting.PrivacyPolicy,
                string.Empty,
                localizableStr.Invoke("DisplayName:BaseService.PlatformSetting.PrivacyPolicy"),
                localizableStr.Invoke("Description:BaseService.PlatformSetting.PrivacyPolicy"),
                true
                ),
            new SettingDefinition(
                BaseServiceSettings.PlatformSetting.PlatformLogo,
                string.Empty,
                localizableStr.Invoke("DisplayName:BaseService.PlatformSetting.PlatformLogo"),
                localizableStr.Invoke("Description:BaseService.PlatformSetting.PlatformLogo"),
                true
                ),
            new SettingDefinition(
                BaseServiceSettings.PlatformSetting.DefaultPic,
                string.Empty,
                localizableStr.Invoke("DisplayName:BaseService.PlatformSetting.DefaultPic"),
                localizableStr.Invoke("Description:BaseService.PlatformSetting.DefaultPic"),
                true
                ),
            new SettingDefinition(
                BaseServiceSettings.PlatformSetting.StoreSwitch,
                false.ToString(),
                localizableStr.Invoke("DisplayName:BaseService.PlatformSetting.StoreSwitch"),
                localizableStr.Invoke("Description:BaseService.PlatformSetting.StoreSwitch"),
                true
                ),
            new SettingDefinition(
                BaseServiceSettings.PlatformSetting.InvoiceSwitch,
                true.ToString(),
                localizableStr.Invoke("DisplayName:BaseService.PlatformSetting.InvoiceSwitch"),
                localizableStr.Invoke("Description:BaseService.PlatformSetting.InvoiceSwitch"),
                true
                ),
            new SettingDefinition(
                BaseServiceSettings.PlatformSetting.RecommendKeys,
                "商品|内容|管理|平台",
                localizableStr.Invoke("DisplayName:BaseService.PlatformSetting.RecommendKeys"),
                localizableStr.Invoke("Description:BaseService.PlatformSetting.RecommendKeys"),
                true
                ),

            /* 分享设置 */
            new SettingDefinition(
                BaseServiceSettings.ShareSetting.ShareTitle,
                "优质好店邀您共享",
                localizableStr.Invoke("DisplayName:BaseService.ShareSetting.ShareTitle"),
                localizableStr.Invoke("Description:BaseService.ShareSetting.ShareTitle"),
                true
                ),
            new SettingDefinition(
                BaseServiceSettings.ShareSetting.ShareDesc,
                "优质好店邀您共享",
                localizableStr.Invoke("DisplayName:BaseService.ShareSetting.ShareDesc"),
                localizableStr.Invoke("Description:BaseService.ShareSetting.ShareDesc"),
                true
                ),
            new SettingDefinition(
                BaseServiceSettings.ShareSetting.ShareImage,
                string.Empty,
                localizableStr.Invoke("DisplayName:BaseService.ShareSetting.ShareImage"),
                localizableStr.Invoke("Description:BaseService.ShareSetting.ShareImage"),
                true
                ),

            /* 会员设置 */
            new SettingDefinition(
                BaseServiceSettings.UsersSetting.IsBindMobile,
                true.ToString(),
                localizableStr.Invoke("DisplayName:BaseService.UsersSetting.IsBindMobile"),
                localizableStr.Invoke("Description:BaseService.UsersSetting.IsBindMobile"),
                true
                ),
            new SettingDefinition(
                BaseServiceSettings.UsersSetting.ShopMobile,
                string.Empty,
                localizableStr.Invoke("DisplayName:BaseService.UsersSetting.ShopMobile"),
                localizableStr.Invoke("Description:BaseService.UsersSetting.ShopMobile"),
                true
                ),

            /* 商品设置 */
            new SettingDefinition(
                BaseServiceSettings.GoodsSetting.GoodsStocksWarn,
                string.Empty,
                localizableStr.Invoke("DisplayName:BaseService.GoodsSetting.GoodsStocksWarn"),
                localizableStr.Invoke("Description:BaseService.GoodsSetting.GoodsStocksWarn"),
                false
                ),

            /* 订单设置 */
            new SettingDefinition(
                BaseServiceSettings.OrderSetting.OrderCancelTime,
                string.Empty,
                localizableStr.Invoke("DisplayName:BaseService.OrderSetting.OrderCancelTime"),
                localizableStr.Invoke("Description:BaseService.OrderSetting.OrderCancelTime"),
                true
                ),
            new SettingDefinition(
                BaseServiceSettings.OrderSetting.OrderCompleteTime,
                string.Empty,
                localizableStr.Invoke("DisplayName:BaseService.OrderSetting.OrderCompleteTime"),
                localizableStr.Invoke("Description:BaseService.OrderSetting.OrderCompleteTime"),
                true
                ),
            new SettingDefinition(
                BaseServiceSettings.OrderSetting.OrderAutoSignTime,
                string.Empty,
                localizableStr.Invoke("DisplayName:BaseService.OrderSetting.OrderAutoSignTime"),
                localizableStr.Invoke("Description:BaseService.OrderSetting.OrderAutoSignTime"),
                true
                ),
            new SettingDefinition(
                BaseServiceSettings.OrderSetting.OrderAutoEvalTime,
                string.Empty,
                localizableStr.Invoke("DisplayName:BaseService.OrderSetting.OrderAutoEvalTime"),
                localizableStr.Invoke("Description:BaseService.OrderSetting.OrderAutoEvalTime"),
                true
                ),
            new SettingDefinition(
                BaseServiceSettings.OrderSetting.RemindOrderTime,
                string.Empty,
                localizableStr.Invoke("DisplayName:BaseService.OrderSetting.RemindOrderTime"),
                localizableStr.Invoke("Description:BaseService.OrderSetting.RemindOrderTime"),
                true
                ),
            new SettingDefinition(
                BaseServiceSettings.OrderSetting.ReshipName,
                string.Empty,
                localizableStr.Invoke("DisplayName:BaseService.OrderSetting.ReshipName"),
                localizableStr.Invoke("Description:BaseService.OrderSetting.ReshipName"),
                true
                ),
            new SettingDefinition(
                BaseServiceSettings.OrderSetting.ReshipMobile,
                string.Empty,
                localizableStr.Invoke("DisplayName:BaseService.OrderSetting.ReshipMobile"),
                localizableStr.Invoke("Description:BaseService.OrderSetting.ReshipMobile"),
                true
                ),
            new SettingDefinition(
                BaseServiceSettings.OrderSetting.ReshipAreaId,
                string.Empty,
                localizableStr.Invoke("DisplayName:BaseService.OrderSetting.ReshipAreaId"),
                localizableStr.Invoke("Description:BaseService.OrderSetting.ReshipAreaId"),
                true
                ),
            new SettingDefinition(
                BaseServiceSettings.OrderSetting.ReshipCoordinate,
                string.Empty,
                localizableStr.Invoke("DisplayName:BaseService.OrderSetting.ReshipCoordinate"),
                localizableStr.Invoke("Description:BaseService.OrderSetting.ReshipCoordinate"),
                true
                ),
            new SettingDefinition(
                BaseServiceSettings.OrderSetting.ReshipAddress,
                string.Empty,
                localizableStr.Invoke("DisplayName:BaseService.OrderSetting.ReshipAddress"),
                localizableStr.Invoke("Description:BaseService.OrderSetting.ReshipAddress"),
                true
                ),
            new SettingDefinition(
                BaseServiceSettings.OrderSetting.StoreOrderAutomaticDelivery,
                false.ToString(),
                localizableStr.Invoke("DisplayName:BaseService.OrderSetting.StoreOrderAutomaticDelivery"),
                localizableStr.Invoke("Description:BaseService.OrderSetting.StoreOrderAutomaticDelivery"),
                true
                ),

            /* 积分设置 */
            new SettingDefinition(
                BaseServiceSettings.PointsSetting.PointSwitch,
                true.ToString(),
                localizableStr.Invoke("DisplayName:BaseService.PointsSetting.PointSwitch"),
                localizableStr.Invoke("Description:BaseService.PointsSetting.PointSwitch"),
                true
                ),
            new SettingDefinition(
                BaseServiceSettings.PointsSetting.PointDiscountedProportion,
                string.Empty,
                localizableStr.Invoke("DisplayName:BaseService.PointsSetting.PointDiscountedProportion"),
                localizableStr.Invoke("Description:BaseService.PointsSetting.PointDiscountedProportion"),
                true
                ),
            new SettingDefinition(
                BaseServiceSettings.PointsSetting.OrdersPointProportion,
                string.Empty,
                localizableStr.Invoke("DisplayName:BaseService.PointsSetting.OrdersPointProportion"),
                localizableStr.Invoke("Description:BaseService.PointsSetting.OrdersPointProportion"),
                true
                ),
            new SettingDefinition(
                BaseServiceSettings.PointsSetting.OrdersRewardProportion,
                string.Empty,
                localizableStr.Invoke("DisplayName:BaseService.PointsSetting.OrdersRewardProportion"),
                localizableStr.Invoke("Description:BaseService.PointsSetting.OrdersRewardProportion"),
                true
                ),
            new SettingDefinition(
                BaseServiceSettings.PointsSetting.SignPointType,
                string.Empty,
                localizableStr.Invoke("DisplayName:BaseService.PointsSetting.SignPointType"),
                localizableStr.Invoke("Description:BaseService.PointsSetting.SignPointType"),
                true
                ),
            new SettingDefinition(
                BaseServiceSettings.PointsSetting.FirstSignPoint,
                string.Empty,
                localizableStr.Invoke("DisplayName:BaseService.PointsSetting.FirstSignPoint"),
                localizableStr.Invoke("Description:BaseService.PointsSetting.FirstSignPoint"),
                true
                ),
            new SettingDefinition(
                BaseServiceSettings.PointsSetting.ContinuitySignAdditional,
                string.Empty,
                localizableStr.Invoke("DisplayName:BaseService.PointsSetting.ContinuitySignAdditional"),
                localizableStr.Invoke("Description:BaseService.PointsSetting.ContinuitySignAdditional"),
                true
                ),
            new SettingDefinition(
                BaseServiceSettings.PointsSetting.SignMostPoint,
                string.Empty,
                localizableStr.Invoke("DisplayName:BaseService.PointsSetting.SignMostPoint"),
                localizableStr.Invoke("Description:BaseService.PointsSetting.SignMostPoint"),
                true
                ),
            new SettingDefinition(
                BaseServiceSettings.PointsSetting.SignRandomMin,
                string.Empty,
                localizableStr.Invoke("DisplayName:BaseService.PointsSetting.SignRandomMin"),
                localizableStr.Invoke("Description:BaseService.PointsSetting.SignRandomMin"),
                true
                ),
            new SettingDefinition(
                BaseServiceSettings.PointsSetting.SignRandomMax,
                string.Empty,
                localizableStr.Invoke("DisplayName:BaseService.PointsSetting.SignRandomMax"),
                localizableStr.Invoke("Description:BaseService.PointsSetting.SignRandomMax"),
                true
                ),

            /* 提现设置 */
            new SettingDefinition(
                BaseServiceSettings.CashSetting.TocashMoneyLow,
                string.Empty,
                localizableStr.Invoke("DisplayName:BaseService.CashSetting.TocashMoneyLow"),
                localizableStr.Invoke("Description:BaseService.CashSetting.TocashMoneyLow"),
                true
                ),
            new SettingDefinition(
                BaseServiceSettings.CashSetting.TocashMoneyRate,
                string.Empty,
                localizableStr.Invoke("DisplayName:BaseService.CashSetting.TocashMoneyRate"),
                localizableStr.Invoke("Description:BaseService.CashSetting.TocashMoneyRate"),
                true
                ),
            new SettingDefinition(
                BaseServiceSettings.CashSetting.TocashMoneyLimit,
                string.Empty,
                localizableStr.Invoke("DisplayName:BaseService.CashSetting.TocashMoneyLimit"),
                localizableStr.Invoke("Description:BaseService.CashSetting.TocashMoneyLimit"),
                true
                ),

            /* 邀请好友设置 */
            new SettingDefinition(
                BaseServiceSettings.InviteFriendSetting.CommissionType,
                string.Empty,
                localizableStr.Invoke("DisplayName:BaseService.InviteFriendSetting.CommissionType"),
                localizableStr.Invoke("Description:BaseService.InviteFriendSetting.CommissionType"),
                true
                ),
            new SettingDefinition(
                BaseServiceSettings.InviteFriendSetting.CommissionFirst,
                string.Empty,
                localizableStr.Invoke("DisplayName:BaseService.InviteFriendSetting.CommissionFirst"),
                localizableStr.Invoke("Description:BaseService.InviteFriendSetting.CommissionFirst"),
                true
                ),
            new SettingDefinition(
                BaseServiceSettings.InviteFriendSetting.CommissionSecond,
                string.Empty,
                localizableStr.Invoke("DisplayName:BaseService.InviteFriendSetting.CommissionSecond"),
                localizableStr.Invoke("Description:BaseService.InviteFriendSetting.CommissionSecond"),
                true
                ),
            new SettingDefinition(
                BaseServiceSettings.InviteFriendSetting.CommissionThird,
                string.Empty,
                localizableStr.Invoke("DisplayName:BaseService.InviteFriendSetting.CommissionThird"),
                localizableStr.Invoke("Description:BaseService.InviteFriendSetting.CommissionThird"),
                true
                ),

            /* 附件设置
            new SettingDefinition(
                BaseServiceSettings.FileStorageSetting.StorageType,
                "LocalStorage",
                localizableStr.Invoke("DisplayName:BaseService.FileStorageSetting.StorageType"),
                localizableStr.Invoke("Description:BaseService.FileStorageSetting.StorageType"),
                false
                ),
            new SettingDefinition(
                BaseServiceSettings.FileStorageSetting.StoragePath,
                "/upload/",
                localizableStr.Invoke("DisplayName:BaseService.FileStorageSetting.StoragePath"),
                localizableStr.Invoke("Description:BaseService.FileStorageSetting.StoragePath"),
                false
                ),
            new SettingDefinition(
                BaseServiceSettings.FileStorageSetting.StorageSuffix,
                "gif,jpg,jpeg,png,bmp,xls,xlsx,doc,pdf,mp4,WebM,Ogv",
                localizableStr.Invoke("DisplayName:BaseService.FileStorageSetting.StorageSuffix"),
                localizableStr.Invoke("Description:BaseService.FileStorageSetting.StorageSuffix"),
                false
                ),
            new SettingDefinition(
                BaseServiceSettings.FileStorageSetting.StorageMaxSize,
                "20",
                localizableStr.Invoke("DisplayName:BaseService.FileStorageSetting.StorageMaxSize"),
                localizableStr.Invoke("Description:BaseService.FileStorageSetting.StorageMaxSize"),
                false
                ),
            new SettingDefinition(
                BaseServiceSettings.FileStorageSetting.StorageServerJson,
                string.Empty,
                localizableStr.Invoke("DisplayName:BaseService.FileStorageSetting.StorageServerJson"),
                localizableStr.Invoke("Description:BaseService.FileStorageSetting.StorageServerJson"),
                false
                ),
            */

            /* 其它设置 */
            new SettingDefinition(
                BaseServiceSettings.OtherSetting.EntId,
                string.Empty,
                localizableStr.Invoke("DisplayName:BaseService.FileStorageSetting.EntId"),
                localizableStr.Invoke("Description:BaseService.FileStorageSetting.EntId"),
                false
                ),
            new SettingDefinition(
                BaseServiceSettings.OtherSetting.QqMapKey,
                string.Empty,
                localizableStr.Invoke("DisplayName:BaseService.FileStorageSetting.QqMapKey"),
                localizableStr.Invoke("Description:BaseService.FileStorageSetting.QqMapKey"),
                false
                ),
            new SettingDefinition(
                BaseServiceSettings.OtherSetting.ShowApiAppId,
                string.Empty,
                localizableStr.Invoke("DisplayName:BaseService.FileStorageSetting.ShowApiAppId"),
                localizableStr.Invoke("Description:BaseService.FileStorageSetting.ShowApiAppId"),
                false
                ),
            new SettingDefinition(
                BaseServiceSettings.OtherSetting.ShowApiSecret,
                string.Empty,
                localizableStr.Invoke("DisplayName:BaseService.FileStorageSetting.ShowApiSecret"),
                localizableStr.Invoke("Description:BaseService.FileStorageSetting.ShowApiSecret"),
                false
                ),
            new SettingDefinition(
                BaseServiceSettings.OtherSetting.Kuaidi100Customer,
                string.Empty,
                localizableStr.Invoke("DisplayName:BaseService.FileStorageSetting.Kuaidi100Customer"),
                localizableStr.Invoke("Description:BaseService.FileStorageSetting.Kuaidi100Customer"),
                false
                ),
            new SettingDefinition(
                BaseServiceSettings.OtherSetting.Kuaidi100Key,
                string.Empty,
                localizableStr.Invoke("DisplayName:BaseService.FileStorageSetting.Kuaidi100Key"),
                localizableStr.Invoke("Description:BaseService.FileStorageSetting.Kuaidi100Key"),
                false
                ),
            new SettingDefinition(
                BaseServiceSettings.OtherSetting.StatisticsCode,
                string.Empty,
                localizableStr.Invoke("DisplayName:BaseService.FileStorageSetting.StatisticsCode"),
                localizableStr.Invoke("Description:BaseService.FileStorageSetting.StatisticsCode"),
                false
                ));
        }
    }
}