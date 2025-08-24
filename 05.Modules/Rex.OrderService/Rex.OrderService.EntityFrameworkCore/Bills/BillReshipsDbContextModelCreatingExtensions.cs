using Microsoft.EntityFrameworkCore;
using Rex.OrderService;
using Rex.OrderService.Bills;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Rex.BillReshipsService.Bills
{
    /// <summary>
    /// 退货单数据库上下文创建扩展
    /// </summary>
    public static class BillReshipsDbContextModelCreatingExtensions
    {
        public static void ConfigureBillReshipsManagement(this ModelBuilder builder)
        {
            Check.NotNull(builder, nameof(builder));

            // 退货单
            builder.Entity<BillReship>(b =>
            {
                b.ToTable(OrderServiceConsts.DefaultDbTablePrefix + "BillReships", OrderServiceConsts.DefaultDbSchema);
                b.ConfigureByConvention();
                b.ApplyObjectExtensionMappings();
            });

            // 退货单明细
            builder.Entity<BillReshipItem>(b =>
            {
                b.ToTable(OrderServiceConsts.DefaultDbTablePrefix + "BillReshipItems", OrderServiceConsts.DefaultDbSchema);
                b.ConfigureByConvention();
                b.ApplyObjectExtensionMappings();
            });
        }
    }
}