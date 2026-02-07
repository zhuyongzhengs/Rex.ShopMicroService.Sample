using Lazy.Captcha.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Http;

namespace Rex.AuthService.Filters
{
    /// <summary>
    /// 图片验证码授权过滤拦截
    /// </summary>
    public class CaptchaAuthenticationFilter : IAsyncAuthorizationFilter
    {
        private readonly ICaptcha _captcha;
        private readonly ILogger<CaptchaAuthenticationFilter> _logger;

        public CaptchaAuthenticationFilter(ICaptcha captcha, ILogger<CaptchaAuthenticationFilter> logger)
        {
            _captcha = captcha;
            _logger = logger;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            // 判断图片验证码
            if (VerifyPwdLogin(context.HttpContext.Request))
            {
                string verificationKey = context.HttpContext.Request.Form["VerificationKey"];
                string verificationCode = context.HttpContext.Request.Form["VerificationCode"];
                if (string.IsNullOrWhiteSpace(verificationKey) || string.IsNullOrWhiteSpace(verificationCode))
                {
                    VerifyFail(context, "请输入验证码！");
                    return;
                }

                if (!_captcha.Validate(verificationKey, verificationCode))
                    VerifyFail(context, $"验证码[{verificationCode}]不正确，请重新输入！", verificationCode);
            }
            await Task.CompletedTask;
        }

        /// <summary>
        /// 验证不通过
        /// </summary>
        private void VerifyFail(AuthorizationFilterContext context, string message, string code = "")
        {
            throw new UserFriendlyException(message, "Rex:Captcha:Invalid", logLevel: LogLevel.Warning)
                .WithData("VerificationCode", code);
        }

        /// <summary>
        /// 验证是否为密码登录
        /// </summary>
        /// <param name="request">Http请求</param>
        /// <returns></returns>
        private bool VerifyPwdLogin(HttpRequest request)
        {
            if (request.HasFormContentType)
            {
                string grantType = request.Form["grant_type"];
                return request.Path.StartsWithSegments("/connect/token") && grantType.Equals("password", StringComparison.OrdinalIgnoreCase);
            }
            return false;
        }
    }
}