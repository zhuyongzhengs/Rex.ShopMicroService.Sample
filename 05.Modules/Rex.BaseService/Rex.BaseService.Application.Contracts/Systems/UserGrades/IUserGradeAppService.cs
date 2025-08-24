using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Rex.BaseService.Systems.UserGrades
{
    /// <summary>
    /// 用户等级服务接口
    /// </summary>
    public interface IUserGradeAppService : ICrudAppService<UserGradeDto, Guid, PagedAndSortedResultRequestDto, UserGradeCreateDto, UserGradeUpdateDto>
    {
        /// <summary>
        /// 查询用户等级
        /// </summary>
        /// <returns></returns>
        Task<List<UserGradeDto>> GetManyAsync();

        /// <summary>
        /// 获取(分页)用户等级列表
        /// </summary>
        /// <param name="input">查询条件</param>
        /// <returns></returns>
        Task<PagedResultDto<UserGradeDto>> GetPageListAsync(GetUserGradeInput input);
    }
}