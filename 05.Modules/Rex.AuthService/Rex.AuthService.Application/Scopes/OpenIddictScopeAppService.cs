using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.OpenIddict.Scopes;

namespace Rex.AuthService.Scopes;

/// <summary>
/// 作用域(范围)服务
/// </summary>
[RemoteService(IsEnabled = false)]
[Authorize]
public class OpenIddictScopeAppService : CrudAppService<OpenIddictScope, OpenIddictScopeDto, Guid, PagedAndSortedResultRequestDto, OpenIddictScopeCreateDto, OpenIddictScopeUpdateDto>, IOpenIddictScopeAppService
{
    private readonly IRepository<OpenIddictScope, Guid> _openIddictScopeRepository;

    public OpenIddictScopeAppService(IRepository<OpenIddictScope, Guid> repository) : base(repository)
    {
        _openIddictScopeRepository = repository;
    }

    /// <summary>
    /// 创建授权Scope
    /// </summary>
    /// <param name="input">授权ScopeDto</param>
    /// <returns></returns>
    public override async Task<OpenIddictScopeDto> CreateAsync(OpenIddictScopeCreateDto input)
    {
        return await base.CreateAsync(input);
    }

    /// <summary>
    /// 修改授权Scope
    /// </summary>
    /// <param name="id">修改ID</param>
    /// <param name="input">授权ScopeDto</param>
    /// <returns></returns>
    public override async Task<OpenIddictScopeDto> UpdateAsync(Guid id, OpenIddictScopeUpdateDto input)
    {
        return await base.UpdateAsync(id, input);
    }

    /// <summary>
    /// 获取作用域(范围)名称
    /// </summary>
    /// <param name="names">名称</param>
    /// <returns></returns>
    public async Task<List<OpenIddictScopeDto>> GetNamesAsync(string[] names)
    {
        List<OpenIddictScope> openIddictScopeList = await _openIddictScopeRepository.GetListAsync(p => names.Contains(p.Name));
        return ObjectMapper.Map<List<OpenIddictScope>, List<OpenIddictScopeDto>>(openIddictScopeList);
    }
}