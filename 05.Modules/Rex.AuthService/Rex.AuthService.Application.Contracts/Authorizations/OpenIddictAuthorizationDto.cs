using System;
using Volo.Abp.Application.Dtos;

namespace Rex.AuthService.Authorizations;

/// <summary>
/// 授权信息Dto
/// </summary>
public class OpenIddictAuthorizationDto : EntityDto<Guid>
{
    /// <summary>
    /// 应用程序ID
    /// </summary>
    public Guid? ApplicationId { get; set; }

    /// <summary>
    /// 授权记录创建日期
    /// </summary>
    public DateTime? CreationDate { get; set; }

    /// <summary>
    /// 可扩展属性
    /// </summary>
    public string Properties { get; set; }

    /// <summary>
    /// 授权的范围（作用域）　
    /// </summary>
    /// <remarks>
    /// 用于定义OpenIddict应用程序可以访问的资源和操作
    /// </remarks>
    public string Scopes { get; set; }

    /// <summary>
    /// 授权记录的状态
    /// </summary>
    public string Status { get; set; }

    /// <summary>
    /// 获取或设置与当前授权关联的主题。
    /// </summary>
    public string Subject { get; set; }

    /// <summary>
    /// 授权类型
    /// </summary>
    /// <remarks>
    /// 指定授权记录是授权码、刷新令牌还是访问令牌
    /// </remarks>
    public string Type { get; set; }
}