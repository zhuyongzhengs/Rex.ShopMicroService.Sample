using EasyAbp.Abp.WeChat.Common.Infrastructure.Services;
using EasyAbp.Abp.WeChat.MiniProgram.Services.Login;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Mvc;

namespace Rex.AuthService.Controllers
{
    /// <summary>
    /// 微信平台控制器
    /// </summary>
    [Route("api/authorization/wechat")]
    public class WeChatController : AbpController
    {
        private readonly IAbpWeChatServiceFactory _serviceFactory;

        public WeChatController(IAbpWeChatServiceFactory serviceFactory)
        {
            _serviceFactory = serviceFactory;
        }

        /// <summary>
        /// 获取登录凭证校验
        /// </summary>
        /// <param name="jsCode">登录时获取的jsCode</param>
        /// <returns></returns>
        [HttpGet("jscode2session")]
        public async Task<IActionResult> GetJscodeToSession([FromQuery] string jsCode)
        {
            LoginWeService loginWeService = await _serviceFactory.CreateAsync<LoginWeService>();
            Code2SessionResponse code2SessionResponse = await loginWeService.Code2SessionAsync(jsCode);
            return Ok(code2SessionResponse);
        }
    }
}