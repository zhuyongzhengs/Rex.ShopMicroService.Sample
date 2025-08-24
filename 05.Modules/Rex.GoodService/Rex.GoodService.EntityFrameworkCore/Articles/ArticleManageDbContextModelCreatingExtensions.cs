using Microsoft.EntityFrameworkCore;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Rex.GoodService.Articles
{
    /// <summary>
    /// 文章数据库上下文创建扩展
    /// </summary>
    public static class ArticleManageDbContextModelCreatingExtensions
    {
        public static void ConfigureArticleManagement(this ModelBuilder builder)
        {
            Check.NotNull(builder, nameof(builder));

            // 文章
            builder.Entity<Article>(b =>
            {
                b.ToTable(GoodServiceConsts.DefaultDbTablePrefix + "Articles", GoodServiceConsts.DefaultDbSchema);
                b.ConfigureByConvention();
                b.ApplyObjectExtensionMappings();
            });

            // 文章分类
            builder.Entity<ArticleType>(b =>
            {
                b.ToTable(GoodServiceConsts.DefaultDbTablePrefix + "ArticleTypes", GoodServiceConsts.DefaultDbSchema);
                b.ConfigureByConvention();
                b.ApplyObjectExtensionMappings();
            });
        }
    }
}