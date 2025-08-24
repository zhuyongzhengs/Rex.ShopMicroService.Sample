using Microsoft.EntityFrameworkCore;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Rex.BaseService.Systems.UserGrades
{
    /// <summary>
    /// 用户等级数据库上下文创建扩展
    /// </summary>
    public static class UserGradeDbContextModelCreatingExtensions
    {
        public static void ConfigureUserGradeManagement(this ModelBuilder builder)
        {
            Check.NotNull(builder, nameof(builder));

            // 微信用户
            builder.Entity<UserGrade>(b =>
            {
                b.ToTable(BaseServiceConsts.DefaultDbTablePrefix + "UserGrade", BaseServiceConsts.DefaultDbSchema);
                b.HasIndex(ug => ug.Title).IsUnique(); // 唯一(不允许重复)
                b.ConfigureByConvention();
                b.ApplyObjectExtensionMappings();
            });
        }
    }
}