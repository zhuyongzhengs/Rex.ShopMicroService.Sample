using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Rex.PromotionService.Promotions
{
    /// <summary>
    /// 常用促销方法服务接口
    /// </summary>
    public interface IPromotionCommonTagAppService : IApplicationService
    {
        /// <summary>
        /// 获取(分页) 常用促销列表
        /// </summary>
        /// <param name="input">查询条件</param>
        /// <returns></returns>
        Task<PagedResultDto<PromotionDto>> GetListAsync(GetPromotionCommonInput input);
    }
}