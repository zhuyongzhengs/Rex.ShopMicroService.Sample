using Microsoft.AspNetCore.Authorization;
using Rex.Service.Permission.BaseServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Identity;

namespace Rex.BaseService.Systems.SecurityLogs
{
    /// <summary>
    /// 安全日志服务
    /// </summary>
    [RemoteService]
    [Authorize(BaseServicePermissions.SecurityLogs.Default)]
    public class SecurityLogAppService : ApplicationService, ISecurityLogAppService
    {
        private readonly IIdentitySecurityLogRepository _securityLogRepository;

        public SecurityLogAppService(
            IIdentitySecurityLogRepository securityLogRepository)
        {
            _securityLogRepository = securityLogRepository;
        }

        /// <summary>
        /// 根据ID获取安全日志
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns></returns>
        public async Task<SecurityLogDto> GetAsync(Guid id)
        {
            var securityLog = await _securityLogRepository.GetAsync(id);
            return ObjectMapper.Map<IdentitySecurityLog, SecurityLogDto>(securityLog);
        }

        /// <summary>
        /// 查询安全日志
        /// </summary>
        /// <param name="input">查询条件</param>
        /// <returns></returns>
        public async Task<PagedResultDto<SecurityLogDto>> GetListAsync(GetSecurityLogInput input)
        {
            // 获取数量
            long totalCount = await _securityLogRepository.GetCountAsync(action: input.ActionName, userName: input.UserName, startTime: input.BeginTime, endTime: input.EndTime);

            // 获取安全日志数据
            List<IdentitySecurityLog> securityLogDtoList = await _securityLogRepository.GetListAsync(sorting: input.Sorting, maxResultCount: input.MaxResultCount, skipCount: input.SkipCount,
                userName: input.UserName, action: input.ActionName, startTime: input.BeginTime, endTime: input.EndTime, includeDetails: true);

            return new PagedResultDto<SecurityLogDto>(
                totalCount,
                ObjectMapper.Map<List<IdentitySecurityLog>, List<SecurityLogDto>>(securityLogDtoList)
            );
        }
    }
}