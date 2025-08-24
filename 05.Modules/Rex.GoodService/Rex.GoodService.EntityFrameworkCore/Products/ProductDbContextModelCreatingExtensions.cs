using Microsoft.EntityFrameworkCore;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Rex.GoodService.Products
{
    /// <summary>
    /// 货品数据库上下文创建扩展
    /// </summary>
    public static class ProductDbContextModelCreatingExtensions
    {
        public static void ConfigureProductManagement(this ModelBuilder builder)
        {
            Check.NotNull(builder, nameof(builder));

            // 货品
            builder.Entity<Product>(b =>
            {
                b.ToTable(GoodServiceConsts.DefaultDbTablePrefix + "Products", GoodServiceConsts.DefaultDbSchema);
                b.HasIndex(p => p.Sn).IsUnique();
                b.ConfigureByConvention();
                b.ApplyObjectExtensionMappings();
            });

            // 货品三级佣金
            builder.Entity<ProductDistribution>(b =>
            {
                b.ToTable(GoodServiceConsts.DefaultDbTablePrefix + "ProductDistributions", GoodServiceConsts.DefaultDbSchema);
                b.ConfigureByConvention();
                b.ApplyObjectExtensionMappings();
            });
        }
    }
}