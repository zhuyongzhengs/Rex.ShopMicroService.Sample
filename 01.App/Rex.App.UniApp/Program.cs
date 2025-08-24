namespace Rex.App.UniApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.Title = "【Rex商城】App平台";
            Console.WriteLine("【提示】进入[RexShop]文件夹执行命令：npm run dev:mp-weixin");

            /*
            var builder = WebApplication.CreateBuilder(args);
            var app = builder.Build();

            app.MapGet("/", () => "Hello World!");

            app.Run();
            */

            Console.ReadKey();
        }
    }
}