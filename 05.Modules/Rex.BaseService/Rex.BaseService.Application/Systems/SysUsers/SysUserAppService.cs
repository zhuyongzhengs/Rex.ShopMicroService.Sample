using Microsoft.AspNetCore.Authorization;
using Rex.Service.Permission.BaseServices;
using Rex.Service.Setting.BaseServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.SettingManagement;

namespace Rex.BaseService.Systems.SysUsers
{
    /// <summary>
    /// 系统用户服务
    /// </summary>
    [Authorize]
    [RemoteService]
    public class SysUserAppService : CrudAppService<SysUser, SysUserDto, Guid, PagedAndSortedResultRequestDto, SysUserCreateDto, SysUserUpdateDto>, ISysUserAppService
    {
        /// <summary>
        /// 设置管理
        /// </summary>
        public ISettingManager SettingManager { get; set; }

        private readonly ISysUserRepository _sysUserRepository;

        public SysUserAppService(ISysUserRepository repository) : base(repository)
        {
            _sysUserRepository = repository;
        }

        /// <summary>
        /// 获取(分页) 系统[注册]用户列表
        /// </summary>
        /// <param name="input">查询条件</param>
        /// <returns></returns>
        public async Task<PagedResultDto<SysUserDto>> GetPageListAsync(GetSysUserInput input)
        {
            // 查询数量
            long totalCount = await _sysUserRepository.GetPageCountAsync(input.Name, input.PhoneNumber, input.Gender, input.GradeId, input.IsActive, input.BeginDate, input.EndDate);
            if (totalCount < 1)
            {
                return new PagedResultDto<SysUserDto>();
            }
            // 查询数据列表
            List<SysUser> sysUserList = await _sysUserRepository.GetPageListAsync(input.SkipCount, input.MaxResultCount, input.Sorting, input.Name, input.PhoneNumber, input.Gender, input.GradeId, input.IsActive, input.BeginDate, input.EndDate);
            return new PagedResultDto<SysUserDto>(
                totalCount,
                ObjectMapper.Map<List<SysUser>, List<SysUserDto>>(sysUserList)
            );
        }

        /// <summary>
        /// 创建系统[注册]用户
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [Authorize(BaseServicePermissions.SysUsers.Create)]
        public override async Task<SysUserDto> CreateAsync(SysUserCreateDto input)
        {
            SysUser sysUser = ObjectMapper.Map<SysUserCreateDto, SysUser>(input);
            sysUser.SetNormalized(CurrentTenant.Id);
            sysUser = await _sysUserRepository.InsertAsync(sysUser);
            return ObjectMapper.Map<SysUser, SysUserDto>(sysUser);
        }

        /// <summary>
        /// 根据用户名称/手机号码查询用户信息
        /// </summary>
        /// <param name="userName">用户名称</param>
        /// <param name="phoneNumber">手机号码</param>
        /// <returns></returns>
        public async Task<List<SysUserDto>> GetSysUserByNameOrPhoneAsync(string? userName, string? phoneNumber)
        {
            List<SysUser> sysUserList = (await _sysUserRepository.GetQueryableAsync())
                .WhereIf(!userName.IsNullOrWhiteSpace(), p => p.UserName.Contains(userName))
                .WhereIf(!phoneNumber.IsNullOrWhiteSpace(), p => p.PhoneNumber.Equals(phoneNumber))
                .ToList();
            return ObjectMapper.Map<List<SysUser>, List<SysUserDto>>(sysUserList);
        }

        /// <summary>
        /// 批量获取用户信息
        /// </summary>
        /// <param name="userIds">用户ID</param>
        /// <returns></returns>
        public async Task<List<SysUserDto>> GetManyAsync(Guid[] userIds)
        {
            List<SysUser> sysUserDtos = await _sysUserRepository.GetListAsync(p => userIds.Contains(p.Id));
            return ObjectMapper.Map<List<SysUser>, List<SysUserDto>>(sysUserDtos);
        }

        /// <summary>
        /// 根据用户名查询用户信息
        /// </summary>
        /// <param name="name">用户名称</param>
        /// <returns></returns>
        public async Task<SysUserDto> GetSysUserByNameAsync(string name)
        {
            SysUser sysUser = (await _sysUserRepository.GetQueryableAsync())
                .Where(x => x.UserName.Contains(name) || x.Name.Contains(name))
                .FirstOrDefault();
            if (sysUser == null) return null;
            return ObjectMapper.Map<SysUser, SysUserDto>(sysUser);
        }

        /// <summary>
        /// 获取用户可用积分
        /// </summary>
        /// <param name="orderMoney">订单金额</param>
        /// <returns></returns>
        public async Task<UserPointExchangeDto> GetUserPointExchangeMoneyAsync(decimal orderMoney)
        {
            if (orderMoney <= 0) return null;

            // 是否开启【积分抵扣】
            string pointSwitch = await SettingManager.GetOrNullForCurrentTenantAsync(BaseServiceSettings.PointsSetting.PointSwitch);
            if (!pointSwitch.Equals(true.ToString(), StringComparison.OrdinalIgnoreCase)) return null;

            // 订单积分折现比例(多少积分可以折现1块钱)
            string pointDiscountedProportion = await SettingManager.GetOrNullForCurrentTenantAsync(BaseServiceSettings.PointsSetting.PointDiscountedProportion);
            if (pointDiscountedProportion.IsNullOrWhiteSpace()) return null;
            int.TryParse(pointDiscountedProportion, out int pDiscountedProportion);

            // 订单积分使用比例
            string ordersPointProportion = await SettingManager.GetOrNullForCurrentTenantAsync(BaseServiceSettings.PointsSetting.OrdersPointProportion);
            if (ordersPointProportion.IsNullOrWhiteSpace()) return null;
            int.TryParse(ordersPointProportion, out int oPointProportion);

            UserPointExchangeDto uPointExchangeMoney = new UserPointExchangeDto();
            SysUser sysUser = await _sysUserRepository.GetAsync(CurrentUser.Id.Value);
            if (sysUser == null || sysUser.Point <= 0) return null;
            uPointExchangeMoney.Point = sysUser.Point;

            // 最多可以抵扣多少金额
            decimal maxPointDeductedMoney = Math.Round((orderMoney * oPointProportion) / 100, 2);

            // 计算需要多少积分
            decimal needsPoint = maxPointDeductedMoney * pDiscountedProportion;

            // 可以使用的积分
            uPointExchangeMoney.CanUsePoint = Convert.ToInt32(needsPoint > uPointExchangeMoney.Point ? uPointExchangeMoney.Point : needsPoint);

            // 计算抵扣金额
            uPointExchangeMoney.Money = uPointExchangeMoney.CanUsePoint / pDiscountedProportion;

            return uPointExchangeMoney;
        }
    }
}