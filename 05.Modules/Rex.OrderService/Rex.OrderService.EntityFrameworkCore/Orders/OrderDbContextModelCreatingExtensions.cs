using Microsoft.EntityFrameworkCore;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Rex.OrderService.Orders
{
    /// <summary>
    /// 订单数据库上下文创建扩展
    /// </summary>
    public static class OrderDbContextModelCreatingExtensions
    {
        public static void ConfigureOrderManagement(this ModelBuilder builder)
        {
            Check.NotNull(builder, nameof(builder));

            // 订单
            builder.Entity<Order>(b =>
            {
                b.ToTable(OrderServiceConsts.DefaultDbTablePrefix + "Orders", OrderServiceConsts.DefaultDbSchema);
                b.HasIndex(bn => bn.No).IsUnique(); // 唯一(不允许重复)
                b.ConfigureByConvention();
                b.ApplyObjectExtensionMappings();
            });

            // 订单明细
            builder.Entity<OrderItem>(b =>
            {
                b.ToTable(OrderServiceConsts.DefaultDbTablePrefix + "OrderItems", OrderServiceConsts.DefaultDbSchema);
                b.ConfigureByConvention();
                b.ApplyObjectExtensionMappings();
            });

            // 订单记录
            builder.Entity<OrderLog>(b =>
            {
                b.ToTable(OrderServiceConsts.DefaultDbTablePrefix + "OrderLogs", OrderServiceConsts.DefaultDbSchema);
                b.ConfigureByConvention();
                b.ApplyObjectExtensionMappings();
            });
        }
    }
}