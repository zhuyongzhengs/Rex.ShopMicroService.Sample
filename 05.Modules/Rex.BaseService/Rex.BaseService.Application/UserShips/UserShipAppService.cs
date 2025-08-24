using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Rex.BaseService.UserShips
{
    /// <summary>
    /// 用户(收货)地址服务
    /// </summary>
    [RemoteService]
    [Authorize]
    public class UserShipAppService : CrudAppService<UserShip, UserShipDto, Guid, PagedAndSortedResultRequestDto, UserShipCreateDto, UserShipUpdateDto>, IUserShipAppService
    {
        private readonly IUserShipRepository _userShipRepository;

        public UserShipAppService(IUserShipRepository repository) : base(repository)
        {
            _userShipRepository = repository;
        }

        /// <summary>
        /// 查询用户(收货)地址
        /// </summary>
        /// <param name="input">查询条件</param>
        /// <returns></returns>
        public override async Task<PagedResultDto<UserShipDto>> GetListAsync(PagedAndSortedResultRequestDto input)
        {
            long totalCount = (await _userShipRepository.GetQueryableAsync())
                .Where(x => x.UserId == CurrentUser.Id)
                .LongCount();
            if (totalCount < 1)
            {
                return new PagedResultDto<UserShipDto>();
            }

            List<UserShip> userShips = (await _userShipRepository.GetQueryableAsync())
                .OrderByIf<UserShip, IQueryable<UserShip>>(!input.Sorting.IsNullOrWhiteSpace(), input.Sorting)
                .Where(x => x.UserId == CurrentUser.Id).PageBy(input.SkipCount, input.MaxResultCount)
                .ToList();
            return new PagedResultDto<UserShipDto>(
                totalCount,
                ObjectMapper.Map<List<UserShip>, List<UserShipDto>>(userShips)
            );
        }

        /// <summary>
        /// 创建用户(收货)地址
        /// </summary>
        /// <param name="input">创建Dto</param>
        /// <returns></returns>
        public override async Task<UserShipDto> CreateAsync(UserShipCreateDto input)
        {
            if (input.IsDefault)
            {
                UserShip userShip = await _userShipRepository.FindAsync(x => x.UserId == CurrentUser.Id && x.IsDefault);
                if (userShip != null) userShip.IsDefault = false;
            }
            return await base.CreateAsync(input);
        }

        /// <summary>
        /// 修改用户(收货)地址
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="input">修改Dto</param>
        /// <returns></returns>
        public override async Task<UserShipDto> UpdateAsync(Guid id, UserShipUpdateDto input)
        {
            if (input.IsDefault)
            {
                UserShip userShip = await _userShipRepository.FindAsync(x => x.UserId == CurrentUser.Id && x.IsDefault && x.Id != id);
                if (userShip != null) userShip.IsDefault = false;
            }
            return await base.UpdateAsync(id, input);
        }

        /// <summary>
        /// 删除用户(收货)地址
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns></returns>
        public override async Task DeleteAsync(Guid id)
        {
            await base.DeleteAsync(id);
        }

        /// <summary>
        /// 获取默认的用户(收货)地址
        /// </summary>
        /// <returns></returns>
        public async Task<UserShipDto> GetDefaultAsync()
        {
            UserShip userShip = await _userShipRepository.FindAsync(x => x.UserId == CurrentUser.Id && x.IsDefault);
            if (userShip != null) return ObjectMapper.Map<UserShip, UserShipDto>(userShip);
            // 未设置默认收货地址 => 最近添加的收货地址则为默认
            userShip = (await _userShipRepository.GetQueryableAsync()).OrderByDescending(x => x.CreationTime).Where(x => x.UserId == CurrentUser.Id).FirstOrDefault();
            if (userShip != null) return ObjectMapper.Map<UserShip, UserShipDto>(userShip);
            return null;
        }
    }
}