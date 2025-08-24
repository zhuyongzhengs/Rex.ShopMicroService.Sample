using Microsoft.EntityFrameworkCore;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Rex.PromotionService.PinTuans
{
    /// <summary>
    /// 拼团数据库上下文创建扩展
    /// </summary>
    public static class PinTuanDbContextModelCreatingExtensions
    {
        public static void ConfigurePinTuanManagement(this ModelBuilder builder)
        {
            Check.NotNull(builder, nameof(builder));

            // 拼团规则
            builder.Entity<PinTuanRule>(b =>
            {
                b.ToTable(PromotionServiceConsts.DefaultDbTablePrefix + "PinTuanRules", PromotionServiceConsts.DefaultDbSchema);
                b.ConfigureByConvention();
                b.ApplyObjectExtensionMappings();
            });

            // 拼团商品
            builder.Entity<PinTuanGood>(b =>
            {
                b.ToTable(PromotionServiceConsts.DefaultDbTablePrefix + "PinTuanGoods", PromotionServiceConsts.DefaultDbSchema);
                b.ConfigureByConvention();
                b.ApplyObjectExtensionMappings();
            });

            // 拼团记录
            builder.Entity<PinTuanRecord>(b =>
            {
                b.ToTable(PromotionServiceConsts.DefaultDbTablePrefix + "PinTuanRecords", PromotionServiceConsts.DefaultDbSchema);
                b.ConfigureByConvention();
                b.ApplyObjectExtensionMappings();
            });
        }
    }
}