using Microsoft.EntityFrameworkCore;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Rex.BaseService.UserShips
{
    /// <summary>
    /// 用户(收货)地址数据库上下文创建扩展
    /// </summary>
    public static class UserShipDbContextModelCreatingExtensions
    {
        public static void ConfigureUserShipManagement(this ModelBuilder builder)
        {
            Check.NotNull(builder, nameof(builder));

            // 用户(收货)地址
            builder.Entity<UserShip>(b =>
            {
                b.ToTable(BaseServiceConsts.DefaultDbTablePrefix + "UserShips", BaseServiceConsts.DefaultDbSchema);
                b.ConfigureByConvention();
                b.ApplyObjectExtensionMappings();
            });
        }
    }
}