using Rex.PaymentService.Localization;
using Rex.Service.Permission.PaymentServices;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Rex.PaymentService.Permissions;

public class PaymentServicePermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var paymentGroup = context.AddGroup(PaymentServicePermissions.GroupName);

        #region 支付方式定义

        context.AddPaymentPermissionDefine(paymentGroup, val => L(val));

        #endregion 支付方式定义

        #region 支付单据定义

        context.AddBillPaymentPermissionDefine(paymentGroup, val => L(val));

        #endregion 支付单据定义

        #region 退款单据定义

        context.AddBillRefundPermissionDefine(paymentGroup, val => L(val));

        #endregion 退款单据定义
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<PaymentServiceResource>(name);
    }
}