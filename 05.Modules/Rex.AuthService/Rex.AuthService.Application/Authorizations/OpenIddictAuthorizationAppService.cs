using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.OpenIddict.Authorizations;

namespace Rex.AuthService.Authorizations;

/// <summary>
/// 授权信息服务
/// </summary>
[RemoteService(IsEnabled = false)]
[Authorize]
public class OpenIddictAuthorizationAppService : CrudAppService<OpenIddictAuthorization, OpenIddictAuthorizationDto, Guid, PagedAndSortedResultRequestDto, OpenIddictAuthorizationCreateDto, OpenIddictAuthorizationUpdateDto>, IOpenIddictAuthorizationAppService
{
    private readonly IRepository<OpenIddictAuthorization, Guid> _openIddictAuthorizationRepository;

    public OpenIddictAuthorizationAppService(IRepository<OpenIddictAuthorization, Guid> repository) : base(repository)
    {
        _openIddictAuthorizationRepository = repository;
    }

    /// <summary>
    /// 获取授权信息
    /// </summary>
    /// <param name="applicationId">应用程序ID</param>
    /// <param name="subject">主题ID</param>
    /// <param name="status">状态</param>
    /// <param name="type">类型</param>
    /// <returns></returns>
    public async Task<List<OpenIddictAuthorizationDto>> GetAuthorizationsAsync(Guid applicationId, string subject = "", string status = "", string type = "")
    {
        Expression<Func<OpenIddictAuthorization, bool>> authorizationExpression = p => p.ApplicationId == applicationId;
        if (!subject.IsNullOrEmpty())
        {
            authorizationExpression = authorizationExpression.And(p => p.Subject.Equals(subject));
        }
        if (!status.IsNullOrEmpty())
        {
            authorizationExpression = authorizationExpression.And(p => p.Status.Equals(status));
        }
        if (!type.IsNullOrEmpty())
        {
            authorizationExpression = authorizationExpression.And(p => p.Type.Equals(type));
        }
        List<OpenIddictAuthorization> openIddictAuthorizationList = await _openIddictAuthorizationRepository.GetListAsync(authorizationExpression);
        return ObjectMapper.Map<List<OpenIddictAuthorization>, List<OpenIddictAuthorizationDto>>(openIddictAuthorizationList);
    }
}