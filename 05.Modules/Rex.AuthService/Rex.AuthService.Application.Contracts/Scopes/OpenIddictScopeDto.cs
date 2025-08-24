using System;
using Volo.Abp.Application.Dtos;

namespace Rex.AuthService.Scopes;

/// <summary>
/// 作用域(范围)Dto
/// </summary>
public class OpenIddictScopeDto : EntityDto<Guid>
{
    /// <summary>
    /// 作用域(范围)描述
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// 本地化作用域(范围)描述
    /// </summary>
    /// <remarks>
    /// 它允许你为不同的语言提供不同的描述信息
    /// </remarks>
    public string Descriptions { get; set; }

    /// <summary>
    /// 作用域(范围)显示名称
    /// </summary>
    public string DisplayName { get; set; }

    /// <summary>
    /// 本地化作用域(范围)显示名称
    /// </summary>
    /// <remarks>
    /// 它允许你为不同的语言提供不同的显示名称
    /// </remarks>
    public string DisplayNames { get; set; }

    /// <summary>
    /// 作用域(范围)名称
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// 扩展属性
    /// </summary>
    public string Properties { get; set; }

    /// <summary>
    /// 资源信息
    /// </summary>
    /// <remarks>
    /// 当前作用域关联的资源，该资源序列化为JSON数组。
    /// </remarks>
    public string Resources { get; set; }

    /// <summary>
    /// 并发(控制)戳
    /// </summary>
    public string ConcurrencyStamp { get; set; }
}