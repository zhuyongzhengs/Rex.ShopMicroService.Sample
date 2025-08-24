using Microsoft.EntityFrameworkCore;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Rex.GoodService.Stores
{
    /// <summary>
    /// 门店数据库上下文创建扩展
    /// </summary>
    public static class StoreDbContextModelCreatingExtensions
    {
        public static void ConfigureStoreManagement(this ModelBuilder builder)
        {
            Check.NotNull(builder, nameof(builder));

            // 门店
            builder.Entity<Store>(b =>
            {
                b.ToTable(GoodServiceConsts.DefaultDbTablePrefix + "Stores", GoodServiceConsts.DefaultDbSchema);
                b.ConfigureByConvention();
                b.ApplyObjectExtensionMappings();
            });
        }
    }
}