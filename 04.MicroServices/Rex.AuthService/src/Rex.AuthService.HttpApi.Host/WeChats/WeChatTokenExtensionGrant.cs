using EasyAbp.Abp.WeChat.Common.Infrastructure.Services;
using EasyAbp.Abp.WeChat.MiniProgram.Services.PhoneNumber;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using OpenIddict.Abstractions;
using OpenIddict.Server.AspNetCore;
using Rex.Service.Core.Configurations;
using System;
using System.Diagnostics;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.DependencyInjection;
using Volo.Abp.OpenIddict;
using Volo.Abp.OpenIddict.Controllers;
using Volo.Abp.OpenIddict.ExtensionGrantTypes;
using Volo.Abp.SecurityLog;
using Volo.Abp.SettingManagement;

namespace Rex.AuthService.WeChats
{
    /// <summary>
    /// 微信[小程序]授权扩展
    /// </summary>
    [IgnoreAntiforgeryToken]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class WeChatTokenExtensionGrant : AbpOpenIdDictControllerBase, ITokenExtensionGrant
    {
        // 扩展授权类型名称
        public const string ExtensionGrantName = "wechat_code";

        public string Name => ExtensionGrantName;

        public IUserWeChatRepository UserWeChatRepository => LazyServiceProvider.GetRequiredService<IUserWeChatRepository>();
        public ISettingManager SettingManager => LazyServiceProvider.GetRequiredService<ISettingManager>();
        public ISecurityLogManager SecurityLogManager => LazyServiceProvider.GetRequiredService<ISecurityLogManager>();
        public IOptions<IdentityOptions> IdentityOptions => LazyServiceProvider.LazyGetRequiredService<IOptions<IdentityOptions>>();
        public IAbpWeChatServiceFactory AbpWeChatServiceFactory => LazyServiceProvider.LazyGetRequiredService<IAbpWeChatServiceFactory>();

        public async Task<IActionResult> HandleAsync(ExtensionGrantContext context)
        {
            LazyServiceProvider = context.HttpContext.RequestServices.GetRequiredService<IAbpLazyServiceProvider>();

            string? openId = context.Request?.GetParameter("openid")?.ToString();
            string? sessionKey = context.Request?.GetParameter("session_Key")?.ToString();
            string? unionId = context.Request?.GetParameter("unionid")?.ToString();
            string? inviteCode = context.Request?.GetParameter("invitecode")?.ToString();
            string? code = context.Request?.GetParameter("code")?.ToString();
            if (string.IsNullOrWhiteSpace(openId)) throw new UserFriendlyException($"openid参数不能为空！", SystemStatusCode.Fail.ToAuthServicePrefix()).WithData("openid", openId);
            if (string.IsNullOrWhiteSpace(sessionKey)) throw new UserFriendlyException($"session_Key参数不能为空！", SystemStatusCode.Fail.ToAuthServicePrefix()).WithData("session_Key", sessionKey);

            // 根据Code获取手机号码
            PhoneNumberWeService phoneNumberWeService = await AbpWeChatServiceFactory.CreateAsync<PhoneNumberWeService>();
            GetPhoneNumberResponse phoneNumberResponse = await phoneNumberWeService.GetPhoneNumberAsync(code);
            if (phoneNumberResponse.ErrorCode != 0)
                throw new UserFriendlyException($"未获取到手机号码，请稍后重试！", SystemStatusCode.Fail.ToAuthServicePrefix());

            WxMpLoginDo wxMpLogin = new WxMpLoginDo();
            wxMpLogin.OpenId = openId;
            wxMpLogin.SessionKey = sessionKey;
            wxMpLogin.Unionid = unionId;
            wxMpLogin.InviteCode = inviteCode;
            wxMpLogin.CountryCode = phoneNumberResponse.PhoneInfo.CountryCode;
            wxMpLogin.PhoneNumber = phoneNumberResponse.PhoneInfo.PhoneNumber;
            ClaimsPrincipal claimsPrincipal = await ServerValidate(context, wxMpLogin);
            if (claimsPrincipal == null) throw new UserFriendlyException($"微信未绑定或该用户未激活！", SystemStatusCode.Fail.ToAuthServicePrefix());
            string action = OpenIddictSecurityLogActionConsts.LoginSucceeded;
            try
            {
                return SignIn(claimsPrincipal, OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
            }
            catch (Exception ex)
            {
                action = OpenIddictSecurityLogActionConsts.LoginFailed;
                Debug.WriteLine(ex.Message);
                return Forbid();
            }
            finally
            {
                await SaveSecurityLogAsync(context, claimsPrincipal, action);
            }
        }

        /// <summary>
        /// 保存安全日志
        /// </summary>
        /// <param name="context">扩展授权上下文</param>
        /// <param name="claimsPrincipal">主要的声明</param>
        /// <param name="action">认证结果</param>
        private async Task SaveSecurityLogAsync(ExtensionGrantContext context, ClaimsPrincipal claimsPrincipal, string action)
        {
            OpenIddictRequest openIddictRequest = context.Request;
            HttpContext httpContext = context.HttpContext;

            var application = await ApplicationManager.FindByClientIdAsync(openIddictRequest.ClientId) ?? throw new InvalidOperationException(L["DetailsConcerningTheCallingClientApplicationCannotBeFound"]);
            var appName = await ApplicationManager.GetDisplayNameAsync(application);
            await SecurityLogManager.SaveAsync(securityLogInfo =>
            {
                securityLogInfo.ApplicationName = appName;
                securityLogInfo.Identity = OpenIddictSecurityLogIdentityConsts.OpenIddict;
                securityLogInfo.Action = action;
                securityLogInfo.UserId = claimsPrincipal.Identity.FindUserId();
                securityLogInfo.UserName = claimsPrincipal.Identity?.Name;
                securityLogInfo.TenantId = claimsPrincipal.Identity.FindTenantId();
                securityLogInfo.ClientId = openIddictRequest.ClientId;
                securityLogInfo.ClientIpAddress = httpContext.Connection?.RemoteIpAddress?.ToString();
                securityLogInfo.BrowserInfo = httpContext.Request.Headers["User-Agent"].ToString();
                securityLogInfo.CreationTime = DateTime.Now;
            });
        }

        /// <summary>
        /// 服务器验证
        /// </summary>
        /// <param name="context">扩展授权上下文</param>
        /// <param name="wxMpLogin">微信小程序登录信息</param>
        /// <returns></returns>
        private async Task<ClaimsPrincipal> ServerValidate(ExtensionGrantContext context, WxMpLoginDo wxMpLogin)
        {
            await IdentityOptions.SetAsync();

            var identityUser = await UserWeChatRepository.WeChatMpLoginAsync(wxMpLogin);
            if (identityUser == null) return null;

            ClaimsPrincipal principal = await SignInManager.CreateUserPrincipalAsync(identityUser);
            var scopes = context.Request.GetScopes();
            principal.SetScopes(scopes);
            var resources = await GetResourcesAsync(scopes);
            principal.SetResources(resources);
            return principal;
        }
    }
}