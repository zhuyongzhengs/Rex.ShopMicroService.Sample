using Microsoft.EntityFrameworkCore;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Rex.GoodService.Areas
{
    /// <summary>
    /// 区域表数据库上下文创建扩展
    /// </summary>
    public static class AreaDbContextModelCreatingExtensions
    {
        public static void ConfigureAreaManagement(this ModelBuilder builder)
        {
            Check.NotNull(builder, nameof(builder));

            // 区域表
            builder.Entity<Area>(b =>
            {
                b.ToTable(GoodServiceConsts.DefaultDbTablePrefix + "Areas", GoodServiceConsts.DefaultDbSchema);
                b.HasKey(e => e.Id);
                b.Property(e => e.Id).UseMySqlIdentityColumn();

                b.ConfigureByConvention();
                b.ApplyObjectExtensionMappings();
            });
        }
    }
}