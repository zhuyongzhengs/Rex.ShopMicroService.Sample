using System;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Identity;

namespace Rex.AuthService.WeChats
{
    /// <summary>
    /// 微信用户服务仓储接口
    /// </summary>
    public interface IUserWeChatRepository : IRepository<IdentityUser, Guid>
    {
        /// <summary>
        /// 微信小程序登录
        /// </summary>
        /// <param name="wxMpLogin">微信小程序登录信息</param>
        /// <param name="isCreateUser">是否创建用户</param>
        /// <remarks>
        /// isCreateUser：True[不存在则创建该用户]、False[不创建新用户,不存在则登录失败]
        /// </remarks>
        /// <returns></returns>
        Task<IdentityUser> WeChatMpLoginAsync(WxMpLoginDo wxMpLogin, bool isCreateUser = true);
    }
}