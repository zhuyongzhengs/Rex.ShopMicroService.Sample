using Rex.OrderService.Localization;
using Rex.Service.Permission.OrderServices;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Rex.OrderService.Permissions;

public class OrderServicePermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var orderGroup = context.AddGroup(OrderServicePermissions.GroupName);

        #region 物流定义

        context.AddLogisticsPermissionDefine(orderGroup, val => L(val));

        #endregion 物流定义

        #region 配送方式定义

        context.AddShipPermissionDefine(orderGroup, val => L(val));

        #endregion 配送方式定义

        #region 订单定义

        context.AddOrderPermissionDefine(orderGroup, val => L(val));

        #region 发货单定义

        context.AddBillDeliveryPermissionDefine(orderGroup, val => L(val));

        #endregion 发货单定义

        #region 售后单定义

        context.AddBillAftersalesPermissionDefine(orderGroup, val => L(val));

        #endregion 售后单定义

        #region 退货单定义

        context.AddBillReshipPermissionDefine(orderGroup, val => L(val));

        #endregion 退货单定义

        #endregion 订单定义
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<OrderServiceResource>(name);
    }
}