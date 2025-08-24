using Microsoft.AspNetCore.Mvc;
using Rex.BaseService.Systems.SysUsers;
using Rex.GoodService.StoreClerks;
using Rex.Service.Core.Configurations;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace Rex.BackendStoreClerkAggregationService.Controllers
{
    /// <summary>
    /// 店铺店员关联控制器
    /// </summary>
    [Route("api/backend/aggregation/storeClerk")]
    [ApiController]
    public class StoreClerkController : ControllerBase
    {
        private readonly IStoreClerkAppService _storeClerkAppService;
        private readonly ISysUserAppService _sysUserAppService;

        public StoreClerkController(IStoreClerkAppService storeClerkAppService, ISysUserAppService sysUserAppService)
        {
            _storeClerkAppService = storeClerkAppService;
            _sysUserAppService = sysUserAppService;
        }

        /// <summary>
        /// 获取店铺店员关联信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetPageListAsync([FromQuery] GetStoreClerkInput input)
        {
            if (!string.IsNullOrWhiteSpace(input.UserName) || !string.IsNullOrWhiteSpace(input.PhoneNumber))
            {
                List<SysUserDto> sysUserList = await _sysUserAppService.GetSysUserByNameOrPhoneAsync(input.UserName, input.PhoneNumber);
                if (sysUserList.Count < 1) return Ok(new PagedResultDto<StoreClerkDto>());
                input.UserIds = sysUserList.Select(p => p.Id).ToArray().JoinAsString(",");
            }
            PagedResultDto<StoreClerkDto> storeClerks = await _storeClerkAppService.GetPageListAsync(input);
            Guid[] storeUserIds = storeClerks.Items.Select(p => p.UserId).ToArray();
            if (storeUserIds.Length > 0)
            {
                List<SysUserDto> sysUserList = await _sysUserAppService.GetManyAsync(storeUserIds);
                foreach (var storeClerk in storeClerks.Items)
                {
                    SysUserDto sysUser = sysUserList.Where(p => p.Id == storeClerk.UserId).FirstOrDefault();
                    if (sysUser == null) continue;
                    storeClerk.Avatar = sysUser.Avatar;
                    storeClerk.UserName = sysUser.UserName;
                    storeClerk.PhoneNumber = sysUser.PhoneNumber;
                }
            }
            return Ok(storeClerks);
        }

        /// <summary>
        /// 创建店员关联用户
        /// </summary>
        /// <param name="input">店员关联</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] StoreClerkCreateDto input)
        {
            List<SysUserDto> sysUserList = await _sysUserAppService.GetSysUserByNameOrPhoneAsync(null, input.PhoneNumber);
            if (!sysUserList.Any())
            {
                throw new UserFriendlyException($"不存在该手机号[{input.PhoneNumber}]的注册用户！", SystemStatusCode.Fail.ToBaseServicePrefix());
            }
            input.UserId = sysUserList.FirstOrDefault().Id;
            StoreClerkDto storeClerkDto = await _storeClerkAppService.CreateAsync(input);
            return Ok(storeClerkDto);
        }

        /// <summary>
        /// 修改店员关联用户
        /// </summary>
        /// <param name="id">关联ID</param>
        /// <param name="input">店员关联</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync([FromRoute] Guid id, [FromBody] StoreClerkUpdateDto input)
        {
            List<SysUserDto> sysUserList = await _sysUserAppService.GetSysUserByNameOrPhoneAsync(null, input.PhoneNumber);
            if (!sysUserList.Any())
            {
                throw new UserFriendlyException($"不存在该手机号[{input.PhoneNumber}]的注册用户！", SystemStatusCode.Fail.ToBaseServicePrefix());
            }
            input.UserId = sysUserList.FirstOrDefault().Id;
            StoreClerkDto storeClerkDto = await _storeClerkAppService.UpdateAsync(id, input);
            return Ok(storeClerkDto);
        }
    }
}