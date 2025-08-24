using Microsoft.EntityFrameworkCore;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Rex.GoodService.PageDesigns
{
    /// <summary>
    /// 页面设计数据库上下文创建扩展
    /// </summary>
    public static class PageDesignDbContextModelCreatingExtensions
    {
        public static void ConfigurePageDesignManagement(this ModelBuilder builder)
        {
            Check.NotNull(builder, nameof(builder));

            // 页面设计
            builder.Entity<PageDesign>(b =>
            {
                b.ToTable(GoodServiceConsts.DefaultDbTablePrefix + "PageDesigns", GoodServiceConsts.DefaultDbSchema);
                b.HasIndex(b => b.Code).IsUnique(); // 唯一(不允许重复)
                b.ConfigureByConvention();
                b.ApplyObjectExtensionMappings();
            });

            // 页面设计项
            builder.Entity<PageDesignItem>(b =>
            {
                b.ToTable(GoodServiceConsts.DefaultDbTablePrefix + "PageDesignItems", GoodServiceConsts.DefaultDbSchema);
                b.ConfigureByConvention();
                b.ApplyObjectExtensionMappings();
            });
        }
    }
}