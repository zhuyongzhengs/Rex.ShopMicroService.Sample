using Quartz;
using Rex.OrderService.Orders;
using Rex.Service.Core.Models;
using StackExchange.Redis;
using System.Threading.Tasks;
using Volo.Abp.BackgroundWorkers.Quartz;
using Volo.Abp.Caching;

namespace Rex.OrderService.BackgroundWorkers
{
    /// <summary>
    /// 订单催付款任务
    /// </summary>
    public class RemindOrderPayWorker : QuartzBackgroundWorkerBase
    {
        public IDistributedCache<WorkerStatusRc> WorkerStatusDistributedCache { get; set; }
        private readonly IOrderRepository _orderRepository;
        private readonly IConnectionMultiplexer _connectionMultiplexer;

        public RemindOrderPayWorker(IOrderRepository orderRepository, IConnectionMultiplexer connectionMultiplexer)
        {
            _orderRepository = orderRepository;
            _connectionMultiplexer = connectionMultiplexer;

            JobDetail = JobBuilder.Create<RemindOrderPayWorker>().WithIdentity(nameof(RemindOrderPayWorker)).Build();
            Trigger = TriggerBuilder.Create()
            .WithIdentity(nameof(RemindOrderPayWorker))
            //.StartNow()
            .StartAt(DateBuilder.FutureDate(30, IntervalUnit.Second)) // 30秒后开启(防止项目还未启动就执行调度)
            // 每5分钟催付款订单
            .WithSimpleSchedule(s => s
                .WithIntervalInMinutes(5)
                .RepeatForever())
            //.WithCronSchedule("0 0/5 * * * ? *")
            .Build();
        }

        public override async Task Execute(IJobExecutionContext context)
        {
            /*
            IDatabase redisDatabase = _connectionMultiplexer.GetDatabase();
            string rKey = $"Rex.OrderService:Lock:{{{nameof(RemindOrderPayWorker)}}}";
            await DistributedTaskHelper.RunWithRedisLockAsync(
                redisDatabase,
                rKey,
                Logger,
                async () =>
                {
                    // TODO：发送通知消息 ---> 待实现
                });
            */
            await Task.CompletedTask;
        }
    }
}