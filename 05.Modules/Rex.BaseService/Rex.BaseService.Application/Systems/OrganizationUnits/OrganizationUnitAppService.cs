using Microsoft.AspNetCore.Authorization;
using Rex.Service.Permission.BaseServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Services;
using Volo.Abp.Identity;

namespace Rex.BaseService.Systems.OrganizationUnits
{
    /// <summary>
    /// 组织单元服务
    /// </summary>
    [RemoteService]
    [Authorize(BaseServicePermissions.OrganizationUnits.Default)]
    public class OrganizationUnitAppService : ApplicationService, IOrganizationUnitAppService
    {
        public OrganizationUnitManager OrganizationUnitManagerRepository { get; set; }
        private readonly IOrganizationUnitRepository _organizationUnitRepository;

        public OrganizationUnitAppService(IOrganizationUnitRepository repository)
        {
            _organizationUnitRepository = repository;
        }

        /// <summary>
        /// 获取组织单元列表
        /// </summary>
        /// <returns></returns>
        public async Task<List<OrganizationUnitDto>> GetListAsync(string sorting = null, int maxResultCount = 1000, int skipCount = 0, bool includeDetails = false)
        {
            List<OrganizationUnit> organizationUnitList = await _organizationUnitRepository.GetListAsync(sorting, maxResultCount, skipCount, includeDetails);
            return ObjectMapper.Map<List<OrganizationUnit>, List<OrganizationUnitDto>>(organizationUnitList);
        }

        /// <summary>
        /// 创建组织单元
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [Authorize(BaseServicePermissions.OrganizationUnits.Create)]
        public async Task CreateAsync(OrganizationUnitCreateDto input)
        {
            OrganizationUnit organizationUnit = ObjectMapper.Map<OrganizationUnitCreateDto, OrganizationUnit>(input);
            await OrganizationUnitManagerRepository.CreateAsync(organizationUnit);
        }

        /// <summary>
        /// 修改组织单元
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [Authorize(BaseServicePermissions.OrganizationUnits.Update)]
        public async Task UpdateAsync(Guid id, OrganizationUnitUpdateDto input)
        {
            OrganizationUnit ou = await _organizationUnitRepository.GetAsync(id);
            if (ou == null)
            {
                throw new BusinessException("error", $"修改的数据[{id}]已不存在！！");
            }
            if (ou.ConcurrencyStamp != input.ConcurrencyStamp)
            {
                throw new BusinessException("warn", "数据已被更改！");
            }
            ou.DisplayName = input.DisplayName;
        }

        /// <summary>
        /// 删除组织单元
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns></returns>
        [Authorize(BaseServicePermissions.OrganizationUnits.Delete)]
        public async Task DeleteAsync(Guid id)
        {
            await OrganizationUnitManagerRepository.DeleteAsync(id);
        }

        /// <summary>
        /// 获取组织单元（Tree）列表
        /// </summary>
        /// <returns></returns>
        public async Task<List<OrganizationUnitTreeDto>> GetTreeAsync(bool includeDetails = false)
        {
            List<OrganizationUnit> organizationUnitList = await _organizationUnitRepository.GetListAsync(includeDetails: includeDetails);
            List<OrganizationUnit> organizationUnitRootList = organizationUnitList.Where(p => p.ParentId == null).OrderBy(p => p.Code).ToList();
            return LoadOrganizationUnitTree(organizationUnitRootList, organizationUnitList);
        }

        /// <summary>
        /// 加载树形组织机构
        /// </summary>
        /// <param name="roots">(根)组织机构</param>
        /// <param name="menus">组织机构</param>
        /// <returns></returns>
        private List<OrganizationUnitTreeDto> LoadOrganizationUnitTree(List<OrganizationUnit> ouRoot, List<OrganizationUnit> ouList)
        {
            List<OrganizationUnitTreeDto> resultOut = new List<OrganizationUnitTreeDto>();
            foreach (var oRoot in ouRoot)
            {
                OrganizationUnitTreeDto menuTree = ObjectMapper.Map<OrganizationUnit, OrganizationUnitTreeDto>(oRoot);
                if (ouList.Where(p => p.ParentId == oRoot.Id).Any())
                {
                    menuTree.Children = LoadOrganizationUnitTree(ouList.Where(m => m.ParentId == oRoot.Id).OrderBy(o => o.Code).ToList(), ouList);
                }
                resultOut.Add(menuTree);
            }
            return resultOut;
        }
    }
}