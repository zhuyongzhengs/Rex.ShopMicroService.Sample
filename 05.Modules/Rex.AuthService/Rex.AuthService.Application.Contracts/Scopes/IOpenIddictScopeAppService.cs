using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Rex.AuthService.Scopes;

/// <summary>
/// 作用域(范围)接口服务
/// </summary>
public interface IOpenIddictScopeAppService : ICrudAppService<
    OpenIddictScopeDto,
    Guid,
    PagedAndSortedResultRequestDto,
    OpenIddictScopeCreateDto,
    OpenIddictScopeUpdateDto>
{
    /// <summary>
    /// 获取作用域(范围)名称
    /// </summary>
    /// <param name="names">名称</param>
    /// <returns></returns>
    Task<List<OpenIddictScopeDto>> GetNamesAsync(string[] names);
}