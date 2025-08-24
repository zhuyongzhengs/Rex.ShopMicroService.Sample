using System.Threading.Tasks;
using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Rex.BaseService.Systems.SecurityLogs
{
    /// <summary>
    /// 安全日志服务接口
    /// </summary>
    public interface ISecurityLogAppService : IApplicationService
    {
        /// <summary>
        /// 根据ID获取安全日志
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns></returns>
        Task<SecurityLogDto> GetAsync(Guid id);

        /// <summary>
        /// 查询安全日志
        /// </summary>
        /// <param name="input">查询条件</param>
        /// <returns></returns>
        Task<PagedResultDto<SecurityLogDto>> GetListAsync(GetSecurityLogInput input);
    }
}