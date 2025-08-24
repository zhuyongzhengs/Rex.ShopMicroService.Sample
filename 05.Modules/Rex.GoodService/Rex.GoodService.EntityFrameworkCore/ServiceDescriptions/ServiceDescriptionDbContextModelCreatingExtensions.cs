using Microsoft.EntityFrameworkCore;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Rex.GoodService.ServiceDescriptions
{
    /// <summary>
    /// 商城服务描述数据库上下文创建扩展
    /// </summary>
    public static class ServiceDescriptionDbContextModelCreatingExtensions
    {
        public static void ConfigureServiceDescriptionManagement(this ModelBuilder builder)
        {
            Check.NotNull(builder, nameof(builder));

            // 商城服务描述
            builder.Entity<ServiceDescription>(b =>
            {
                b.ToTable(GoodServiceConsts.DefaultDbTablePrefix + "ServiceDescriptions", GoodServiceConsts.DefaultDbSchema);
                b.ConfigureByConvention();
                b.ApplyObjectExtensionMappings();
            });
        }
    }
}