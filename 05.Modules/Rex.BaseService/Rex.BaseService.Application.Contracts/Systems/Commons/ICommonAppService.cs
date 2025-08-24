using Rex.BaseService.Systems.SysUsers;
using Rex.Service.Core.Models;
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Rex.BaseService.Systems.Commons
{
    /// <summary>
    /// 公共服务接口
    /// </summary>
    public interface ICommonAppService : IApplicationService
    {
        /// <summary>
        /// 获取当前用户信息
        /// </summary>
        /// <returns></returns>
        Task<CurrentSysUserEto> GetCurrentUserAsync();

        /// <summary>
        /// 更新当前用户信息
        /// </summary>
        /// <returns></returns>
        Task<CurrentSysUserEto> UpdateCurrentUserAsync();

        /// <summary>
        /// 获取(分页)用户积分记录列表
        /// </summary>
        /// <param name="input">查询条件</param>
        /// <returns></returns>
        Task<PagedResultDto<UserPointLogDto>> GetUserPointLogPageListAsync(GetUserPointLogInput input);

        /// <summary>
        /// 修改用户密码
        /// </summary>
        /// <param name="password">密码</param>
        /// <returns></returns>
        Task UpdateSysUserPwdAsync(string password);

        /// <summary>
        /// 修改用户资料信息
        /// </summary>
        /// <param name="input">用户资料</param>
        /// <returns></returns>
        Task UpdateSysUserInformationAsync(SysUserInformationUpdateDto input);
    }
}