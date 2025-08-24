using Microsoft.EntityFrameworkCore;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Rex.GoodService.Stocks
{
    /// <summary>
    /// 库存表数据库上下文创建扩展
    /// </summary>
    public static class StockDbContextModelCreatingExtensions
    {
        public static void ConfigureStockManagement(this ModelBuilder builder)
        {
            Check.NotNull(builder, nameof(builder));

            // 库存操作表
            builder.Entity<Stock>(b =>
            {
                b.ToTable(GoodServiceConsts.DefaultDbTablePrefix + "Stocks", GoodServiceConsts.DefaultDbSchema);
                b.ConfigureByConvention();
                b.ApplyObjectExtensionMappings();
            });

            // 库存记录表
            builder.Entity<StockLog>(b =>
            {
                b.ToTable(GoodServiceConsts.DefaultDbTablePrefix + "StockLogs", GoodServiceConsts.DefaultDbSchema);
                b.ConfigureByConvention();
                b.ApplyObjectExtensionMappings();
            });
        }
    }
}