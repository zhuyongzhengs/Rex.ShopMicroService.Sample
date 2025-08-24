using Lazy.Captcha.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Rex.Service.Core.Configurations;
using System;
using System.Threading.Tasks;
using Volo.Abp;

namespace Rex.AuthService.Filters
{
    /// <summary>
    /// 图片验证码授权过滤拦截
    /// </summary>
    public class CaptchaAuthenticationFilter : IAsyncAuthorizationFilter
    {
        private readonly ICaptcha _captcha;

        public CaptchaAuthenticationFilter(ICaptcha captcha)
        {
            _captcha = captcha;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            // 判断图片验证码
            if (VerifyPwdLogin(context.HttpContext.Request))
            {
                string verificationKey = context.HttpContext.Request.Form["VerificationKey"];
                string verificationCode = context.HttpContext.Request.Form["VerificationCode"];
                if (string.IsNullOrWhiteSpace(verificationKey) || string.IsNullOrWhiteSpace(verificationCode))
                    throw new UserFriendlyException($"请输入验证码！", SystemStatusCode.Fail.ToAuthServicePrefix()).WithData("VerificationKey", string.Empty);
                if (!_captcha.Validate(verificationKey, verificationCode))
                    throw new UserFriendlyException($"验证码[{verificationCode}]不正确，请重新输入！", SystemStatusCode.Fail.ToAuthServicePrefix()).WithData("VerificationCode", verificationCode);
            }

            await Task.CompletedTask;
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