using System;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Rex.Service.Permission.OrderServices
{
    /// <summary>
    /// 订单权限定义
    /// </summary>
    public static class OrderServicePermissionDefinitionExtensions
    {
        /// <summary>
        /// 添加物流权限
        /// </summary>
        /// <param name="orderGroup">组名称</param>
        public static void AddLogisticsPermissionDefine(this IPermissionDefinitionContext context, PermissionGroupDefinition orderGroup, Func<string, LocalizableString> localizableStr = null)
        {
            var permissionDefinition = orderGroup.AddPermission(OrderServicePermissions.Logisticss.Default, localizableStr != null ? localizableStr.Invoke("Logistics") : null);
            permissionDefinition.AddChild(OrderServicePermissions.Logisticss.Create, localizableStr != null ? localizableStr.Invoke("Create") : null);
            permissionDefinition.AddChild(OrderServicePermissions.Logisticss.Update, localizableStr != null ? localizableStr.Invoke("Update") : null);
            permissionDefinition.AddChild(OrderServicePermissions.Logisticss.Delete, localizableStr != null ? localizableStr.Invoke("Delete") : null);
        }

        /// <summary>
        /// 添加配送方式权限
        /// </summary>
        /// <param name="orderGroup">组名称</param>
        public static void AddShipPermissionDefine(this IPermissionDefinitionContext context, PermissionGroupDefinition orderGroup, Func<string, LocalizableString> localizableStr = null)
        {
            var permissionDefinition = orderGroup.AddPermission(OrderServicePermissions.Ships.Default, localizableStr != null ? localizableStr.Invoke("Ship") : null);
            permissionDefinition.AddChild(OrderServicePermissions.Ships.Create, localizableStr != null ? localizableStr.Invoke("Create") : null);
            permissionDefinition.AddChild(OrderServicePermissions.Ships.Update, localizableStr != null ? localizableStr.Invoke("Update") : null);
            permissionDefinition.AddChild(OrderServicePermissions.Ships.Delete, localizableStr != null ? localizableStr.Invoke("Delete") : null);
        }

        /// <summary>
        /// 添加订单权限
        /// </summary>
        /// <param name="orderGroup">组名称</param>
        public static void AddOrderPermissionDefine(this IPermissionDefinitionContext context, PermissionGroupDefinition orderGroup, Func<string, LocalizableString> localizableStr = null)
        {
            var permissionDefinition = orderGroup.AddPermission(OrderServicePermissions.Orders.Default, localizableStr != null ? localizableStr.Invoke("Order") : null);
            permissionDefinition.AddChild(OrderServicePermissions.Orders.Update, localizableStr != null ? localizableStr.Invoke("Update") : null);
            permissionDefinition.AddChild(OrderServicePermissions.Orders.Delete, localizableStr != null ? localizableStr.Invoke("Delete") : null);
            permissionDefinition.AddChild(OrderServicePermissions.Orders.Cancel, localizableStr != null ? localizableStr.Invoke("Cancel") : null);
            permissionDefinition.AddChild(OrderServicePermissions.Orders.Pay, localizableStr != null ? localizableStr.Invoke("Pay") : null);
            permissionDefinition.AddChild(OrderServicePermissions.Orders.Delivery, localizableStr != null ? localizableStr.Invoke("Delivery") : null);
            permissionDefinition.AddChild(OrderServicePermissions.Orders.Print, localizableStr != null ? localizableStr.Invoke("Print") : null);
            permissionDefinition.AddChild(OrderServicePermissions.Orders.Complete, localizableStr != null ? localizableStr.Invoke("Complete") : null);
        }

        /// <summary>
        /// 添加发货单权限
        /// </summary>
        /// <param name="orderGroup">组名称</param>
        public static void AddBillDeliveryPermissionDefine(this IPermissionDefinitionContext context, PermissionGroupDefinition orderGroup, Func<string, LocalizableString> localizableStr = null)
        {
            var permissionDefinition = orderGroup.AddPermission(OrderServicePermissions.BillDeliverys.Default, localizableStr != null ? localizableStr.Invoke("BillDelivery") : null);
            permissionDefinition.AddChild(OrderServicePermissions.BillDeliverys.Update, localizableStr != null ? localizableStr.Invoke("Update") : null);
        }

        /// <summary>
        /// 添加售后单权限
        /// </summary>
        /// <param name="orderGroup">组名称</param>
        public static void AddBillAftersalesPermissionDefine(this IPermissionDefinitionContext context, PermissionGroupDefinition orderGroup, Func<string, LocalizableString> localizableStr = null)
        {
            var permissionDefinition = orderGroup.AddPermission(OrderServicePermissions.BillAftersales.Default, localizableStr != null ? localizableStr.Invoke("BillAftersales") : null);
            permissionDefinition.AddChild(OrderServicePermissions.BillAftersales.Operate, localizableStr != null ? localizableStr.Invoke("Operate") : null);
        }

        /// <summary>
        /// 添加退货单权限
        /// </summary>
        /// <param name="orderGroup">组名称</param>
        public static void AddBillReshipPermissionDefine(this IPermissionDefinitionContext context, PermissionGroupDefinition orderGroup, Func<string, LocalizableString> localizableStr = null)
        {
            var permissionDefinition = orderGroup.AddPermission(OrderServicePermissions.BillReships.Default, localizableStr != null ? localizableStr.Invoke("BillReships") : null);
            permissionDefinition.AddChild(OrderServicePermissions.BillReships.ConfirmDelivery, localizableStr != null ? localizableStr.Invoke("ConfirmDelivery") : null);
        }
    }
}