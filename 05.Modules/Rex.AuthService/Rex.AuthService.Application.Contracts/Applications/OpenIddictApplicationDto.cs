using System;
using Volo.Abp.Application.Dtos;

namespace Rex.AuthService.Applications;

/// <summary>
/// 应用程序Dto
/// </summary>
public class OpenIddictApplicationDto : EntityDto<Guid>
{
    /// <summary>
    /// 客户端ID
    /// </summary>
    public string ClientId { get; set; }

    /// <summary>
    /// 客户端密钥
    /// </summary>
    public string ClientSecret { get; set; }

    /// <summary>
    /// 同意类型
    /// </summary>
    /// <remarks>
    /// 例如：必需、可选、不需要
    /// </remarks>
    public string ConsentType { get; set; }

    /// <summary>
    /// 显示名称
    /// </summary>
    public string DisplayName { get; set; }

    /// <summary>
    /// 请求的权限列表
    /// </summary>
    /// <remarks>
    /// 它定义了OpenIddict应用程序需要访问的资源和操作
    /// </remarks>
    public string Permissions { get; set; }

    /// <summary>
    /// 退出后重定向到的URI
    /// </summary>
    /// <remarks>
    /// 它指定了用户登出后需要重定向到哪个URI
    /// </remarks>
    public string PostLogoutRedirectUris { get; set; }

    /// <summary>
    /// 扩展属性
    /// </summary>
    public string Properties { get; set; }

    /// <summary>
    /// 重定向到的URI
    /// </summary>
    /// <remarks>
    /// 它指定了用户在完成身份验证后需要重定向到哪个URI
    /// </remarks>
    public string RedirectUris { get; set; }

    /// <summary>
    /// 定义了OpenIddict应用程序需要满足的条件
    /// </summary>
    /// <remarks>
    /// 需要特定的声明或角色
    /// </remarks>
    public string Requirements { get; set; }

    /// <summary>
    /// 应用程序的类型
    /// </summary>
    /// <remarks>
    /// 它指定了OpenIddict应用程序是一个Web应用程序、本机应用程序还是其他类型的应用程序
    /// </remarks>
    public string Type { get; set; }

    /// <summary>
    /// 客户端URI
    /// </summary>
    /// <remarks>
    /// 它可以是OpenIddict应用程序的主页或其他相关页面的URI
    /// </remarks>
    public string ClientUri { get; set; }

    /// <summary>
    /// 应用程序的Logo Uri
    /// </summary>
    public string LogoUri { get; set; }

    /// <summary>
    /// 并发(控制)戳
    /// </summary>
    public string ConcurrencyStamp { get; set; }
}