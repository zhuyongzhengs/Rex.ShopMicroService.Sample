namespace Rex.App.WebAdmin
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.Title = "【Rex商城】后台管理平台";
            Console.WriteLine("【提示】进入[WebAdmin]文件夹执行命令：npm run dev");

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