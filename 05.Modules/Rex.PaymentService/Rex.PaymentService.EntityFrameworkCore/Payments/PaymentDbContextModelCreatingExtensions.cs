using Microsoft.EntityFrameworkCore;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Rex.PaymentService.Payments
{
    /// <summary>
    /// 支付方式数据库上下文创建扩展
    /// </summary>
    public static class PaymentDbContextModelCreatingExtensions
    {
        public static void ConfigurePaymentManagement(this ModelBuilder builder)
        {
            Check.NotNull(builder, nameof(builder));

            // 支付方式
            builder.Entity<Payment>(b =>
            {
                b.ToTable(PaymentServiceConsts.DefaultDbTablePrefix + "Payments", PaymentServiceConsts.DefaultDbSchema);
                b.ConfigureByConvention();
                b.ApplyObjectExtensionMappings();
            });

            // 支付单
            builder.Entity<BillPayment>(b =>
            {
                b.ToTable(PaymentServiceConsts.DefaultDbTablePrefix + "BillPayments", PaymentServiceConsts.DefaultDbSchema);
                b.HasIndex(bp => bp.No).IsUnique(); // 唯一(不允许重复)
                b.ConfigureByConvention();
                b.ApplyObjectExtensionMappings();
            });

            // 退款单
            builder.Entity<BillRefund>(b =>
            {
                b.ToTable(PaymentServiceConsts.DefaultDbTablePrefix + "BillRefunds", PaymentServiceConsts.DefaultDbSchema);
                b.HasIndex(br => br.No).IsUnique(); // 唯一(不允许重复)
                b.ConfigureByConvention();
                b.ApplyObjectExtensionMappings();
            });
        }
    }
}