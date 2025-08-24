using Rex.Service.Core.Models;
using System.Collections.Generic;

namespace Rex.Service.Core.Configurations
{
    /// <summary>
    /// 全局基础配置字典类型
    /// </summary>
    public class SystemSettingDictionary
    {
        /// <summary>
        /// 获取促销添加参数类型字典
        /// </summary>
        /// <returns></returns>
        public static List<CommonKeyValue> GetPromotionConditionType()
        {
            var list = new List<CommonKeyValue>
            {
                new CommonKeyValue() { Description = "所有商品满足条件", Value = "Goods", Key = "GOODS_ALL" },
                new CommonKeyValue() { Description = "指定某些商品满足条件", Value = "Goods", Key = "GOODS_IDS" },
                new CommonKeyValue() { Description = "指定商品分类满足条件", Value = "Goods", Key = "GOODS_CATS" },
                new CommonKeyValue() { Description = "指定商品品牌满足条件", Value = "Goods", Key = "GOODS_BRANDS" },
                new CommonKeyValue() { Description = "订单满××金额满足条件", Value = "Order", Key = "ORDER_FULL" },
                new CommonKeyValue() { Description = "用户符合指定等级", Value = "User", Key = "USER_GRADE" }
            };
            return list;
        }

        /// <summary>
        /// 获取促销添加结果类型字典
        /// </summary>
        /// <returns></returns>
        public static List<CommonKeyValue> GetPromotionResultType()
        {
            var list = new List<CommonKeyValue>
            {
                new CommonKeyValue() { Description = "指定商品减固定金额", Value = "Goods", Key = "GOODS_REDUCE" },
                new CommonKeyValue() { Description = "指定商品打X折", Value = "Goods", Key = "GOODS_DISCOUNT" },
                new CommonKeyValue() { Description = "指定商品一口价", Value = "Goods", Key = "GOODS_ONE_PRICE" },
                new CommonKeyValue() { Description = "订单减指定金额", Value = "Order", Key = "ORDER_REDUCE" },
                new CommonKeyValue() { Description = "订单打X折", Value = "Order", Key = "ORDER_DISCOUNT" },
                new CommonKeyValue() { Description = "指定商品每第几件减指定金额", Value = "Goods", Key = "GOODS_HALF_PRICE" }
            };
            return list;
        }
    }
}