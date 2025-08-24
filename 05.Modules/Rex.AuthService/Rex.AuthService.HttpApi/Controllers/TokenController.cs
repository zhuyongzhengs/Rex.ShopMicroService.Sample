using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rex.AuthService.Tokens;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Rex.AuthService.Controllers
{
    /// <summary>
    /// Token控制器
    /// </summary>
    [Route("api/auth/token")]
    [Authorize]
    public class TokenController : AuthServiceController
    {
        private readonly IOpenIddictTokenAppService _tokenAppService;

        public TokenController(IOpenIddictTokenAppService tokenAppService)
        {
            _tokenAppService = tokenAppService;
        }

        /// <summary>
        /// 获取授权Token
        /// </summary>
        /// <param name="applicationId">应用程序ID</param>
        /// <param name="subject">主题</param>
        /// <param name="status">状态</param>
        /// <param name="type">类型</param>
        /// <returns></returns>
        [HttpGet("{applicationId}")]
        public async Task<IActionResult> GetTokensAsync([FromRoute] Guid applicationId, string subject = "", string status = "", string type = "")
        {
            List<OpenIddictTokenDto> tokenList = await _tokenAppService.GetTokensAsync(applicationId, subject, status, type);
            return Ok(tokenList);
        }
    }
}