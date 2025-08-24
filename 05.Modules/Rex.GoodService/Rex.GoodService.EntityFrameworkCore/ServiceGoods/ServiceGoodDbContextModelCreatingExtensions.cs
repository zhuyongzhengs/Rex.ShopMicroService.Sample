using Microsoft.EntityFrameworkCore;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Rex.GoodService.ServiceGoods
{
    /// <summary>
    /// 服务商品数据库上下文创建扩展
    /// </summary>
    public static class ServiceGoodDbContextModelCreatingExtensions
    {
        public static void ConfigureServiceGoodManagement(this ModelBuilder builder)
        {
            Check.NotNull(builder, nameof(builder));

            // 服务商品
            builder.Entity<ServiceGood>(b =>
            {
                b.ToTable(GoodServiceConsts.DefaultDbTablePrefix + "ServiceGoods", GoodServiceConsts.DefaultDbSchema);
                b.ConfigureByConvention();
                b.ApplyObjectExtensionMappings();
            });
        }
    }
}