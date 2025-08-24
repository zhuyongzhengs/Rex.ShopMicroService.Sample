using Microsoft.EntityFrameworkCore;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Rex.OrderService.Ships
{
    /// <summary>
    /// 配送方式数据库上下文创建扩展
    /// </summary>
    public static class ShipDbContextModelCreatingExtensions
    {
        public static void ConfigureShipManagement(this ModelBuilder builder)
        {
            Check.NotNull(builder, nameof(builder));

            // 配送方式
            builder.Entity<Ship>(b =>
            {
                b.ToTable(OrderServiceConsts.DefaultDbTablePrefix + "Ships", OrderServiceConsts.DefaultDbSchema);
                b.ConfigureByConvention();
                b.ApplyObjectExtensionMappings();
            });
        }
    }
}