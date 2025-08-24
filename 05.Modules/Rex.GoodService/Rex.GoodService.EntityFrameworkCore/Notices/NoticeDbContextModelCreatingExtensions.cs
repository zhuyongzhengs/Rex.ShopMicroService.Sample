using Microsoft.EntityFrameworkCore;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Rex.GoodService.Notices
{
    /// <summary>
    /// 公告数据库上下文创建扩展
    /// </summary>
    public static class NoticeDbContextModelCreatingExtensions
    {
        public static void ConfigureNoticeManagement(this ModelBuilder builder)
        {
            Check.NotNull(builder, nameof(builder));

            // 公告
            builder.Entity<Notice>(b =>
            {
                b.ToTable(GoodServiceConsts.DefaultDbTablePrefix + "Notices", GoodServiceConsts.DefaultDbSchema);
                b.ConfigureByConvention();
                b.ApplyObjectExtensionMappings();
            });
        }
    }
}