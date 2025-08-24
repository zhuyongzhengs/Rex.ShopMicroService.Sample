namespace Rex.Service.Permission.OrderServices
{
    /// <summary>
    /// 订单服务权限
    /// </summary>
    public static class OrderServicePermissions
    {
        /// <summary>
        /// 分组(服务)名称
        /// </summary>
        public const string GroupName = "OrderService";

        #region 配送方式

        public static class Ships
        {
            /// <summary>
            /// 默认权限
            /// </summary>
            public const string Default = GroupName + ".Ships";

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

        #endregion 配送方式

        #region 物流

        public static class Logisticss
        {
            /// <summary>
            /// 默认权限
            /// </summary>
            public const string Default = GroupName + ".Logisticss";

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

        #endregion 物流

        #region 订单

        public static class Orders
        {
            /// <summary>
            /// 默认权限
            /// </summary>
            public const string Default = GroupName + ".Orders";

            /// <summary>
            /// 修改权限
            /// </summary>
            public const string Update = Default + ".Update";

            /// <summary>
            /// 删除权限
            /// </summary>
            public const string Delete = Default + ".Delete";

            /// <summary>
            /// 取消权限
            /// </summary>
            public const string Cancel = Default + ".Cancel";

            /// <summary>
            /// 支付权限
            /// </summary>
            public const string Pay = Default + ".Pay";

            /// <summary>
            /// 发货权限
            /// </summary>
            public const string Delivery = Default + ".Delivery";

            /// <summary>
            /// 打印权限
            /// </summary>
            public const string Print = Default + ".Print";

            /// <summary>
            /// 完成权限
            /// </summary>
            public const string Complete = Default + ".Complete";
        }

        #endregion 订单

        #region 发货单

        public static class BillDeliverys
        {
            /// <summary>
            /// 默认权限
            /// </summary>
            public const string Default = GroupName + ".BillDeliverys";

            /// <summary>
            /// 修改权限
            /// </summary>
            public const string Update = Default + ".Update";
        }

        #endregion 发货单

        #region 售后单

        public static class BillAftersales
        {
            /// <summary>
            /// 默认权限
            /// </summary>
            public const string Default = GroupName + ".BillAftersales";

            /// <summary>
            /// 操作权限
            /// </summary>
            public const string Operate = Default + ".Operate";
        }

        #endregion 售后单

        #region 退货单

        public static class BillReships
        {
            /// <summary>
            /// 默认权限
            /// </summary>
            public const string Default = GroupName + ".BillReships";

            /// <summary>
            /// 确认收货权限
            /// </summary>
            public const string ConfirmDelivery = Default + ".ConfirmDelivery";
        }

        #endregion 退货单
    }
}