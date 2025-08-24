using Microsoft.EntityFrameworkCore;
using Rex.OrderService;
using Rex.OrderService.Bills;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Rex.BillAftersalesService.Bills
{
    /// <summary>
    /// 售后单数据库上下文创建扩展
    /// </summary>
    public static class BillAftersalesDbContextModelCreatingExtensions
    {
        public static void ConfigureBillAftersalesManagement(this ModelBuilder builder)
        {
            Check.NotNull(builder, nameof(builder));

            // 售后单
            builder.Entity<BillAftersales>(b =>
            {
                b.ToTable(OrderServiceConsts.DefaultDbTablePrefix + "BillAftersales", OrderServiceConsts.DefaultDbSchema);
                b.ConfigureByConvention();
                b.ApplyObjectExtensionMappings();
            });

            // 售后单图片关联
            builder.Entity<BillAftersalesImages>(b =>
            {
                b.ToTable(OrderServiceConsts.DefaultDbTablePrefix + "BillAftersalesImages", OrderServiceConsts.DefaultDbSchema);
                b.ConfigureByConvention();
                b.ApplyObjectExtensionMappings();
            });

            // 售后单明细
            builder.Entity<BillAftersalesItem>(b =>
            {
                b.ToTable(OrderServiceConsts.DefaultDbTablePrefix + "BillAftersalesItems", OrderServiceConsts.DefaultDbSchema);
                b.ConfigureByConvention();
                b.ApplyObjectExtensionMappings();
            });
        }
    }
}