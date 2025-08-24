using Microsoft.EntityFrameworkCore;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Rex.GoodService.Forms
{
    /// <summary>
    /// 表单表数据库上下文创建扩展
    /// </summary>
    public static class FormDbContextModelCreatingExtensions
    {
        public static void ConfigureFormManagement(this ModelBuilder builder)
        {
            Check.NotNull(builder, nameof(builder));

            // 表单
            builder.Entity<Form>(b =>
            {
                b.ToTable(GoodServiceConsts.DefaultDbTablePrefix + "Forms", GoodServiceConsts.DefaultDbSchema);
                b.ConfigureByConvention();
                b.ApplyObjectExtensionMappings();
            });

            // 表单项目
            builder.Entity<FormItem>(b =>
            {
                b.ToTable(GoodServiceConsts.DefaultDbTablePrefix + "FormItems", GoodServiceConsts.DefaultDbSchema);
                b.HasIndex(p => p.Name).IsUnique();
                b.ConfigureByConvention();
                b.ApplyObjectExtensionMappings();
            });
        }
    }
}