using Microsoft.EntityFrameworkCore;
using Rex.OrderService;
using Rex.OrderService.Carts;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Rex.CartService.Carts
{
    /// <summary>
    /// 购物车数据库上下文创建扩展
    /// </summary>
    public static class CartDbContextModelCreatingExtensions
    {
        public static void ConfigureCartManagement(this ModelBuilder builder)
        {
            Check.NotNull(builder, nameof(builder));

            // 购物车
            builder.Entity<Cart>(b =>
            {
                b.ToTable(OrderServiceConsts.DefaultDbTablePrefix + "Carts", OrderServiceConsts.DefaultDbSchema);
                b.ConfigureByConvention();
                b.ApplyObjectExtensionMappings();
            });
        }
    }
}