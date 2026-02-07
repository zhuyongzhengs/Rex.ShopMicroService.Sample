using Microsoft.EntityFrameworkCore;
using Rex.GoodService.Areas;
using Rex.GoodService.Articles;
using Rex.GoodService.Brands;
using Rex.GoodService.Forms;
using Rex.GoodService.Goods;
using Rex.GoodService.Labels;
using Rex.GoodService.Notices;
using Rex.GoodService.PageDesigns;
using Rex.GoodService.Products;
using Rex.GoodService.ServiceDescriptions;
using Rex.GoodService.ServiceGoods;
using Rex.GoodService.Stocks;
using Rex.GoodService.StoreClerks;
using Rex.GoodService.Stores;
using Volo.Abp.AuditLogging;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace Rex.GoodService.EntityFrameworkCore;

[ConnectionStringName(GoodServiceConsts.ConnectionStringName)]
public class GoodServiceDbContext :
    AbpDbContext<GoodServiceDbContext>/*, IHasEventOutbox, IHasEventInbox*/
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

    #region 品牌

    public DbSet<Brand> Brands { get; set; }

    #endregion 品牌

    #region 商品

    public DbSet<Good> Goods { get; set; }
    public DbSet<GoodBrowsing> GoodBrowsings { get; set; }
    public DbSet<GoodCategory> GoodCategorys { get; set; }
    public DbSet<GoodCategoryExtend> GoodCategoryExtends { get; set; }
    public DbSet<GoodCollection> GoodCollections { get; set; }
    public DbSet<GoodComment> GoodComments { get; set; }
    public DbSet<GoodGrade> GoodGrades { get; set; }
    public DbSet<GoodImage> GoodImages { get; set; }
    public DbSet<GoodParam> GoodParams { get; set; }
    public DbSet<GoodTypeSpec> GoodTypeSpecs { get; set; }
    public DbSet<GoodTypeSpecValue> GoodTypeSpecValues { get; set; }

    #endregion 商品

    #region 标签

    public DbSet<Label> Labels { get; set; }

    #endregion 标签

    #region 货品

    public DbSet<Product> Products { get; set; }
    public DbSet<ProductDistribution> ProductDistributions { get; set; }

    #endregion 货品

    #region 服务商品

    public DbSet<ServiceGood> ServiceGoods { get; set; }

    #endregion 服务商品

    #region 门店

    public DbSet<Store> Stores { get; set; }

    #endregion 门店

    #region 店铺店员关联

    public DbSet<StoreClerk> StoreClerks { get; set; }

    #endregion 店铺店员关联

    #region 区域

    public DbSet<Area> Areas { get; set; }

    #endregion 区域

    #region 文章管理

    public DbSet<Article> Articles { get; set; }
    public DbSet<ArticleType> ArticleTypes { get; set; }

    #endregion 文章管理

    #region 页面设计

    public DbSet<PageDesign> PageDesigns { get; set; }
    public DbSet<PageDesignItem> PageDesignItems { get; set; }

    #endregion 页面设计

    #region 公告

    public DbSet<Notice> Notices { get; set; }

    #endregion 公告

    #region 表单

    public DbSet<Form> Forms { get; set; }

    #endregion 表单

    #region 商城服务描述

    public DbSet<ServiceDescription> ServiceDescriptions { get; set; }

    #endregion 商城服务描述

    #region 库存信息

    public DbSet<Stock> Stocks { get; set; }
    public DbSet<StockLog> StockLogs { get; set; }

    #endregion 库存信息

    public GoodServiceDbContext(DbContextOptions<GoodServiceDbContext> options)
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

        AbpAuditLoggingDbProperties.DbTablePrefix = GoodServiceConsts.SysDbTablePrefix;
        AbpAuditLoggingDbProperties.DbSchema = GoodServiceConsts.SysDbSchema;

        #endregion 审计日志

        #region 品牌

        builder.ConfigureBrandManagement();

        #endregion 品牌

        #region 商品

        builder.ConfigureGoodManagement();

        #endregion 商品

        #region 标签

        builder.ConfigureLabelManagement();

        #endregion 标签

        #region 货品

        builder.ConfigureProductManagement();

        #endregion 货品

        #region 服务商品

        builder.ConfigureServiceGoodManagement();

        #endregion 服务商品

        #region 门店

        builder.ConfigureStoreManagement();

        #endregion 门店

        #region 店铺店员关联

        builder.ConfigureStoreClerkManagement();

        #endregion 店铺店员关联

        #region 区域

        builder.ConfigureAreaManagement();

        #endregion 区域

        #region 分类管理

        builder.ConfigureArticleManagement();

        #endregion 分类管理

        #region 页面设计

        builder.ConfigurePageDesignManagement();

        #endregion 页面设计

        #region 公告

        builder.ConfigureNoticeManagement();

        #endregion 公告

        #region 表单

        builder.ConfigureFormManagement();

        #endregion 表单

        #region 商城服务描述

        builder.ConfigureServiceDescriptionManagement();

        #endregion 商城服务描述

        #region 库存信息

        builder.ConfigureStockManagement();

        #endregion 库存信息

        //builder.Entity<YourEntity>(b =>
        //{
        //    b.ToTable(GoodServiceConsts.DbTablePrefix + "YourEntities", GoodServiceConsts.DbSchema);
        //    b.ConfigureByConvention(); //auto configure for the base class props
        //    //...
        //});
    }
}