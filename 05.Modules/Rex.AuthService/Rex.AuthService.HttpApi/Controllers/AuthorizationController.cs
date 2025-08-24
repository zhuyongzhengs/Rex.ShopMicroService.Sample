using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rex.AuthService.Authorizations;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Rex.AuthService.Controllers
{
    /// <summary>
    /// 授权控制器
    /// </summary>
    [Route("api/auth/authorization")]
    [Authorize]
    public class AuthorizationController : AuthServiceController
    {
        private readonly IOpenIddictAuthorizationAppService _authorizationAppService;

        public AuthorizationController(IOpenIddictAuthorizationAppService authorizationAppService)
        {
            _authorizationAppService = authorizationAppService;
        }

        /// <summary>
        /// 获取授权信息
        /// </summary>
        /// <param name="applicationId">应用程序ID</param>
        /// <param name="subject">主题ID</param>
        /// <param name="status">状态</param>
        /// <param name="type">类型</param>
        /// <returns></returns>
        [HttpGet("{applicationId}")]
        public async Task<IActionResult> GetAuthorizationsAsync([FromRoute] Guid applicationId, string subject = "", string status = "", string type = "")
        {
            List<OpenIddictAuthorizationDto> authorizations = await _authorizationAppService.GetAuthorizationsAsync(applicationId, subject, status, type);
            return Ok(authorizations);
        }
    }
}