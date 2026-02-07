using Duende.IdentityModel.Client;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Http.Client.Authentication;

namespace Rex.FrontAggregationService.Core
{
    /// <summary>
    /// 动态C#客户端设置请求头
    /// </summary>
    public class RequestRemoteServiceHttpClientAuthenticator : IRemoteServiceHttpClientAuthenticator, ISingletonDependency
    {
        /// <summary>
        /// HttpContext访问器
        /// </summary>
        private readonly IHttpContextAccessor _httpContextAccessor;

        public RequestRemoteServiceHttpClientAuthenticator(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// 进行身份验证
        /// </summary>
        /// <param name="context">远程服务认证上下文</param>
        /// <returns></returns>
        public Task Authenticate(RemoteServiceHttpClientAuthenticateContext context)
        {
            var httpClient = context.Client;

            #region 获取Token

            string authorization = _httpContextAccessor?.HttpContext?.Request?.Headers["Authorization"];
            if (authorization != null)
            {
                string token = authorization.Split(' ')[1];
                httpClient.SetBearerToken(token);
            }

            #endregion 获取Token

            /*
            【自定义授权登录】
            // 获取对应的客户认证：在配置文件appsettings.secrets中的IdentityClients进行配置，默认 Default
            //string identityClientName = context.RemoteServiceName;
            bool authResult = Authenticator.TryAuthenticateAsync(httpClient).Result;
            Debug.WriteLine("授权结果：{0}", authResult);
            */
            return Task.CompletedTask;
        }
    }
}