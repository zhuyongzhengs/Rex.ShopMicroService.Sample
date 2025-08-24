using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Rex.BaseService.Systems.UserBalances
{
    /// <summary>
    /// 用户余额服务
    /// </summary>
    [RemoteService]
    [Authorize]
    public class UserBalanceAppService : ApplicationService, IUserBalanceAppService
    {
        private readonly IUserBalanceRepository _userBalanceRepository;

        public UserBalanceAppService(IUserBalanceRepository repository)
        {
            _userBalanceRepository = repository;
        }

        /// <summary>
        /// 获取(分页)用户余额列表
        /// </summary>
        /// <param name="input">查询条件</param>
        /// <returns></returns>
        public async Task<PagedResultDto<UserBalanceDto>> GetListAsync(GetUserBalanceInput input)
        {
            // 查询数量
            long totalCount = (await _userBalanceRepository.GetQueryableAsync())
                .Where(p => p.UserId == CurrentUser.Id)
                .WhereIf(input.Type.HasValue, p => p.Type == input.Type)
                .LongCount();

            // 查询数据列表
            if (totalCount < 1) return new PagedResultDto<UserBalanceDto>();
            List<UserBalance> userBalanceList = (await _userBalanceRepository.GetQueryableAsync())
                .Where(p => p.UserId == CurrentUser.Id)
                .WhereIf(input.Type.HasValue, p => p.Type == input.Type)
                    .OrderByIf<UserBalance, IQueryable<UserBalance>>(!input.Sorting.IsNullOrWhiteSpace(), input.Sorting)
                    .PageBy(input.SkipCount, input.MaxResultCount)
                    .ToList();

            return new PagedResultDto<UserBalanceDto>(
                totalCount,
                ObjectMapper.Map<List<UserBalance>, List<UserBalanceDto>>(userBalanceList)
            );
        }
    }
}