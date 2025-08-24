using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;

namespace Rex.AuthService.Scopes;

/// <summary>
/// 创建作用域(范围)Dto
/// </summary>
public class OpenIddictScopeCreateDto : EntityDto
{
    /// <summary>
    /// 作用域(范围)名称
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// 作用域(范围)显示名称
    /// </summary>
    public string DisplayName { get; set; }

    /// <summary>
    /// 资源信息
    /// </summary>
    /// <remarks>
    /// 当前作用域关联的资源，该资源序列化为JSON数组。
    /// </remarks>
    public List<string> Resources { get; set; }

    /// <summary>
    /// 作用域(范围)描述
    /// </summary>
    public string Description { get; set; }
}