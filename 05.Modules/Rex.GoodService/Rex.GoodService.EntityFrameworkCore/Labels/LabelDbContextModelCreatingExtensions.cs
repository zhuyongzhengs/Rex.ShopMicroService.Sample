using Microsoft.EntityFrameworkCore;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Rex.GoodService.Labels
{
    /// <summary>
    /// 标签数据库上下文创建扩展
    /// </summary>
    public static class LabelDbContextModelCreatingExtensions
    {
        public static void ConfigureLabelManagement(this ModelBuilder builder)
        {
            Check.NotNull(builder, nameof(builder));

            // 标签
            builder.Entity<Label>(b =>
            {
                b.ToTable(GoodServiceConsts.DefaultDbTablePrefix + "Labels", GoodServiceConsts.DefaultDbSchema);
                b.ConfigureByConvention();
                b.ApplyObjectExtensionMappings();
            });
        }
    }
}