using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Rex.AuthService.Applications;

/// <summary>
/// 应用程序接口服务
/// </summary>
public interface IOpenIddictApplicationAppService : ICrudAppService<
        OpenIddictApplicationDto,
        Guid,
        PagedAndSortedResultRequestDto,
        OpenIddictApplicationCreateDto,
        OpenIddictApplicationUpdateDto>
{
    /// <summary>
    /// 查询客户端ID
    /// </summary>
    /// <param name="clientId">客户端ID</param>
    /// <returns></returns>
    Task<OpenIddictApplicationDto> GetClientIdAsync(string clientId);

    /// <summary>
    /// 查询退出后的重定向的Uri
    /// </summary>
    /// <param name="address">退出重定向地址</param>
    /// <returns></returns>
    Task<List<OpenIddictApplicationDto>> GetPostLogoutRedirectUrisAsync(string address);

    /// <summary>
    /// 查询重定向地址
    /// </summary>
    /// <param name="address">重定向地址</param>
    /// <returns></returns>
    Task<List<OpenIddictApplicationDto>> GetRedirectUrisAsync(string address);
}