using Microsoft.EntityFrameworkCore;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Rex.BaseService.Systems.UserWeChats
{
    /// <summary>
    /// 微信用户数据库上下文创建扩展
    /// </summary>
    public static class UserWeChatDbContextModelCreatingExtensions
    {
        public static void ConfigureUserWeChatManagement(this ModelBuilder builder)
        {
            Check.NotNull(builder, nameof(builder));

            // 微信用户
            builder.Entity<UserWeChat>(b =>
            {
                b.ToTable(BaseServiceConsts.DefaultDbTablePrefix + "UserWeChat", BaseServiceConsts.DefaultDbSchema);
                b.ConfigureByConvention();
                b.ApplyObjectExtensionMappings();
            });
        }
    }
}