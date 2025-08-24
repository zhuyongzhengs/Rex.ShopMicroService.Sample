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
using Volo.Abp.OpenIddict.Tokens;

namespace Rex.AuthService.Tokens;

/// <summary>
/// 授权Token服务
/// </summary>
[RemoteService(IsEnabled = false)]
[Authorize]
public class OpenIddictTokenAppService : CrudAppService<OpenIddictToken, OpenIddictTokenDto, Guid, PagedAndSortedResultRequestDto, OpenIddictTokenCreateDto, OpenIddictTokenUpdateDto>, IOpenIddictTokenAppService
{
    public IOpenIddictTokenRepository OpenIddictTokenRepository { get; set; }
    private readonly IRepository<OpenIddictToken, Guid> _openIddictTokenRepository;

    public OpenIddictTokenAppService(IRepository<OpenIddictToken, Guid> repository) : base(repository)
    {
        _openIddictTokenRepository = repository;
    }

    /// <summary>
    /// 获取授权Token
    /// </summary>
    /// <param name="applicationId">应用程序ID</param>
    /// <param name="subject">主题ID</param>
    /// <param name="status">状态</param>
    /// <param name="type">类型</param>
    /// <returns></returns>
    public async Task<List<OpenIddictTokenDto>> GetTokensAsync(Guid applicationId, string subject = "", string status = "", string type = "")
    {
        Expression<Func<OpenIddictToken, bool>> authorizationExpression = p => p.ApplicationId == applicationId;
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
        List<OpenIddictToken> openIddictTokenList = await _openIddictTokenRepository.GetListAsync(authorizationExpression);
        return ObjectMapper.Map<List<OpenIddictToken>, List<OpenIddictTokenDto>>(openIddictTokenList);
    }
}