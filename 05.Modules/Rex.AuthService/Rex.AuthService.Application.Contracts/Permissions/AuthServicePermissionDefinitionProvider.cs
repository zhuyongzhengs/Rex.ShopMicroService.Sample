using Rex.AuthService.Localization;
using Rex.Service.Permission.BaseServices;
using Rex.Service.Permission.GoodServices;
using Rex.Service.Permission.OrderServices;
using Rex.Service.Permission.PaymentServices;
using Rex.Service.Permission.PromotionServices;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Rex.AuthService.Permissions;

public class AuthServicePermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        #region Base服务权限定义

        var baseGroup = context.AddGroup(BaseServicePermissions.GroupName);

        #region 首页定义

        context.AddBaseHomePermissionDefine(baseGroup, val => L(val));

        #endregion 首页定义

        #region 菜单定义

        context.AddBaseMenuPermissionDefine(baseGroup, val => L(val));

        #endregion 菜单定义

        #region 组织单元定义

        context.AddBaseOrganizationUnitDefine(baseGroup, val => L(val));

        #endregion 组织单元定义

        #region 审计日志定义

        context.AddBaseAuditLoggingPermissionDefine(baseGroup, val => L(val));

        #endregion 审计日志定义

        #region 安全日志定义

        context.AddBaseSecurityLogPermissionDefine(baseGroup, val => L(val));

        #endregion 安全日志定义

        #region 注册用户定义

        context.AddBaseSysUserPermissionDefine(baseGroup, val => L(val));

        #endregion 注册用户定义

        #region 微信用户定义

        context.AddBaseUserWeChatPermissionDefine(baseGroup, val => L(val));

        #endregion 微信用户定义

        #region 用户等级定义

        context.AddBaseUserGradePermissionDefine(baseGroup, val => L(val));

        #endregion 用户等级定义

        #region 平台设置定义

        context.AddBasePlatformSettingPermissionDefine(baseGroup, val => L(val));

        #endregion 平台设置定义

        #endregion Base服务权限定义

        #region 商品服务权限定义

        var goodGroup = context.AddGroup(GoodServicePermissions.GroupName);

        #region 品牌定义

        context.AddGoodBrandPermissionDefine(goodGroup, val => L(val));

        #endregion 品牌定义

        #region 商品定义

        context.AddGoodPermissionDefine(goodGroup, val => L(val));

        #endregion 商品定义

        #region 商品分类 定义

        context.AddGoodCategoryPermissionDefine(goodGroup, val => L(val));

        #endregion 商品分类 定义

        #region 商品参数[模型]定义

        context.AddGoodParamPermissionDefine(goodGroup, val => L(val));

        #endregion 商品参数[模型]定义

        #region 商品类型规格定义

        context.AddGoodTypeSpecPermissionDefine(goodGroup, val => L(val));

        #endregion 商品类型规格定义

        #region 商品服务定义

        context.AddServiceGoodPermissionDefine(goodGroup, val => L(val));

        #endregion 商品服务定义

        #region 门店定义

        context.AddStorePermissionDefine(goodGroup, val => L(val));

        #endregion 门店定义

        #region 店铺店员关联定义

        context.AddStoreClerkPermissionDefine(goodGroup, val => L(val));

        #endregion 店铺店员关联定义

        #region 区域定义

        context.AddAreaPermissionDefine(goodGroup, val => L(val));

        #endregion 区域定义

        #region 文章定义

        context.AddArticlePermissionDefine(goodGroup, val => L(val));

        #endregion 文章定义

        #region 文章分类定义

        context.AddArticleTypePermissionDefine(goodGroup, val => L(val));

        #endregion 文章分类定义

        #region 页面设计定义

        context.AddPageDesignPermissionDefine(goodGroup, val => L(val));

        #endregion 页面设计定义

        #region 公告定义

        context.AddNoticePermissionDefine(goodGroup, val => L(val));

        #endregion 公告定义

        #region 自定义表单定义

        context.AddFormPermissionDefine(goodGroup, val => L(val));

        #endregion 自定义表单定义

        #region 商城服务描述定义

        context.AddServiceDescriptionPermissionDefine(goodGroup, val => L(val));

        #endregion 商城服务描述定义

        #region 商品评价定义

        context.AddGoodCommentPermissionDefine(goodGroup, val => L(val));

        #endregion 商品评价定义

        #endregion 商品服务权限定义

        #region 促销服务权限定义

        var promotionGroup = context.AddGroup(PromotionServicePermissions.GroupName);

        #region 拼团规则定义

        context.AddPinTuanRulePermissionDefine(promotionGroup, val => L(val));

        #endregion 拼团规则定义

        #region 拼团记录定义

        context.AddPinTuanRecordPermissionDefine(promotionGroup, val => L(val));

        #endregion 拼团记录定义

        #region 促销定义

        context.AddPromotionPermissionDefine(promotionGroup, val => L(val));

        #endregion 促销定义

        #region 优惠劵定义

        context.AddCouponPermissionDefine(promotionGroup, val => L(val));

        #endregion 优惠劵定义

        #region 团购秒杀定义

        context.AddGroupSeckillPermissionDefine(promotionGroup, val => L(val));

        #endregion 团购秒杀定义

        #endregion 促销服务权限定义

        #region 订单服务权限定义

        var orderGroup = context.AddGroup(OrderServicePermissions.GroupName);

        #region 物流定义

        context.AddLogisticsPermissionDefine(orderGroup, val => L(val));

        #endregion 物流定义

        #region 配送方式定义

        context.AddShipPermissionDefine(orderGroup, val => L(val));

        #endregion 配送方式定义

        #region 订单定义

        context.AddOrderPermissionDefine(orderGroup, val => L(val));

        #endregion 订单定义

        #region 发货单定义

        context.AddBillDeliveryPermissionDefine(orderGroup, val => L(val));

        #endregion 发货单定义

        #region 售后单定义

        context.AddBillAftersalesPermissionDefine(orderGroup, val => L(val));

        #endregion 售后单定义

        #region 退货单定义

        context.AddBillReshipPermissionDefine(orderGroup, val => L(val));

        #endregion 退货单定义

        #endregion 订单服务权限定义

        #region 支付服务权限定义

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

        #endregion 支付服务权限定义
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<AuthServiceResource>(name);
    }
}