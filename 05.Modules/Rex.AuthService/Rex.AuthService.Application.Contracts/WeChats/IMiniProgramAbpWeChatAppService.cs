using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Rex.AuthService.WeChats
{
    /// <summary>
    /// 微信小程序应用服务接口
    /// </summary>
    public interface IMiniProgramAbpWeChatAppService : IApplicationService
    {
        /// <summary>
        /// 测试分布式Redis缓存
        /// </summary>
        /// <param name="testValue">测试值</param>
        /// <returns></returns>
        Task TestDistributedRedisCache(string testValue);
    }
}