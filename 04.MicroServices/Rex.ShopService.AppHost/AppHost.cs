using Microsoft.Extensions.Hosting;
using Rex.ShopService.AppHost.Configurations;
using Rex.ShopService.AppHost.Extensions;
using System.Text;

Console.OutputEncoding = Encoding.UTF8;
Console.Title = "【Rex商城】Aspire 应用服务";

var builder = DistributedApplication.CreateBuilder(args);
var parameters = AppParameters.Create(builder); // 应用参数管理

#region 1.基础设施服务

/// <summary>
/// 定义 PostgreSQL 数据服务
/// </summary>
var postgres = builder.AddPostgres("rex-postgres",
    parameters.PostgreSql.Username,
    parameters.PostgreSql.Password, 6432)
    .WithContainerName("rex-postgres")
    .WithDataVolume()
    .WithLifetime(ContainerLifetime.Persistent)
    .WithPgAdmin(op =>
    {
        op.WithContainerName("rex-postgres-pgadmin");
        op.WithHostPort(8032);
        op.WithLifetime(ContainerLifetime.Persistent);
    }, "rex-postgres-pgadmin");

/// <summary>
/// 定义 MongoDB 数据服务
/// </summary>
var mongodb = builder.AddMongoDB("rex-mongo")
    .WithContainerName("rex-mongo")
    .WithDataVolume()
    .WithLifetime(ContainerLifetime.Persistent)
    .WithMongoExpress(op =>
    {
        op.WithContainerName("rex-mongo-express");
        op.WithHostPort(8081);
        op.WithExternalHttpEndpoints();
        op.WithLifetime(ContainerLifetime.Persistent);
    }, "rex-mongo-express");

/// <summary>
/// 定义 Redis 数据服务
/// </summary>
var redis = builder.AddRedis("rex-redis", 7379)
    .WithContainerName("rex-redis")
    .WithPassword(parameters.Redis.Password)
    .WithLifetime(ContainerLifetime.Persistent)
    .WithRedisCommander((op) =>
    {
        op.WithContainerName("rex-redis-commander");
        op.WithLifetime(ContainerLifetime.Persistent);
        op.WithHostPort(8079);
        op.WithUrlForEndpoint("http", url => url.DisplayLocation = UrlDisplayLocation.SummaryAndDetails);
    }, "rex-redis-commander");

/// <summary>
/// 定义 RabbitMQ 消息队列服务
/// </summary>
var rabbitmq = builder.AddRabbitMQ("rex-rabbitmq", parameters.RabbitMq.Username, parameters.RabbitMq.Password, port: 6672)
    .WithContainerName("rex-rabbitmq")
    .WithManagementPlugin(port: 25672)
    .WithLifetime(ContainerLifetime.Persistent)
    .WithUrlForEndpoint("rex-rabbitmq-management", url =>
    {
        url.DisplayText = "RabbitMQ 管理界面";
    });

/// <summary>
/// 定义 Minio 文件服务
/// </summary>
var minio = builder.AddMinioContainer("rex-minio", parameters.Minio.AccessKey, parameters.Minio.SecretKey, 9000)
    .WithContainerName("rex-minio")
    .WithDataVolume()
    .WithLifetime(ContainerLifetime.Persistent)
    .WithUrlForEndpoint("rex-minio-console", url =>
    {
        url.DisplayText = "MinIO 管理后台";
        url.DisplayLocation = UrlDisplayLocation.SummaryAndDetails;
    });

#endregion 1.基础设施服务

#region 2.商品微服务

// 共享的基础/授权库 (PostgreSQL)
var sharedDb = postgres.AddDatabase("SharedBaseDb", "Rex.Shop.BaseService");

// 审计日志库 (MongoDB)
var auditLogMongo = mongodb.AddDatabase("AbpAuditLogging");

// 商品微服务的业务库 (PostgreSQL)
var goodsDb = postgres.AddDatabase("GoodsDb", "Rex.Shop.GoodService");
var orderDb = postgres.AddDatabase("OrderDb", "Rex.Shop.OrderService");
var paymentDb = postgres.AddDatabase("PaymentDb", "Rex.Shop.PaymentService");
var promotionDb = postgres.AddDatabase("PromotionDb", "Rex.Shop.PromotionService");

/// <summary>
/// 认证授权服务
/// </summary>
var authApi = builder.AddProject<Projects.Rex_AuthService_HttpApi_Host>("auth-service")
    .WithCommonReferences(redis, sharedDb, auditLogMongo, rabbitmq);
//.WithHttpsEndpoint(port: 4466);
// 认证授权(数据迁移)
if (builder.Environment.IsDevelopment())
    builder.AddMigration<Projects.Rex_AuthService_DbMigrator>("auth-migrator", authApi, redis, sharedDb);

/// <summary>
/// 基础服务
/// </summary>
var baseApi = builder.AddProject<Projects.Rex_BaseService_HttpApi_Host>("base-service")
    .WithCommonReferences(redis, sharedDb, auditLogMongo, rabbitmq, minio)
    //.WithHttpsEndpoint(port: 4455)
    .WithReference(authApi).WaitFor(authApi);
// 基础服务(数据迁移)
if (builder.Environment.IsDevelopment())
    builder.AddMigration<Projects.Rex_BaseService_DbMigrator>("base-migrator", baseApi, redis, sharedDb);

/// <summary>
/// 商品服务
/// </summary>
var goodsApi = builder.AddProject<Projects.Rex_GoodService_HttpApi_Host>("goods-service")
    .WithCommonReferences(redis, sharedDb, auditLogMongo, rabbitmq)
    //.WithHttpsEndpoint(port: 4477)
    .WithReference(goodsDb, connectionName: "Goods")
    .WithReference(authApi).WaitFor(authApi);
// 商品服务(数据迁移)
if (builder.Environment.IsDevelopment())
    builder.AddMigration<Projects.Rex_GoodService_DbMigrator>("goods-migrator", goodsApi, redis, goodsDb, "Goods");

/// <summary>
/// 订单服务
/// </summary>
var orderApi = builder.AddProject<Projects.Rex_OrderService_HttpApi_Host>("order-service")
    .WithCommonReferences(redis, sharedDb, auditLogMongo, rabbitmq)
    //.WithHttpsEndpoint(port: 5500)
    .WithReference(orderDb, connectionName: "Order")
    .WithReference(authApi).WaitFor(authApi);
// 订单服务(数据迁移)
if (builder.Environment.IsDevelopment())
    builder.AddMigration<Projects.Rex_OrderService_DbMigrator>("order-migrator", orderApi, redis, orderDb, "Order");

/// <summary>
/// 支付服务
/// </summary>
var paymentApi = builder.AddProject<Projects.Rex_PaymentService_HttpApi_Host>("payment-service")
    .WithCommonReferences(redis, sharedDb, auditLogMongo, rabbitmq)
    //.WithHttpsEndpoint(port: 5510)
    .WithReference(paymentDb, connectionName: "Payment")
    .WithReference(authApi).WaitFor(authApi);
// 支付服务(数据迁移)
if (builder.Environment.IsDevelopment())
    builder.AddMigration<Projects.Rex_PaymentService_DbMigrator>("payment-migrator", paymentApi, redis, paymentDb, "Payment");

/// <summary>
/// 促销服务
/// </summary>
var promotionApi = builder.AddProject<Projects.Rex_PromotionService_HttpApi_Host>("promotion-service")
    .WithCommonReferences(redis, sharedDb, auditLogMongo, rabbitmq)
    //.WithHttpsEndpoint(port: 4488)
    .WithReference(promotionDb, connectionName: "Promotion")
    .WithReference(authApi).WaitFor(authApi);
// 促销服务(数据迁移)
if (builder.Environment.IsDevelopment())
    builder.AddMigration<Projects.Rex_PromotionService_DbMigrator>("promotion-migrator", promotionApi, redis, promotionDb, "Promotion");

#endregion 2.商品微服务

#region 3.聚合服务

/// <summary>
/// 前台聚合服务
/// </summary>
var frontAggregationService = builder.AddProject<Projects.Rex_FrontAggregationService>("front-aggregation")
    .WithAggregationReferences(redis, sharedDb, rabbitmq)
    //.WithHttpsEndpoint(port: 4420)
    .WithReference(authApi).WaitFor(authApi)
    .WithReference(baseApi).WaitFor(baseApi)
    .WithReference(goodsApi).WaitFor(goodsApi)
    .WithReference(orderApi).WaitFor(orderApi)
    .WithReference(paymentApi).WaitFor(paymentApi)
    .WithReference(promotionApi).WaitFor(promotionApi);

/// <summary>
/// 后台聚合服务
/// </summary>
var backendAggregationService = builder.AddProject<Projects.Rex_BackendAggregationService>("backend-aggregation")
    //.WithAggregationReferences(redis, sharedDb, rabbitmq)
    //.WithHttpsEndpoint(port: 4410)
    .WithReference(authApi).WaitFor(authApi)
    .WithReference(baseApi).WaitFor(baseApi)
    .WithReference(goodsApi).WaitFor(goodsApi)
    .WithReference(orderApi).WaitFor(orderApi)
    .WithReference(paymentApi).WaitFor(paymentApi)
    .WithReference(promotionApi).WaitFor(promotionApi);

#endregion 3.聚合服务

#region 4.网关服务

/// <summary>
/// 前台网关服务
/// </summary>
var webPublicGatewayService = builder.AddProject<Projects.Rex_Shop_WebPublicGateway>("web-public-gateway")
    //.WithHttpsEndpoint(port: 4433)
    .WithReference(frontAggregationService).WaitFor(frontAggregationService);

/// <summary>
/// 后台网关服务
/// </summary>
var webGatewayService = builder.AddProject<Projects.Rex_Shop_WebGateway>("web-gateway")
    //.WithHttpsEndpoint(port: 4444)
    .WithReference(backendAggregationService).WaitFor(backendAggregationService);

#endregion 4.网关服务

#region 5.后台管理平台

/// <summary>
/// 后台管理端
/// </summary>
var shopShopAdmin = builder.AddJavaScriptApp("rex-shop-admin", "../../01.App/Rex.App.WebAdmin/WebAdmin", "dev")
    .WithHttpEndpoint(port: 5120, name: "http", env: "VITE_PORT")
    .WithExternalHttpEndpoints()
    .WithReference(webGatewayService)
    .WaitFor(webGatewayService);

/*
/// <summary>
/// H5移动端
/// </summary>
var shopShopApp = builder.AddJavaScriptApp("rex-shop-app", "../../01.App/Rex.App.UniApp/RexShop", "dev:h5")
    .WithHttpEndpoint(port: 5130, name: "http", env: "VITE_PORT")
    .WithExternalHttpEndpoints()
    .WithReference(webPublicGatewayService)
    .WaitFor(webPublicGatewayService);
*/

#endregion 5.后台管理平台

var app = builder.Build();
FrameworkExtensions.OutputFrameworkInfo();
app.Run();