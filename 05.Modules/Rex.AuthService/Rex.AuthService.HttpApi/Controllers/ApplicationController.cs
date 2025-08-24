using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rex.AuthService.Applications;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Rex.AuthService.Controllers
{
    /// <summary>
    /// 应用程序控制器
    /// </summary>
    [Route("api/auth/application")]
    [Authorize]
    public class ApplicationController : AuthServiceController
    {
        private readonly IOpenIddictApplicationAppService _applicationAppService;

        public ApplicationController(IOpenIddictApplicationAppService applicationAppService)
        {
            _applicationAppService = applicationAppService;
        }

        /// <summary>
        /// 创建应用程序
        /// </summary>
        /// <param name="input">应用程序Dto</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] OpenIddictApplicationCreateDto input)
        {
            OpenIddictApplicationDto openIddictApplication = await _applicationAppService.CreateAsync(input);
            return Ok(openIddictApplication);
        }

        /// <summary>
        /// 修改应用程序
        /// </summary>
        /// <param name="id">应用程序ID</param>
        /// <param name="input">应用程序Dto</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync([FromRoute] Guid id, [FromBody] OpenIddictApplicationUpdateDto input)
        {
            OpenIddictApplicationDto openIddictApplication = await _applicationAppService.UpdateAsync(id, input);
            return Ok(openIddictApplication);
        }

        /// <summary>
        /// 删除应用程序
        /// </summary>
        /// <param name="id">应用程序ID</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] Guid id)
        {
            await _applicationAppService.DeleteAsync(id);
            return NoContent();
        }

        /// <summary>
        /// 查询客户端ID
        /// </summary>
        /// <param name="clientId">客户端ID</param>
        /// <returns></returns>
        [HttpGet("/clientId/{clientId}")]
        public async Task<IActionResult> GetClientIdAsync([FromRoute] string clientId)
        {
            OpenIddictApplicationDto openIddictApplication = await _applicationAppService.GetClientIdAsync(clientId);
            return Ok(openIddictApplication);
        }

        /// <summary>
        /// 查询退出后的重定向的Uri
        /// </summary>
        /// <param name="address">退出重定向地址</param>
        /// <returns></returns>
        [HttpGet("/post-logout-redirect-uri")]
        public async Task<IActionResult> GetPostLogoutRedirectUrisAsync([FromQuery] string address)
        {
            List<OpenIddictApplicationDto> openIddictApplicationList = await _applicationAppService.GetPostLogoutRedirectUrisAsync(address);
            return Ok(openIddictApplicationList);
        }

        /// <summary>
        /// 查询重定向地址
        /// </summary>
        /// <param name="address">重定向地址</param>
        /// <returns></returns>
        [HttpGet("/redirect-uri")]
        public async Task<IActionResult> GetRedirectUrisAsync([FromQuery] string address)
        {
            List<OpenIddictApplicationDto> openIddictApplicationList = await _applicationAppService.GetRedirectUrisAsync(address);
            return Ok(openIddictApplicationList);
        }
    }
}