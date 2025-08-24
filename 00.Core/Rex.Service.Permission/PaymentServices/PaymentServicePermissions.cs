namespace Rex.Service.Permission.PaymentServices
{
    public static class PaymentServicePermissions
    {
        public const string GroupName = "PaymentService";

        #region 支付方式

        public static class Payments
        {
            /// <summary>
            /// 默认权限
            /// </summary>
            public const string Default = GroupName + ".Payments";
        }

        #endregion 支付方式

        #region 支付单据

        public static class BillPayments
        {
            /// <summary>
            /// 默认权限
            /// </summary>
            public const string Default = GroupName + ".BillPayments";
        }

        #endregion 支付单据

        #region 退款单据

        public static class BillRefunds
        {
            /// <summary>
            /// 默认权限
            /// </summary>
            public const string Default = GroupName + ".BillRefunds";

            /// <summary>
            /// 审核退款
            /// </summary>
            public const string AuditRefund = Default + ".AuditRefund";
        }

        #endregion 退款单据
    }
}