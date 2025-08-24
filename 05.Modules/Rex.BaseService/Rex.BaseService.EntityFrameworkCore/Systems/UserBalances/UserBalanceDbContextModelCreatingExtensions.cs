using Microsoft.EntityFrameworkCore;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Rex.BaseService.Systems.UserBalances
{
    /// <summary>
    /// 用户余额数据库上下文创建扩展
    /// </summary>
    public static class UserBalanceDbContextModelCreatingExtensions
    {
        public static void ConfigureUserBalanceManagement(this ModelBuilder builder)
        {
            Check.NotNull(builder, nameof(builder));

            // 用户余额
            builder.Entity<UserBalance>(b =>
            {
                b.ToTable(BaseServiceConsts.DefaultDbTablePrefix + "UserBalances", BaseServiceConsts.DefaultDbSchema);
                b.ConfigureByConvention();
                b.ApplyObjectExtensionMappings();
            });
        }
    }
}