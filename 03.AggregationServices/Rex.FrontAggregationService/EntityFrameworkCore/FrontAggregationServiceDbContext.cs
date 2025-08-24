using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace Rex.FrontAggregationService.EntityFrameworkCore;

[ConnectionStringName(FrontAggregationServiceConsts.ConnectionStringName)]
public class FrontAggregationServiceDbContext :
    AbpDbContext<FrontAggregationServiceDbContext>
//IHasEventOutbox, IHasEventInbox
{
    /* Add DbSet properties for your Aggregate Roots / Entities here. */

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

    public FrontAggregationServiceDbContext(DbContextOptions<FrontAggregationServiceDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        AbpCommonDbProperties.DbTablePrefix = FrontAggregationServiceConsts.DefaultDbTablePrefix;
        AbpCommonDbProperties.DbSchema = FrontAggregationServiceConsts.DefaultDbSchema;

        /*

        #region 事件发/收件箱

        builder.ConfigureEventOutbox();

        builder.ConfigureEventInbox();

        #endregion 事件发/收件箱

        */

        //builder.Entity<YourEntity>(b =>
        //{
        //    b.ToTable(FrontAggregationServiceConsts.DbTablePrefix + "YourEntities", FrontAggregationServiceConsts.DbSchema);
        //    b.ConfigureByConvention(); //auto configure for the base class props
        //    //...
        //});
    }
}