using System.Threading.Tasks;
using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Rex.BaseService.Systems.AuditLoggings
{
    /// <summary>
    /// 审计日志服务接口
    /// </summary>
    public interface IAuditLoggingAppService : IApplicationService
    {
        /// <summary>
        /// 根据ID获取审计日志
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns></returns>
        Task<AuditLogDto> GetAsync(Guid id);

        /// <summary>
        /// 查询审计日志
        /// </summary>
        /// <param name="input">查询条件</param>
        /// <returns></returns>
        Task<PagedResultDto<AuditLogDto>> GetListAsync(GetAuditLogInput input);
    }
}