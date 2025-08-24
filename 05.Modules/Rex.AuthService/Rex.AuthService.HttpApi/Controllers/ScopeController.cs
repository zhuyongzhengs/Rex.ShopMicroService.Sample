using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rex.AuthService.Scopes;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Rex.AuthService.Controllers
{
    /// <summary>
    /// Scope(作用域)控制器
    /// </summary>
    [Route("api/auth/scope")]
    [Authorize]
    public class ScopeController : AuthServiceController
    {
        private readonly IOpenIddictScopeAppService _scopeAppService;

        public ScopeController(IOpenIddictScopeAppService scopeAppService)
        {
            _scopeAppService = scopeAppService;
        }

        /// <summary>
        /// 创建Scope(作用域)
        /// </summary>
        /// <param name="input">Scope(作用域)Dto</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] OpenIddictScopeCreateDto input)
        {
            OpenIddictScopeDto openIddictScope = await _scopeAppService.CreateAsync(input);
            return Ok(openIddictScope);
        }

        /// <summary>
        /// 修改Scope(作用域)
        /// </summary>
        /// <param name="id">Scope(作用域)ID</param>
        /// <param name="input">Scope(作用域)Dto</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync([FromRoute] Guid id, [FromBody] OpenIddictScopeUpdateDto input)
        {
            await _scopeAppService.UpdateAsync(id, input);
            return NoContent();
        }

        /// <summary>
        /// 删除Scope(作用域)
        /// </summary>
        /// <param name="id">Scope(作用域)ID</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] Guid id)
        {
            await _scopeAppService.DeleteAsync(id);
            return NoContent();
        }

        /// <summary>
        /// 获取作用域(范围)名称
        /// </summary>
        /// <param name="names">名称</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetNamesAsync([FromBody] string[] names)
        {
            List<OpenIddictScopeDto> openIddictScopeList = await _scopeAppService.GetNamesAsync(names);
            return Ok(openIddictScopeList);
        }
    }
}