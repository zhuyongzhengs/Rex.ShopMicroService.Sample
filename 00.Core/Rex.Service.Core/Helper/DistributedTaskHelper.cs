using Microsoft.Extensions.Logging;
using StackExchange.Redis;
using System;
using System.Threading.Tasks;

namespace Rex.Service.Core.Helper
{
    public class DistributedTaskHelper
    {
        /// <summary>
        /// 通用分布式任务执行器，防止集群下重复执行
        /// </summary>
        /// <param name="db">Redis数据库</param>
        /// <param name="lockKey">锁Key（建议用任务名）</param>
        /// <param name="logger">日志</param>
        /// <param name="taskFunc">要执行的任务</param>
        /// <param name="lockExpireSeconds">锁过期时间（秒）</param>
        public static async Task RunWithRedisLockAsync(
            IDatabase db,
            string lockKey,
            ILogger logger,
            Func<Task> taskFunc,
            int lockExpireSeconds = 600)
        {
            string lockValue = $"Begin：{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}";
            bool acquired = await db.StringSetAsync(lockKey, lockValue, TimeSpan.FromSeconds(lockExpireSeconds), When.NotExists);
            if (!acquired)
            {
                logger?.LogInformation($"任务 {lockKey} 有其他节点正在执行，跳过本次。");
                return;
            }

            try
            {
                await taskFunc();
            }
            catch (Exception ex)
            {
                logger?.LogError(ex, $"任务 {lockKey} 执行异常");
                throw;
            }
            finally
            {
                // 只释放自己加的锁
                string script = @"
                    if redis.call('get', KEYS[1]) == ARGV[1] then
                        return redis.call('del', KEYS[1])
                    else
                        return 0
                    end"
                ;
                await db.ScriptEvaluateAsync(script, new RedisKey[] { lockKey }, new RedisValue[] { lockValue });
            }
        }
    }
}