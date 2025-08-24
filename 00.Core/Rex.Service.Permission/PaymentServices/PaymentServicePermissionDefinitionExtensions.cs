using System;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;
using static Rex.Service.Permission.PaymentServices.PaymentServicePermissions;

namespace Rex.Service.Permission.PaymentServices
{
    /// <summary>
    /// 支付权限定义
    /// </summary>
    public static class PaymentServicePermissionDefinitionExtensions
    {
        /// <summary>
        /// 添加支付方式权限
        /// </summary>
        /// <param name="paymentGroup">组名称</param>
        public static void AddPaymentPermissionDefine(this IPermissionDefinitionContext context, PermissionGroupDefinition paymentGroup, Func<string, LocalizableString> localizableStr = null)
        {
            var permissionDefinition = paymentGroup.AddPermission(PaymentServicePermissions.Payments.Default, localizableStr != null ? localizableStr.Invoke("Payment") : null);
        }

        /// <summary>
        /// 添加支付单据权限
        /// </summary>
        /// <param name="paymentGroup">组名称</param>
        public static void AddBillPaymentPermissionDefine(this IPermissionDefinitionContext context, PermissionGroupDefinition paymentGroup, Func<string, LocalizableString> localizableStr = null)
        {
            var permissionDefinition = paymentGroup.AddPermission(PaymentServicePermissions.BillPayments.Default, localizableStr != null ? localizableStr.Invoke("BillPayment") : null);
        }

        /// <summary>
        /// 添加退款单据权限
        /// </summary>
        /// <param name="paymentGroup">组名称</param>
        public static void AddBillRefundPermissionDefine(this IPermissionDefinitionContext context, PermissionGroupDefinition paymentGroup, Func<string, LocalizableString> localizableStr = null)
        {
            var permissionDefinition = paymentGroup.AddPermission(PaymentServicePermissions.BillRefunds.Default, localizableStr != null ? localizableStr.Invoke("BillRefund") : null);
            permissionDefinition.AddChild(PaymentServicePermissions.BillRefunds.AuditRefund, localizableStr != null ? localizableStr.Invoke("AuditRefund") : null);
        }
    }
}