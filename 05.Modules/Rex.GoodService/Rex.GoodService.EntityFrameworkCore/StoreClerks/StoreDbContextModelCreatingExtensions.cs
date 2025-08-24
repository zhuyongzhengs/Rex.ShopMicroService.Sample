using Microsoft.EntityFrameworkCore;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Rex.GoodService.StoreClerks
{
    /// <summary>
    /// 店铺店员关联表数据库上下文创建扩展
    /// </summary>
    public static class StoreClerkDbContextModelCreatingExtensions
    {
        public static void ConfigureStoreClerkManagement(this ModelBuilder builder)
        {
            Check.NotNull(builder, nameof(builder));

            // 店铺店员关联表
            builder.Entity<StoreClerk>(b =>
            {
                b.ToTable(GoodServiceConsts.DefaultDbTablePrefix + "StoreClerks", GoodServiceConsts.DefaultDbSchema);
                b.ConfigureByConvention();
                b.ApplyObjectExtensionMappings();
            });
        }
    }
}