using Microsoft.EntityFrameworkCore;
using Rex.PromotionService.PinTuans;
using Rex.PromotionService.Promotions;
using Volo.Abp.AuditLogging;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace Rex.PromotionService.EntityFrameworkCore;

[ConnectionStringName(PromotionServiceConsts.ConnectionStringName)]
public class PromotionServiceDbContext :
    AbpDbContext<PromotionServiceDbContext>/*, IHasEventOutbox, IHasEventInbox*/
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

    #region 促销

    public DbSet<Promotion> Promotions { get; set; }
    public DbSet<PromotionCondition> PromotionConditions { get; set; }
    public DbSet<PromotionRecord> PromotionRecords { get; set; }
    public DbSet<PromotionResult> PromotionResults { get; set; }
    public DbSet<Coupon> Coupons { get; set; }

    #endregion 促销

    #region 拼团

    public DbSet<PinTuanGood> PinTuanGoods { get; set; }
    public DbSet<PinTuanRecord> PinTuanRecords { get; set; }
    public DbSet<PinTuanRule> PinTuanRules { get; set; }

    #endregion 拼团

    public PromotionServiceDbContext(DbContextOptions<PromotionServiceDbContext> options)
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

        #region 促销

        builder.ConfigurePromotionManagement();

        #endregion 促销

        #region 拼团

        builder.ConfigurePinTuanManagement();

        #endregion 拼团

        //builder.Entity<YourEntity>(b =>
        //{
        //    b.ToTable(PromotionServiceConsts.DbTablePrefix + "YourEntities", PromotionServiceConsts.DbSchema);
        //    b.ConfigureByConvention(); //auto configure for the base class props
        //    //...
        //});
    }
}