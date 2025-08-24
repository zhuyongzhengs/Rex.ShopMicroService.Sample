using Microsoft.EntityFrameworkCore;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Rex.PromotionService.Promotions
{
    /// <summary>
    /// 促销数据库上下文创建扩展
    /// </summary>
    public static class PromotionDbContextModelCreatingExtensions
    {
        public static void ConfigurePromotionManagement(this ModelBuilder builder)
        {
            Check.NotNull(builder, nameof(builder));

            // 促销
            builder.Entity<Promotion>(b =>
            {
                b.ToTable(PromotionServiceConsts.DefaultDbTablePrefix + "Promotions", PromotionServiceConsts.DefaultDbSchema);
                b.ConfigureByConvention();
                b.ApplyObjectExtensionMappings();
            });

            // 促销条件
            builder.Entity<PromotionCondition>(b =>
            {
                b.ToTable(PromotionServiceConsts.DefaultDbTablePrefix + "PromotionConditions", PromotionServiceConsts.DefaultDbSchema);
                b.ConfigureByConvention();
                b.ApplyObjectExtensionMappings();
            });

            // 促销活动记录
            builder.Entity<PromotionRecord>(b =>
            {
                b.ToTable(PromotionServiceConsts.DefaultDbTablePrefix + "PromotionRecords", PromotionServiceConsts.DefaultDbSchema);
                b.ConfigureByConvention();
                b.ApplyObjectExtensionMappings();
            });

            // 促销结果
            builder.Entity<PromotionResult>(b =>
            {
                b.ToTable(PromotionServiceConsts.DefaultDbTablePrefix + "PromotionResults", PromotionServiceConsts.DefaultDbSchema);
                b.ConfigureByConvention();
                b.ApplyObjectExtensionMappings();
            });

            // 优惠劵
            builder.Entity<Coupon>(b =>
            {
                b.ToTable(PromotionServiceConsts.DefaultDbTablePrefix + "Coupons", PromotionServiceConsts.DefaultDbSchema);
                b.HasIndex(bn => bn.CouponCode).IsUnique(); // 唯一(不允许重复)
                b.ConfigureByConvention();
                b.ApplyObjectExtensionMappings();
            });
        }
    }
}