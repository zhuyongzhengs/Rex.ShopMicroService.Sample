using Microsoft.EntityFrameworkCore;
using Rex.OrderService;
using Rex.OrderService.Bills;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Rex.BillDeliverysService.Bills
{
    /// <summary>
    /// 发货单数据库上下文创建扩展
    /// </summary>
    public static class BillDeliverysDbContextModelCreatingExtensions
    {
        public static void ConfigureBillDeliverysManagement(this ModelBuilder builder)
        {
            Check.NotNull(builder, nameof(builder));

            // 售后单
            builder.Entity<BillDelivery>(b =>
            {
                b.ToTable(OrderServiceConsts.DefaultDbTablePrefix + "BillDeliverys", OrderServiceConsts.DefaultDbSchema);
                b.ConfigureByConvention();
                b.ApplyObjectExtensionMappings();
            });

            // 发货单明细
            builder.Entity<BillDeliveryItem>(b =>
            {
                b.ToTable(OrderServiceConsts.DefaultDbTablePrefix + "BillDeliveryItems", OrderServiceConsts.DefaultDbSchema);
                b.ConfigureByConvention();
                b.ApplyObjectExtensionMappings();
            });
        }
    }
}