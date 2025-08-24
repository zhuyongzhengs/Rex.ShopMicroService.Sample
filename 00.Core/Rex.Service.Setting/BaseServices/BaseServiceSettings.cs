namespace Rex.Service.Setting.BaseServices
{
    public static class BaseServiceSettings
    {
        /// <summary>
        /// 前缀
        /// </summary>
        private const string Prefix = "BaseService";

        /// <summary>
        /// 特殊开关
        /// </summary>
        public static class SpecialSwitch
        {
            public const string Default = Prefix + ".SpecialSwitch";

            /// <summary>
            /// 显示门店列表
            /// </summary>
            public const string ShowStore = Default + ".ShowStore";

            /// <summary>
            /// 显示充值功能
            /// </summary>
            public const string ShowCharge = Default + ".ShowCharge";
        }

        /// <summary>
        /// 平台设置
        /// </summary>
        public static class PlatformSetting
        {
            public const string Default = Prefix + ".PlatformSetting";

            /// <summary>
            /// 平台名称
            /// </summary>
            public const string ShopName = Default + ".ShopName";

            /// <summary>
            /// 平台描述
            /// </summary>
            public const string ShopDesc = Default + ".ShopDesc";

            /// <summary>
            /// 备案信息
            /// </summary>
            public const string ShopBeiAn = Default + ".ShopBeiAn";

            /// <summary>
            /// 关于我们
            /// </summary>
            public const string AboutArticle = Default + ".AboutArticle";

            /// <summary>
            /// 用户协议
            /// </summary>
            public const string UserAgreement = Default + ".UserAgreement";

            /// <summary>
            /// 隐私政策
            /// </summary>
            public const string PrivacyPolicy = Default + ".PrivacyPolicy";

            /// <summary>
            /// 平台Logo
            /// </summary>
            public const string PlatformLogo = Default + ".PlatformLogo";

            /// <summary>
            /// 默认图
            /// </summary>
            public const string DefaultPic = Default + ".DefaultPic";

            /// <summary>
            /// 开启门店自提
            /// </summary>
            public const string StoreSwitch = Default + ".StoreSwitch";

            /// <summary>
            /// 发票功能
            /// </summary>
            public const string InvoiceSwitch = Default + ".InvoiceSwitch";

            /// <summary>
            /// 搜索发现关键词
            /// </summary>
            public const string RecommendKeys = Default + ".RecommendKeys";

            /// <summary>
            /// 腾讯地图Key
            /// </summary>
            public const string TMapKey = Default + ".TMapKey";
        }

        /// <summary>
        /// 分享设置
        /// </summary>
        public static class ShareSetting
        {
            public const string Default = Prefix + ".ShareSetting";

            /// <summary>
            /// 分享标题
            /// </summary>
            public const string ShareTitle = Default + ".ShareTitle";

            /// <summary>
            /// 分享描述
            /// </summary>
            public const string ShareDesc = Default + ".ShareDesc";

            /// <summary>
            /// 分享图片
            /// </summary>
            public const string ShareImage = Default + ".ShareImage";
        }

        /// <summary>
        /// 会员设置
        /// </summary>
        public static class UsersSetting
        {
            public const string Default = Prefix + ".UsersSetting";

            /// <summary>
            /// 绑定手机号码
            /// </summary>
            public const string IsBindMobile = Default + ".IsBindMobile";

            /// <summary>
            /// 商家手机号码
            /// </summary>
            public const string ShopMobile = Default + ".ShopMobile";
        }

        /// <summary>
        /// 商品设置
        /// </summary>
        public static class GoodsSetting
        {
            public const string Default = Prefix + ".GoodsSetting";

            /// <summary>
            /// 库存警报数量
            /// </summary>
            public const string GoodsStocksWarn = Default + ".GoodsStocksWarn";
        }

        /// <summary>
        /// 订单设置
        /// </summary>
        public static class OrderSetting
        {
            public const string Default = Prefix + ".OrderSetting";

            /// <summary>
            /// 订单取消时间
            /// </summary>
            public const string OrderCancelTime = Default + ".OrderCancelTime";

            /// <summary>
            /// 订单完成时间
            /// </summary>
            public const string OrderCompleteTime = Default + ".OrderCompleteTime";

            /// <summary>
            /// 订单确认收货时间
            /// </summary>
            public const string OrderAutoSignTime = Default + ".OrderAutoSignTime";

            /// <summary>
            /// 订单自动评价时间
            /// </summary>
            public const string OrderAutoEvalTime = Default + ".OrderAutoEvalTime";

            /// <summary>
            /// 订单提醒付款时间
            /// </summary>
            public const string RemindOrderTime = Default + ".RemindOrderTime";

            /// <summary>
            /// 退货联系人
            /// </summary>
            public const string ReshipName = Default + ".ReshipName";

            /// <summary>
            /// 退货联系方式
            /// </summary>
            public const string ReshipMobile = Default + ".ReshipMobile";

            /// <summary>
            /// 退货区域
            /// </summary>
            public const string ReshipAreaId = Default + ".ReshipAreaId";

            /// <summary>
            /// 退货坐标
            /// </summary>
            public const string ReshipCoordinate = Default + ".ReshipCoordinate";

            /// <summary>
            /// 退货详细地址
            /// </summary>
            public const string ReshipAddress = Default + ".ReshipAddress";

            /// <summary>
            /// 门店自提自动发货
            /// </summary>
            public const string StoreOrderAutomaticDelivery = Default + ".StoreOrderAutomaticDelivery";
        }

        /// <summary>
        /// 积分设置
        /// </summary>
        public static class PointsSetting
        {
            public const string Default = Prefix + ".PointsSetting";

            /// <summary>
            /// 开启积分功能
            /// </summary>
            public const string PointSwitch = Default + ".PointSwitch";

            /// <summary>
            /// 订单积分折现比例
            /// </summary>
            public const string PointDiscountedProportion = Default + ".PointDiscountedProportion";

            /// <summary>
            /// 订单积分使用比例
            /// </summary>
            public const string OrdersPointProportion = Default + ".OrdersPointProportion";

            /// <summary>
            /// 订单积分奖励比例
            /// </summary>
            public const string OrdersRewardProportion = Default + ".OrdersRewardProportion";

            /// <summary>
            /// 签到奖励类型
            /// </summary>
            public const string SignPointType = Default + ".SignPointType";

            /// <summary>
            /// 首次奖励积分
            /// </summary>
            public const string FirstSignPoint = Default + ".FirstSignPoint";

            /// <summary>
            /// 连续签到追加
            /// </summary>
            public const string ContinuitySignAdditional = Default + ".ContinuitySignAdditional";

            /// <summary>
            /// 单日最大奖励
            /// </summary>
            public const string SignMostPoint = Default + ".SignMostPoint";

            /// <summary>
            /// 随机奖励积分最小值
            /// </summary>
            public const string SignRandomMin = Default + ".SignRandomMin";

            /// <summary>
            /// 随机奖励积分最大值
            /// </summary>
            public const string SignRandomMax = Default + ".SignRandomMax";
        }

        /// <summary>
        /// 提现设置
        /// </summary>
        public static class CashSetting
        {
            public const string Default = Prefix + ".CashSetting";

            /// <summary>
            /// 最低提现金额
            /// </summary>
            public const string TocashMoneyLow = Default + ".TocashMoneyLow";

            /// <summary>
            /// 提现服务费率
            /// </summary>
            public const string TocashMoneyRate = Default + ".TocashMoneyRate";

            /// <summary>
            /// 每日提现上限
            /// </summary>
            public const string TocashMoneyLimit = Default + ".TocashMoneyLimit";
        }

        /// <summary>
        /// 邀请好友设置
        /// </summary>
        public static class InviteFriendSetting
        {
            public const string Default = Prefix + ".InviteFriendSetting";

            /// <summary>
            /// 佣金类型
            /// </summary>
            public const string CommissionType = Default + ".CommissionType";

            /// <summary>
            /// 一级佣金
            /// </summary>
            public const string CommissionFirst = Default + ".CommissionFirst";

            /// <summary>
            /// 二级佣金
            /// </summary>
            public const string CommissionSecond = Default + ".CommissionSecond";

            /// <summary>
            /// 三级佣金
            /// </summary>
            public const string CommissionThird = Default + ".CommissionThird";
        }

        /// <summary>
        /// 附件设置
        /// </summary>
        public static class FileStorageSetting
        {
            public const string Default = Prefix + ".FileStorageSetting";

            /// <summary>
            /// 存储方式
            /// </summary>
            public const string StorageType = Default + ".StorageType";

            /// <summary>
            /// 存储路径
            /// </summary>
            public const string StoragePath = Default + ".StoragePath";

            /// <summary>
            /// 文件后缀类型
            /// </summary>
            public const string StorageSuffix = Default + ".StorageSuffix";

            /// <summary>
            /// 文件最大大小
            /// </summary>
            public const string StorageMaxSize = Default + ".StorageMaxSize";

            /// <summary>
            /// 存储服务[Json对象]
            /// </summary>
            public const string StorageServerJson = Default + ".StorageServerJson";
        }

        /// <summary>
        /// 其它设置
        /// </summary>
        public static class OtherSetting
        {
            public const string Default = Prefix + ".OtherSetting";

            /// <summary>
            /// 客服ID
            /// </summary>
            public const string EntId = Default + ".EntId";

            /// <summary>
            /// 腾讯地图Key
            /// </summary>
            public const string QqMapKey = Default + ".QqMapKey";

            /// <summary>
            /// [易源]AppId
            /// </summary>
            public const string ShowApiAppId = Default + ".ShowApiAppId";

            /// <summary>
            /// [易源]授权Secret
            /// </summary>
            public const string ShowApiSecret = Default + ".ShowApiSecret";

            /// <summary>
            /// [快递100]公司编号
            /// </summary>
            public const string Kuaidi100Customer = Default + ".Kuaidi100Customer";

            /// <summary>
            /// [快递100]授权Key
            /// </summary>
            public const string Kuaidi100Key = Default + ".Kuaidi100Key";

            /// <summary>
            /// [统计代码]百度统计代码
            /// </summary>
            public const string StatisticsCode = Default + ".StatisticsCode";
        }
    }
}