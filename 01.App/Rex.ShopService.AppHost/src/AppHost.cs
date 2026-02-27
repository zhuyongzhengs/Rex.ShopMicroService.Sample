using Rex.ShopService.AppHost.Configurations;
using Rex.ShopService.AppHost.Extensions;
using System.Text;

Console.OutputEncoding = Encoding.UTF8;
Console.Title = "【Rex商城】Aspire 应用服务";

var builder = DistributedApplication.CreateBuilder(args);

// 添加服务编排
builder.AddOrchestrationService();

var app = builder.Build();
FrameworkExtensions.OutputFrameworkInfo();
app.Run();