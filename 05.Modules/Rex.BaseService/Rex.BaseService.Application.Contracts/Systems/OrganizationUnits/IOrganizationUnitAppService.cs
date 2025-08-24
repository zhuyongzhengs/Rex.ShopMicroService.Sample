using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Rex.BaseService.Systems.OrganizationUnits
{
    /// <summary>
    /// 组织单元服务接口
    /// </summary>
    public interface IOrganizationUnitAppService : IApplicationService
    {
        /// <summary>
        /// 获取组织单元列表
        /// </summary>
        /// <returns></returns>
        Task<List<OrganizationUnitDto>> GetListAsync(
            string sorting = null,
            int maxResultCount = 1000,
            int skipCount = 0,
            bool includeDetails = false
        );

        /// <summary>
        /// 获取组织单元（Tree）列表
        /// </summary>
        /// <param name="includeDetails"></param>
        /// <returns></returns>
        Task<List<OrganizationUnitTreeDto>> GetTreeAsync(bool includeDetails = false);

        /// <summary>
        /// 创建组织单元
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task CreateAsync(OrganizationUnitCreateDto input);

        /// <summary>
        /// 修改组织单元
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        Task UpdateAsync(Guid id, OrganizationUnitUpdateDto input);

        /// <summary>
        /// 删除组织单元
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns></returns>
        Task DeleteAsync(Guid id);
    }
}