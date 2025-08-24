using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Rex.AuthService.Authorizations;

/// <summary>
/// 授权信息接口服务
/// </summary>
public interface IOpenIddictAuthorizationAppService : ICrudAppService<
        OpenIddictAuthorizationDto,
        Guid,
        PagedAndSortedResultRequestDto,
        OpenIddictAuthorizationCreateDto,
        OpenIddictAuthorizationUpdateDto>
{
    /// <summary>
    /// 获取授权信息
    /// </summary>
    /// <param name="applicationId">应用程序ID</param>
    /// <param name="subject">主题ID</param>
    /// <param name="status">状态</param>
    /// <param name="type">类型</param>
    /// <returns></returns>
    Task<List<OpenIddictAuthorizationDto>> GetAuthorizationsAsync(Guid applicationId, string subject = "", string status = "", string type = "");
}