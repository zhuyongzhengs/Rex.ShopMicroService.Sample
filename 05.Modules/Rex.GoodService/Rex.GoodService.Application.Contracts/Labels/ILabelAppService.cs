using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Rex.GoodService.Labels
{
    /// <summary>
    /// 标签服务接口
    /// </summary>
    public interface ILabelAppService : ICrudAppService<LabelDto, Guid, PagedAndSortedResultRequestDto, LabelCreateDto, LabelUpdateDto>
    {
        /// <summary>
        /// 根据标签名称查询标签
        /// </summary>
        /// <param name="names">标签名称</param>
        /// <returns></returns>
        Task<List<LabelDto>> GetGoodLabelByNameAsync(string[] names);

        /// <summary>
        /// 根据标签名称查询标签
        /// </summary>
        /// <param name="ids">ID</param>
        /// <returns></returns>
        Task<List<LabelDto>> GetManyByIdsAsync(Guid[] ids);
    }
}