using Microsoft.AspNetCore.Authorization;
using Rex.Service.Permission.BaseServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.AuditLogging;

namespace Rex.BaseService.Systems.AuditLoggings
{
    /// <summary>
    /// 审计日志服务
    /// </summary>
    [RemoteService]
    [Authorize(BaseServicePermissions.AuditLoggings.Default)]
    public class AuditLoggingAppService : ApplicationService, IAuditLoggingAppService
    {
        private readonly IAuditLogRepository _auditLogRepository;

        public AuditLoggingAppService(
            IAuditLogRepository auditLogRepository)
        {
            _auditLogRepository = auditLogRepository;
        }

        /// <summary>
        /// 根据ID获取审计日志
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns></returns>
        public async Task<AuditLogDto> GetAsync(Guid id)
        {
            var auditLog = await _auditLogRepository.GetAsync(id);
            return ObjectMapper.Map<AuditLog, AuditLogDto>(auditLog);
        }

        /// <summary>
        /// 查询审计日志
        /// </summary>
        /// <param name="input">查询条件</param>
        /// <returns></returns>
        public async Task<PagedResultDto<AuditLogDto>> GetListAsync(GetAuditLogInput input)
        {
            // 获取数量
            long totalCount = await _auditLogRepository.GetCountAsync(httpMethod: input.HttpMethod, url: input.Url,
                userName: input.UserName, applicationName: input.ApplicationName, correlationId: input.CorrelationId, maxExecutionDuration: input.MaxExecutionDuration,
                minExecutionDuration: input.MinExecutionDuration, hasException: input.HasException, httpStatusCode: input.HttpStatusCode);

            // 获取审计日志数据
            List<AuditLog> auditLogDtoList = await _auditLogRepository.GetListAsync(sorting: input.Sorting, maxResultCount: input.MaxResultCount, skipCount: input.SkipCount, httpMethod: input.HttpMethod, url: input.Url,
                userName: input.UserName, applicationName: input.ApplicationName, correlationId: input.CorrelationId, maxExecutionDuration: input.MaxExecutionDuration,
                minExecutionDuration: input.MinExecutionDuration, hasException: input.HasException, httpStatusCode: input.HttpStatusCode, includeDetails: true);

            return new PagedResultDto<AuditLogDto>(
                totalCount,
                ObjectMapper.Map<List<AuditLog>, List<AuditLogDto>>(auditLogDtoList)
            );
        }
    }
}