using Quartz;
using Rex.OrderService.Orders;
using Rex.Service.Core.Helper;
using Rex.Service.Core.Models;
using StackExchange.Redis;
using System.Threading.Tasks;
using Volo.Abp.BackgroundWorkers.Quartz;
using Volo.Abp.Caching;

namespace Rex.OrderService.BackgroundWorkers
{
    /// <summary>
    /// 自动取消订单任务
    /// </summary>
    /// <remarks>
    /// 未付款订单超时后取消订单操作任务
    /// </remarks>
    public class AutoCancelOrderWorker : QuartzBackgroundWorkerBase
    {
        public IDistributedCache<WorkerStatusRc> WorkerStatusDistributedCache { get; set; }
        private readonly IOrderRepository _orderRepository;
        private readonly IConnectionMultiplexer _connectionMultiplexer;

        public AutoCancelOrderWorker(IOrderRepository orderRepository, IConnectionMultiplexer connectionMultiplexer)
        {
            _orderRepository = orderRepository;
            _connectionMultiplexer = connectionMultiplexer;

            JobDetail = JobBuilder.Create<AutoCancelOrderWorker>().WithIdentity(nameof(AutoCancelOrderWorker)).Build();
            Trigger = TriggerBuilder.Create()
            .WithIdentity(nameof(AutoCancelOrderWorker))
            //.StartNow()
            .StartAt(DateBuilder.FutureDate(30, IntervalUnit.Second)) // 30秒后开启(防止项目还未启动就执行调度)
            // 每隔 5 分钟执行一次
            .WithSimpleSchedule(s => s
                .WithIntervalInMinutes(5)
                .RepeatForever())
            //.WithCronSchedule("0 0/5 * * * ? *")
            .Build();
        }

        public override async Task Execute(IJobExecutionContext context)
        {
            IDatabase redisDatabase = _connectionMultiplexer.GetDatabase();
            string rKey = $"Rex.OrderService:Lock:{{{nameof(AutoCancelOrderWorker)}}}";
            await DistributedTaskHelper.RunWithRedisLockAsync(
                redisDatabase,
                rKey,
                Logger,
                async () =>
                {
                    await _orderRepository.AutoCancelOrderJobAsync();
                });
            await Task.CompletedTask;
        }
    }
}