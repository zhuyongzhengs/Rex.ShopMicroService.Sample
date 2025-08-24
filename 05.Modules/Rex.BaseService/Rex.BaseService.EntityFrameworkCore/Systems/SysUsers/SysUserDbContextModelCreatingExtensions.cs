using Microsoft.EntityFrameworkCore;
using System.Net;
using System;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;
using Volo.Abp.Identity;
using System.Reflection.Emit;
using System.Diagnostics;
using Rex.BaseService.Systems.UserGrades;

namespace Rex.BaseService.Systems.SysUsers
{
    /// <summary>
    /// 系统用户数据库上下文创建扩展
    /// </summary>
    public static class SysUserDbContextModelCreatingExtensions
    {
        public static void ConfigureSysUserManagement(this ModelBuilder builder)
        {
            Check.NotNull(builder, nameof(builder));

            // 系统用户
            builder.Entity<SysUser>(b =>
            {
                b.HasBaseType<IdentityUser>();
                b.HasDiscriminator<string>("Discriminator").HasValue<SysUser>(nameof(SysUser));

                b.ToTable(BaseServiceConsts.DefaultDbTablePrefix + "Users", BaseServiceConsts.DefaultDbSchema);
                b.ConfigureByConvention();
                b.ApplyObjectExtensionMappings();
            });

            // 用户积分记录
            builder.Entity<UserPointLog>(b =>
            {
                b.ToTable(BaseServiceConsts.DefaultDbTablePrefix + "UserPointLogs", BaseServiceConsts.DefaultDbSchema);
                b.ConfigureByConvention();
                b.ApplyObjectExtensionMappings();
            });
        }
    }
}