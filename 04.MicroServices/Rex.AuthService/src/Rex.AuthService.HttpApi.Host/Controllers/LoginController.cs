using Lazy.Captcha.Core;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using Volo.Abp.AspNetCore.Mvc;

namespace Rex.AuthService.Controllers
{
    /// <summary>
    /// 账号登录控制器
    /// </summary>
    [Route("api/account")]
    public class LoginController : AbpController
    {
        private readonly ICaptcha _captcha;

        public LoginController(ICaptcha captcha)
        {
            _captcha = captcha;
        }

        /// <summary>
        /// 获取图片验证码
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns></returns>
        [HttpGet("captcha")]
        public IActionResult GetCaptcha([FromQuery] string id)
        {
            CaptchaData captcha = _captcha.Generate(id);
            return File(new MemoryStream(captcha.Bytes), "image/gif");
        }

        /*
        /// <summary>
        /// 测试接口
        /// </summary>
        /// <param name="isError">是否模拟异常</param>
        /// <returns></returns>
        [HttpGet("test")]
        public IActionResult Test([FromQuery] bool isError = false)
        {
            if (isError) throw new UserFriendlyException($"测试模拟异常！", SystemStatusCode.Fail.ToAuthServicePrefix());
            return Ok("OK");
        }
        */
    }
}