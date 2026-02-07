using Microsoft.EntityFrameworkCore;
using Rex.BillAftersalesService.Bills;
using Rex.BillDeliverysService.Bills;
using Rex.BillReshipsService.Bills;
using Rex.CartService.Carts;
using Rex.OrderService.Bills;
using Rex.OrderService.Carts;
using Rex.OrderService.Logisticss;
using Rex.OrderService.Orders;
using Rex.OrderService.Ships;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace Rex.OrderService.EntityFrameworkCore;

[ConnectionStringName(OrderServiceConsts.ConnectionStringName)]
public class OrderServiceDbContext :
    AbpDbContext<OrderServiceDbContext>/*, IHasEventOutbox, IHasEventInbox*/
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

    #region 购物车

    public DbSet<Cart> Carts { get; set; }

    #endregion 购物车

    #region 订单

    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<OrderLog> OrderLogs { get; set; }

    #endregion 订单

    #region 物流

    public DbSet<Logistics> Logistics { get; set; }

    #endregion 物流

    #region 配送方式

    public DbSet<Ship> Ships { get; set; }

    #endregion 配送方式

    #region 售后单

    public DbSet<BillAftersales> BillAftersales { get; set; }
    public DbSet<BillAftersalesImages> BillAftersalesImages { get; set; }
    public DbSet<BillAftersalesItem> BillAftersalesItems { get; set; }

    #endregion 售后单

    #region 发货单

    public DbSet<BillDelivery> BillDeliverys { get; set; }
    public DbSet<BillDeliveryItem> BillDeliveryItems { get; set; }

    #endregion 发货单

    #region 退货单

    public DbSet<BillReship> BillReships { get; set; }
    public DbSet<BillReshipItem> BillReshipItems { get; set; }

    #endregion 退货单

    public OrderServiceDbContext(DbContextOptions<OrderServiceDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        /* Include modules to your migration db context */

        /*

        #region 事件发/收件箱

        builder.ConfigureEventOutbox();

        builder.ConfigureEventInbox();

        #endregion 事件发/收件箱

        */

        #region 购物车

        builder.ConfigureCartManagement();

        #endregion 购物车

        #region 订单

        builder.ConfigureOrderManagement();

        #endregion 订单

        #region 物流

        builder.ConfigureLogisticsManagement();

        #endregion 物流

        #region 配送方式

        builder.ConfigureShipManagement();

        #endregion 配送方式

        #region 售后单

        builder.ConfigureBillAftersalesManagement();

        #endregion 售后单

        #region 发货单

        builder.ConfigureBillDeliverysManagement();

        #endregion 发货单

        #region 退货单

        builder.ConfigureBillReshipsManagement();

        #endregion 退货单
    }
}