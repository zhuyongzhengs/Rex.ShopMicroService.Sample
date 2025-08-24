using Microsoft.EntityFrameworkCore;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Rex.GoodService.Brands
{
    /// <summary>
    /// 品牌数据库上下文创建扩展
    /// </summary>
    public static class BrandDbContextModelCreatingExtensions
    {
        public static void ConfigureBrandManagement(this ModelBuilder builder)
        {
            Check.NotNull(builder, nameof(builder));

            // 品牌
            builder.Entity<Brand>(b =>
            {
                b.ToTable(GoodServiceConsts.DefaultDbTablePrefix + "Brands", GoodServiceConsts.DefaultDbSchema);
                b.HasIndex(b => b.Name).IsUnique(); // 唯一(不允许重复)
                b.ConfigureByConvention();
                b.ApplyObjectExtensionMappings();
            });
        }
    }
}