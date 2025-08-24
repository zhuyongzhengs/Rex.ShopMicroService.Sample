using Rex.PromotionService.Localization;
using Rex.Service.Permission.PromotionServices;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Rex.PromotionService.Permissions;

public class PromotionServicePermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var promotionGroup = context.AddGroup(PromotionServicePermissions.GroupName);

        #region 促销定义

        context.AddPromotionPermissionDefine(promotionGroup, val => L(val));

        #endregion 促销定义

        #region 优惠劵定义

        context.AddCouponPermissionDefine(promotionGroup, val => L(val));

        #endregion 优惠劵定义

        #region 团购秒杀定义

        context.AddGroupSeckillPermissionDefine(promotionGroup, val => L(val));

        #endregion 团购秒杀定义

        #region 拼团规则定义

        context.AddPinTuanRulePermissionDefine(promotionGroup, val => L(val));

        #endregion 拼团规则定义

        #region 拼团记录定义

        context.AddPinTuanRecordPermissionDefine(promotionGroup, val => L(val));

        #endregion 拼团记录定义
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<PromotionServiceResource>(name);
    }
}