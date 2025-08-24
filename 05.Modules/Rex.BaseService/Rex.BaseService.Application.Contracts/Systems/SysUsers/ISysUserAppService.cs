using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Rex.BaseService.Systems.SysUsers
{
    /// <summary>
    /// 系统(注册)用户服务接口
    /// </summary>
    public interface ISysUserAppService : ICrudAppService<SysUserDto, Guid, PagedAndSortedResultRequestDto, SysUserCreateDto, SysUserUpdateDto>
    {
        /// <summary>
        /// 获取(分页) 系统[注册]用户列表
        /// </summary>
        /// <param name="input">查询条件</param>
        /// <returns></returns>
        Task<PagedResultDto<SysUserDto>> GetPageListAsync(GetSysUserInput input);

        /// <summary>
        /// 批量获取用户信息
        /// </summary>
        /// <param name="userIds">用户ID</param>
        /// <returns></returns>
        Task<List<SysUserDto>> GetManyAsync(Guid[] userIds);

        /// <summary>
        /// 根据用户名称/手机号码查询用户信息
        /// </summary>
        /// <param name="userName">用户名称</param>
        /// <param name="phoneNumber">手机号码</param>
        /// <returns></returns>
        Task<List<SysUserDto>> GetSysUserByNameOrPhoneAsync(string? userName, string? phoneNumber);

        /// <summary>
        /// 根据用户名查询用户信息
        /// </summary>
        /// <param name="name">用户名称</param>
        /// <returns></returns>
        Task<SysUserDto> GetSysUserByNameAsync(string name);

        /// <summary>
        /// 获取用户可用积分
        /// </summary>
        /// <param name="orderMoney">订单金额</param>
        /// <returns></returns>
        Task<UserPointExchangeDto> GetUserPointExchangeMoneyAsync(decimal orderMoney);
    }
}