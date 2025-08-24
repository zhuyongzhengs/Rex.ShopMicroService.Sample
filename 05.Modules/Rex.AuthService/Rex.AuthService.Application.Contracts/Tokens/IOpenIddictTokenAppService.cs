using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Rex.AuthService.Tokens;

/// <summary>
/// 授权Token接口服务
/// </summary>
public interface IOpenIddictTokenAppService : ICrudAppService<
    OpenIddictTokenDto,
    Guid,
    PagedAndSortedResultRequestDto,
    OpenIddictTokenCreateDto,
    OpenIddictTokenUpdateDto>
{
    /// <summary>
    /// 获取授权Token
    /// </summary>
    /// <param name="applicationId">应用程序ID</param>
    /// <param name="subject">主题</param>
    /// <param name="status">状态</param>
    /// <param name="type">类型</param>
    /// <returns></returns>
    Task<List<OpenIddictTokenDto>> GetTokensAsync(Guid applicationId, string subject = "", string status = "", string type = "");
}