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
    /// 订单自动完成任务
    /// </summary>
    public class AutoCompleteOrderWorker : QuartzBackgroundWorkerBase
    {
        public IDistributedCache<WorkerStatusRc> WorkerStatusDistributedCache { get; set; }
        private readonly IOrderRepository _orderRepository;
        private readonly IConnectionMultiplexer _connectionMultiplexer;

        public AutoCompleteOrderWorker(IOrderRepository orderRepository, IConnectionMultiplexer connectionMultiplexer)
        {
            _orderRepository = orderRepository;
            _connectionMultiplexer = connectionMultiplexer;

            JobDetail = JobBuilder.Create<AutoCompleteOrderWorker>().WithIdentity(nameof(AutoCompleteOrderWorker)).Build();
            Trigger = TriggerBuilder.Create()
            .WithIdentity(nameof(AutoCompleteOrderWorker))
            //.StartNow()
            .StartAt(DateBuilder.FutureDate(30, IntervalUnit.Second)) // 30秒后开启(防止项目还未启动就执行调度)
            // 每小时自动完成订单
            .WithSimpleSchedule(s => s
                .WithIntervalInHours(1)
                .RepeatForever())
            //.WithCronSchedule("0 0 0/1 * * ? *")
            .Build();
        }

        public override async Task Execute(IJobExecutionContext context)
        {
            IDatabase redisDatabase = _connectionMultiplexer.GetDatabase();
            string rKey = $"Rex.OrderService:Lock:{{{nameof(AutoCompleteOrderWorker)}}}";
            await DistributedTaskHelper.RunWithRedisLockAsync(
                redisDatabase,
                rKey,
                Logger,
                async () =>
                {
                    await _orderRepository.AutoCompleteOrderJobAsync();
                });
            await Task.CompletedTask;
        }
    }
}