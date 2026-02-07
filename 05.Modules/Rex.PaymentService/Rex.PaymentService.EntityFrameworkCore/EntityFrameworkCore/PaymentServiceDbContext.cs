using Microsoft.EntityFrameworkCore;
using Rex.PaymentService.Payments;
using Volo.Abp.AuditLogging;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace Rex.PaymentService.EntityFrameworkCore;

[ConnectionStringName(PaymentServiceConsts.ConnectionStringName)]
public class PaymentServiceDbContext :
    AbpDbContext<PaymentServiceDbContext>/*, IHasEventOutbox, IHasEventInbox*/
{
    /*

    #region 事件发/收件箱

    /// <summary>
    /// 事件发件箱
    /// </summary>
    public DbSet<OutgoingEventRecord> OutgoingEvents { get; set; }

    /// <summary>
    /// 事件收件箱
    /// </summary>
    public DbSet<IncomingEventRecord> IncomingEvents { get; set; }

    #endregion 事件发/收件箱

    */

    #region 支付方式

    public DbSet<Payment> Payments { get; set; }

    #endregion 支付方式

    #region 支付单

    public DbSet<BillPayment> BillPayments { get; set; }

    #endregion 支付单

    #region 退款单

    public DbSet<BillRefund> BillRefunds { get; set; }

    #endregion 退款单

    public PaymentServiceDbContext(DbContextOptions<PaymentServiceDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        /*

        #region 事件发/收件箱

        builder.ConfigureEventOutbox();

        builder.ConfigureEventInbox();

        #endregion 事件发/收件箱

        */

        #region 审计日志

        AbpAuditLoggingDbProperties.DbTablePrefix = PaymentServiceConsts.SysDbTablePrefix;
        AbpAuditLoggingDbProperties.DbSchema = PaymentServiceConsts.SysDbSchema;

        #endregion 审计日志

        #region 支付

        builder.ConfigurePaymentManagement();

        #endregion 支付

        /* Include modules to your migration db context */

        /* Configure your own tables/entities inside here */

        //builder.Entity<YourEntity>(b =>
        //{
        //    b.ToTable(PaymentServiceConsts.DbTablePrefix + "YourEntities", PaymentServiceConsts.DbSchema);
        //    b.ConfigureByConvention(); //auto configure for the base class props
        //    //...
        //});
    }
}