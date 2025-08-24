using Microsoft.EntityFrameworkCore;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Rex.GoodService.Goods
{
    /// <summary>
    /// 商品数据库上下文创建扩展
    /// </summary>
    public static class GoodDbContextModelCreatingExtensions
    {
        public static void ConfigureGoodManagement(this ModelBuilder builder)
        {
            Check.NotNull(builder, nameof(builder));

            // 商品
            builder.Entity<Good>(b =>
            {
                b.ToTable(GoodServiceConsts.DefaultDbTablePrefix + "Goods", GoodServiceConsts.DefaultDbSchema);
                b.HasIndex(p => p.BarCode).IsUnique();
                b.ConfigureByConvention();
                b.ApplyObjectExtensionMappings();
            });

            // 商品浏览记录
            builder.Entity<GoodBrowsing>(b =>
            {
                b.ToTable(GoodServiceConsts.DefaultDbTablePrefix + "GoodBrowsings", GoodServiceConsts.DefaultDbSchema);
                b.ConfigureByConvention();
                b.ApplyObjectExtensionMappings();
            });

            // 商品分类
            builder.Entity<GoodCategory>(b =>
            {
                b.ToTable(GoodServiceConsts.DefaultDbTablePrefix + "GoodCategorys", GoodServiceConsts.DefaultDbSchema);
                b.ConfigureByConvention();
                b.ApplyObjectExtensionMappings();
            });

            // 商品分类扩展表
            builder.Entity<GoodCategoryExtend>(b =>
            {
                b.ToTable(GoodServiceConsts.DefaultDbTablePrefix + "GoodCategoryExtends", GoodServiceConsts.DefaultDbSchema);
                b.ConfigureByConvention();
                b.ApplyObjectExtensionMappings();
            });

            // 商品收藏
            builder.Entity<GoodCollection>(b =>
            {
                b.ToTable(GoodServiceConsts.DefaultDbTablePrefix + "GoodCollections", GoodServiceConsts.DefaultDbSchema);
                b.ConfigureByConvention();
                b.ApplyObjectExtensionMappings();
            });

            // 商品评价
            builder.Entity<GoodComment>(b =>
            {
                b.ToTable(GoodServiceConsts.DefaultDbTablePrefix + "GoodComments", GoodServiceConsts.DefaultDbSchema);
                b.ConfigureByConvention();
                b.ApplyObjectExtensionMappings();
            });

            // 商品会员价
            builder.Entity<GoodGrade>(b =>
            {
                b.ToTable(GoodServiceConsts.DefaultDbTablePrefix + "GoodGrades", GoodServiceConsts.DefaultDbSchema);
                b.ConfigureByConvention();
                b.ApplyObjectExtensionMappings();
            });

            // 商品图片
            builder.Entity<GoodImage>(b =>
            {
                b.ToTable(GoodServiceConsts.DefaultDbTablePrefix + "GoodImages", GoodServiceConsts.DefaultDbSchema);
                b.ConfigureByConvention();
                b.ApplyObjectExtensionMappings();
            });

            // 商品参数
            builder.Entity<GoodParam>(b =>
            {
                b.ToTable(GoodServiceConsts.DefaultDbTablePrefix + "GoodParams", GoodServiceConsts.DefaultDbSchema);
                b.HasIndex(p => p.Name).IsUnique();
                b.ConfigureByConvention();
                b.ApplyObjectExtensionMappings();
            });

            // 商品类型属性
            builder.Entity<GoodTypeSpec>(b =>
            {
                b.ToTable(GoodServiceConsts.DefaultDbTablePrefix + "GoodTypeSpecs", GoodServiceConsts.DefaultDbSchema);
                b.HasIndex(p => p.Name).IsUnique();
                b.ConfigureByConvention();
                b.ApplyObjectExtensionMappings();
            });

            // 商品类型属性值
            builder.Entity<GoodTypeSpecValue>(b =>
            {
                b.ToTable(GoodServiceConsts.DefaultDbTablePrefix + "GoodTypeSpecValues", GoodServiceConsts.DefaultDbSchema);
                b.ConfigureByConvention();
                b.ApplyObjectExtensionMappings();
            });
        }
    }
}