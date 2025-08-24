using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Rex.PromotionService.PinTuans
{
    /// <summary>
    /// 拼团记录服务接口
    /// </summary>
    public interface IPinTuanRecordAppService : IApplicationService
    {
        /// <summary>
        /// 获取(分页)拼团规则列表
        /// </summary>
        /// <param name="input">查询条件</param>
        /// <returns></returns>
        Task<PagedResultDto<PinTuanRecordDto>> GetListAsync(GetPinTuanRecordInput input);
    }
}