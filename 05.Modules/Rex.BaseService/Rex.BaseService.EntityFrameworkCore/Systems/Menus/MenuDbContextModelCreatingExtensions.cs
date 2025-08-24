using Microsoft.EntityFrameworkCore;
using Rex.BaseService.Systems.RoleMenus;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Rex.BaseService.Systems.Menus
{
    /// <summary>
    /// 菜单数据库上下文创建扩展
    /// </summary>
    public static class MenuDbContextModelCreatingExtensions
    {
        public static void ConfigureMenuManagement(this ModelBuilder builder)
        {
            Check.NotNull(builder, nameof(builder));

            // 菜单
            builder.Entity<Menu>(b =>
            {
                b.ToTable(BaseServiceConsts.DefaultDbTablePrefix + "Menus", BaseServiceConsts.DefaultDbSchema);
                b.ConfigureByConvention();
                b.ApplyObjectExtensionMappings();
            });

            // 角色菜单
            builder.Entity<RoleMenu>(b =>
            {
                b.ToTable(BaseServiceConsts.DefaultDbTablePrefix + "RoleMenus", BaseServiceConsts.DefaultDbSchema);
                b.ConfigureByConvention();
                b.ApplyObjectExtensionMappings();
            });
        }
    }
}