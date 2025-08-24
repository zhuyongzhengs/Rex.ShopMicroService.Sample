using Microsoft.EntityFrameworkCore;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Rex.OrderService.Logisticss
{
    /// <summary>
    /// 物流数据库上下文创建扩展
    /// </summary>
    public static class LogisticsDbContextModelCreatingExtensions
    {
        public static void ConfigureLogisticsManagement(this ModelBuilder builder)
        {
            Check.NotNull(builder, nameof(builder));

            // 物流
            builder.Entity<Logistics>(b =>
            {
                b.ToTable(OrderServiceConsts.DefaultDbTablePrefix + "Logistics", OrderServiceConsts.DefaultDbSchema);
                b.ConfigureByConvention();
                b.ApplyObjectExtensionMappings();
            });
        }
    }
}