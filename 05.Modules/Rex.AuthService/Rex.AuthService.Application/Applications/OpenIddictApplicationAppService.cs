using Microsoft.AspNetCore.Authorization;
using OpenIddict.Abstractions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.OpenIddict.Applications;

namespace Rex.AuthService.Applications;

/// <summary>
/// 应用程序服务
/// </summary>
[RemoteService(IsEnabled = false)]
[Authorize]
public class OpenIddictApplicationAppService : CrudAppService<OpenIddictApplication, OpenIddictApplicationDto, Guid, PagedAndSortedResultRequestDto, OpenIddictApplicationCreateDto, OpenIddictApplicationUpdateDto>, IOpenIddictApplicationAppService
{
    private readonly IRepository<OpenIddictApplication, Guid> _openIddictApplicationRepository;
    private readonly IAbpApplicationManager _applicationManager;

    public OpenIddictApplicationAppService(IRepository<OpenIddictApplication, Guid> repository, IAbpApplicationManager applicationManager) : base(repository)
    {
        _openIddictApplicationRepository = repository;
        _applicationManager = applicationManager;
    }

    /// <summary>
    /// 创建应用程序
    /// </summary>
    /// <param name="input">应用程序Dto</param>
    /// <returns></returns>
    public override async Task<OpenIddictApplicationDto> CreateAsync(OpenIddictApplicationCreateDto input)
    {
        if (!string.IsNullOrEmpty(input.ClientSecret) && string.Equals(input.Type, OpenIddictConstants.ClientTypes.Public, StringComparison.OrdinalIgnoreCase))
        {
            // 不能给类型为“Public”的设置密钥
            throw new BusinessException(L["NoClientSecretCanBeSetForPublicApplications"]);
        }

        if (string.IsNullOrEmpty(input.ClientSecret) && string.Equals(input.Type, OpenIddictConstants.ClientTypes.Confidential, StringComparison.OrdinalIgnoreCase))
        {
            // 必须给类型为“Confidential”的设置密钥
            throw new BusinessException(L["TheClientSecretIsRequiredForConfidentialApplications"]);
        }

        // 映射应用程序
        input.ClientSecret = input.ClientSecret.IsNullOrWhiteSpace() ? null : input.ClientSecret.Trim();
        AbpApplicationDescriptor applicationDescriptor = ObjectMapper.Map<OpenIddictApplicationCreateDto, AbpApplicationDescriptor>(input);

        // 保存
        await _applicationManager.CreateAsync(applicationDescriptor);
        OpenIddictApplication openIddictApplication = await _openIddictApplicationRepository.FindAsync(p => p.ClientId == input.ClientId);
        return ObjectMapper.Map<OpenIddictApplication, OpenIddictApplicationDto>(openIddictApplication);
    }

    /// <summary>
    /// 修改应用程序
    /// </summary>
    /// <param name="id">ID</param>
    /// <param name="input">应用程序Dto</param>
    /// <returns></returns>
    public override async Task<OpenIddictApplicationDto> UpdateAsync(Guid id, OpenIddictApplicationUpdateDto input)
    {
        if (!string.IsNullOrEmpty(input.ClientSecret) && string.Equals(input.Type, OpenIddictConstants.ClientTypes.Public, StringComparison.OrdinalIgnoreCase))
        {
            // 不能给类型为“Public”的设置密钥
            throw new BusinessException(L["NoClientSecretCanBeSetForPublicApplications"]);
        }

        if (string.IsNullOrEmpty(input.ClientSecret) && string.Equals(input.Type, OpenIddictConstants.ClientTypes.Confidential, StringComparison.OrdinalIgnoreCase))
        {
            // 必须给类型为“Confidential”的设置密钥
            throw new BusinessException(L["TheClientSecretIsRequiredForConfidentialApplications"]);
        }

        // 映射应用程序
        OpenIddictApplicationModel openIddictApplicationModel = ObjectMapper.Map<OpenIddictApplicationUpdateDto, OpenIddictApplicationModel>(input);
        openIddictApplicationModel.Id = id;

        // 保存
        await _applicationManager.UpdateAsync(openIddictApplicationModel);
        OpenIddictApplication openIddictApplication = await _openIddictApplicationRepository.GetAsync(id);
        return ObjectMapper.Map<OpenIddictApplication, OpenIddictApplicationDto>(openIddictApplication);
    }

    /// <summary>
    /// 查询客户端ID
    /// </summary>
    /// <param name="clientId">客户端ID</param>
    /// <returns></returns>
    public async Task<OpenIddictApplicationDto> GetClientIdAsync(string clientId)
    {
        OpenIddictApplication openIddictApplication = await _openIddictApplicationRepository.FindAsync(p => p.ClientId == clientId);
        return ObjectMapper.Map<OpenIddictApplication, OpenIddictApplicationDto>(openIddictApplication);
    }

    /// <summary>
    /// 查询退出后的重定向的Uri
    /// </summary>
    /// <param name="address">退出重定向地址</param>
    /// <returns></returns>
    public async Task<List<OpenIddictApplicationDto>> GetPostLogoutRedirectUrisAsync(string address)
    {
        List<OpenIddictApplication> openIddictApplicationList = await _openIddictApplicationRepository.GetListAsync(p => p.PostLogoutRedirectUris.Contains(address));
        return ObjectMapper.Map<List<OpenIddictApplication>, List<OpenIddictApplicationDto>>(openIddictApplicationList);
    }

    /// <summary>
    /// 查询重定向地址
    /// </summary>
    /// <param name="address">重定向地址</param>
    /// <returns></returns>
    public async Task<List<OpenIddictApplicationDto>> GetRedirectUrisAsync(string address)
    {
        List<OpenIddictApplication> openIddictApplicationList = await _openIddictApplicationRepository.GetListAsync(p => p.RedirectUris.Contains(address));
        return ObjectMapper.Map<List<OpenIddictApplication>, List<OpenIddictApplicationDto>>(openIddictApplicationList);
    }
}