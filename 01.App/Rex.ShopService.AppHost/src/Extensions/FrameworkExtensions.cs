using Spectre.Console;

namespace Rex.ShopService.AppHost.Extensions
{
    /// <summary>
    /// 框架扩展信息
    /// </summary>
    public static class FrameworkExtensions
    {
        /// <summary>
        /// 输出框架信息
        /// </summary>
        public static void OutputFrameworkInfo()
        {
            AnsiConsole.WriteLine();
            AnsiConsole.MarkupLine("[dodgerblue1]┏" + new string('━', 64) + "┓[/]");
            AnsiConsole.WriteLine();
            AnsiConsole.MarkupLine("[teal]   ██████╗ ███████╗██╗  ██╗    ███████╗██╗  ██╗ ██████╗ ██████╗ [/]");
            AnsiConsole.MarkupLine("[darkcyan]   ██╔══██╗██╔════╝╚██╗██╔╝    ██╔════╝██║  ██║██╔═══██╗██╔══██╗[/]");
            AnsiConsole.MarkupLine("[deepskyblue1]   ██████╔╝█████╗   ╚███╔╝     ███████╗███████║██║   ██║██████╔╝[/]");
            AnsiConsole.MarkupLine("[dodgerblue1]   ██╔══██╗██╔══╝   ██╔██╗     ╚════██║██╔══██║██║   ██║██╔═══╝ [/]");
            AnsiConsole.MarkupLine("[steelblue1]   ██║  ██║███████╗██╔╝ ██╗    ███████║██║  ██║╚██████╔╝██║     [/]");
            AnsiConsole.MarkupLine("[blue3]   ╚═╝  ╚═╝╚══════╝╚═╝  ╚═╝    ╚══════╝╚═╝  ╚═╝ ╚═════╝ ╚═╝     [/]");
            AnsiConsole.WriteLine();
            AnsiConsole.MarkupLine("[Cyan]" + new string(' ', 10) + "一款基于ABP Framework 的微服务商城平台[/]");
            AnsiConsole.WriteLine();
            AnsiConsole.MarkupLine("[dodgerblue1]┗" + new string('━', 64) + "┛[/]");
            AnsiConsole.WriteLine();
            AnsiConsole.MarkupLine(
                "[cyan].NET框架版本：[/][Grey93].NET 10[/]" + new string(' ', 5) +
                "[cyan]ABP框架版本：[/][Grey93]10.0.2[/]" + new string(' ', 5) +
                "[cyan]容器编排：[/][Grey93]Aspire[/]"
            );
            AnsiConsole.WriteLine();
            //Thread.Sleep(1500);
            Tree baseTree = new Tree("[bold darkcyan]◆ 基础设施服务[/]");
            baseTree.AddNode("[Grey93]PostgreSQL 17.6[/]");
            baseTree.AddNode("[Grey93]Redis 8.2[/]");
            baseTree.AddNode("[Grey93]MongoDB 8.2[/]");
            baseTree.AddNode("[Grey93]RabbitMQ 4.2-management[/]");
            baseTree.AddNode("[Grey93]Minio RELEASE.2025[/]");
            baseTree.AddNode("[Grey93]Elasticsearch[Gold3_1](按需)[/] 9.3.0[/]");
            AnsiConsole.Write(baseTree);
            AnsiConsole.WriteLine();
            Tree goodsTree = new Tree("[bold darkcyan]◆ 商品微服务[/]");
            goodsTree.AddNode("[Grey93]认证授权服务[/]");
            goodsTree.AddNode("[Grey93]用户基础服务[/]");
            goodsTree.AddNode("[Grey93]商品服务[/]");
            goodsTree.AddNode("[Grey93]订单服务[/]");
            goodsTree.AddNode("[Grey93]支付服务[/]");
            goodsTree.AddNode("[Grey93]促销服务[/]");
            AnsiConsole.Write(goodsTree);
            AnsiConsole.WriteLine();
            Tree aggregationTree = new Tree("[bold darkcyan]◆ 聚合服务[/]");
            aggregationTree.AddNode("[Grey93]前台聚合服务[/]");
            aggregationTree.AddNode("[Grey93]后台聚合服务[/]");
            AnsiConsole.Write(aggregationTree);
            AnsiConsole.WriteLine();
            Tree gatewayTree = new Tree("[bold darkcyan]◆ 网关服务[/]");
            gatewayTree.AddNode("[Grey93]前台(公共)网关服务[/]");
            gatewayTree.AddNode("[Grey93]后台网关服务[/]");
            AnsiConsole.Write(gatewayTree);
            AnsiConsole.WriteLine();
            Tree webManageTree = new Tree("[bold darkcyan]◆ 应用程序[/]");
            webManageTree.AddNode("[Grey93]后台管理平台[/] [SeaGreen1]✔[/]");
            webManageTree.AddNode("[Grey93]微信小程序[/] [SeaGreen1]✔[/]");
            webManageTree.AddNode("[Grey93]H5[/] [Red3]✘[/]");
            webManageTree.AddNode("[Grey93]Android[/] [Red3]✘[/]");
            webManageTree.AddNode("[Grey93]IPhone[/] [Red3]✘[/]");
            AnsiConsole.Write(webManageTree);
            AnsiConsole.WriteLine();
            AnsiConsole.MarkupLine("[darkcyan]Rex Shop 服务初始化完成…[/]");
            AnsiConsole.WriteLine();
            //Thread.Sleep(1500);
        }
    }
}