using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Rex.BaseService.Systems.UserBalances
{
    /// <summary>
    /// 用户余额服务接口
    /// </summary>
    public interface IUserBalanceAppService : IApplicationService
    {
        /// <summary>
        /// 获取(分页)用户余额列表
        /// </summary>
        /// <param name="input">查询条件</param>
        /// <returns></returns>
        Task<PagedResultDto<UserBalanceDto>> GetListAsync(GetUserBalanceInput input);
    }
}