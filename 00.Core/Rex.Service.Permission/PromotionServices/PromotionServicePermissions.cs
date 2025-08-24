namespace Rex.Service.Permission.PromotionServices
{
    /// <summary>
    /// 促销服务权限
    /// </summary>
    public static class PromotionServicePermissions
    {
        /// <summary>
        /// 分组(服务)名称
        /// </summary>
        public const string GroupName = "PromotionService";

        #region (全局)促销

        public static class Promotions
        {
            /// <summary>
            /// 默认权限
            /// </summary>
            public const string Default = GroupName + ".Promotions";

            /// <summary>
            /// 创建权限
            /// </summary>
            public const string Create = Default + ".Create";

            /// <summary>
            /// 修改权限
            /// </summary>
            public const string Update = Default + ".Update";

            /// <summary>
            /// 删除权限
            /// </summary>
            public const string Delete = Default + ".Delete";
        }

        #endregion (全局)促销

        #region 优惠劵

        public static class Coupons
        {
            /// <summary>
            /// 默认权限
            /// </summary>
            public const string Default = GroupName + ".Coupons";

            /// <summary>
            /// 券码列表
            /// </summary>
            public const string Couponlist = Default + ".Couponlist";

            /// <summary>
            /// 创建权限
            /// </summary>
            public const string Create = Default + ".Create";

            /// <summary>
            /// 修改权限
            /// </summary>
            public const string Update = Default + ".Update";

            /// <summary>
            /// 删除权限
            /// </summary>
            public const string Delete = Default + ".Delete";
        }

        #endregion 优惠劵

        #region 团购秒杀

        public static class GroupSeckills
        {
            /// <summary>
            /// 默认权限
            /// </summary>
            public const string Default = GroupName + ".GroupSeckills";

            /// <summary>
            /// 创建权限
            /// </summary>
            public const string Create = Default + ".Create";

            /// <summary>
            /// 修改权限
            /// </summary>
            public const string Update = Default + ".Update";

            /// <summary>
            /// 删除权限
            /// </summary>
            public const string Delete = Default + ".Delete";
        }

        #endregion 团购秒杀

        #region 拼团规则

        public static class PinTuanRules
        {
            /// <summary>
            /// 默认权限
            /// </summary>
            public const string Default = GroupName + ".PinTuanRules";

            /// <summary>
            /// 创建权限
            /// </summary>
            public const string Create = Default + ".Create";

            /// <summary>
            /// 修改权限
            /// </summary>
            public const string Update = Default + ".Update";

            /// <summary>
            /// 删除权限
            /// </summary>
            public const string Delete = Default + ".Delete";
        }

        #endregion 拼团规则

        #region 拼团记录

        public static class PinTuanRecords
        {
            /// <summary>
            /// 默认权限
            /// </summary>
            public const string Default = GroupName + ".PinTuanRecords";
        }

        #endregion 拼团记录
    }
}