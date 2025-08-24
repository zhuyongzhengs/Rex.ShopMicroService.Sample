using Microsoft.AspNetCore.Authorization;
using Rex.AreaService.Areas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Rex.GoodService.Areas
{
    /// <summary>
    /// 区域服务
    /// </summary>
    [RemoteService]
    [Authorize]
    public class AreaAppService : CrudAppService<Area, AreaDto, long, PagedAndSortedResultRequestDto, AreaCreateDto, AreaUpdateDto>, IAreaAppService
    {
        private readonly IAreaRepository _areaRepository;

        public AreaAppService(IAreaRepository repository) : base(repository)
        {
            _areaRepository = repository;
        }

        /// <summary>
        /// 获取全部的区域
        /// </summary>
        /// <returns></returns>
        public async Task<List<AreaDto>> GetManyAsync()
        {
            List<Area> areaList = (await _areaRepository.GetQueryableAsync()).ToList();
            return ObjectMapper.Map<List<Area>, List<AreaDto>>(areaList);
        }

        /// <summary>
        /// 获取(分页)区域列表
        /// </summary>
        /// <param name="input">查询条件</param>
        /// <returns></returns>
        public async Task<PagedResultDto<AreaDto>> GetPageListAsync(GetAreaInput input)
        {
            // 查询数量
            long totalCount = await _areaRepository.GetPageCountAsync(input.ParentId, input.Name);
            if (totalCount < 1)
            {
                return new PagedResultDto<AreaDto>();
            }
            // 查询数据列表
            List<Area> areaList = await _areaRepository.GetPageListAsync(input.SkipCount, input.MaxResultCount, input.Sorting, input.ParentId, input.Name);
            return new PagedResultDto<AreaDto>(
                totalCount,
                ObjectMapper.Map<List<Area>, List<AreaDto>>(areaList)
            );
        }

        /// <summary>
        /// 获取(树形)区域
        /// </summary>
        /// <param name="parentId">区域父ID</param>
        /// <param name="depth">深度</param>
        /// <param name="activeId">选中的ID</param>
        /// <returns></returns>
        public async Task<List<AreaTreeDto>> GetTreeAsync(long parentId, int? depth = null, int? activeId = null)
        {
            List<Area> areaList = (await _areaRepository.GetQueryableAsync())
                .Where(x => x.ParentId == parentId)
                .WhereIf(depth.HasValue, p => p.Depth == depth)
                .ToList();
            List<AreaTreeDto> areaTreeList = ObjectMapper.Map<List<Area>, List<AreaTreeDto>>(areaList);
            if (activeId.HasValue && activeId.Value > 0)
            {
                AreaTreeDto activeAreaTree = await GetTreeActiveAsync(activeId.Value);
                int index = areaTreeList.FindIndex(x => x.Id == activeAreaTree.Id);
                if (index != -1)
                {
                    areaTreeList.RemoveAt(index);
                    areaTreeList.Insert(index, activeAreaTree);
                }
            }
            return areaTreeList;
        }

        /// <summary>
        /// 获取选中的区域
        /// </summary>
        /// <param name="id">区域ID</param>
        /// <returns></returns>
        public async Task<AreaTreeDto> GetTreeActiveAsync(long id)
        {
            Area area = await _areaRepository.GetAsync(id);
            if (area == null) return new AreaTreeDto();
            AreaTreeDto currAreaTree = ObjectMapper.Map<Area, AreaTreeDto>(area);
            while (currAreaTree.ParentId.HasValue && currAreaTree.ParentId.Value > 0)
            {
                List<Area> cAreaList = _areaRepository.GetListAsync(x => x.ParentId == currAreaTree.ParentId.Value).Result;
                List<AreaTreeDto> cAreaTreeList = ObjectMapper.Map<List<Area>, List<AreaTreeDto>>(cAreaList);

                // 替换
                int index = cAreaTreeList.FindIndex(x => x.Id == currAreaTree.Id);
                if (index != -1)
                {
                    cAreaTreeList.RemoveAt(index);
                    cAreaTreeList.Insert(index, currAreaTree);
                }

                Area pArea = _areaRepository.GetAsync(currAreaTree.ParentId.Value).Result;
                AreaTreeDto pAreaTree = ObjectMapper.Map<Area, AreaTreeDto>(pArea);
                if (pAreaTree.Children == null) pAreaTree.Children = new List<AreaTreeDto>();
                pAreaTree.Children.AddRange(cAreaTreeList);
                currAreaTree = pAreaTree;
            }
            return currAreaTree;
        }

        #region 查询(树形)区域

        /// <summary>
        /// 获取(树形)区域
        /// </summary>
        /// <returns></returns>
        [Obsolete("此方法已过时，请改用GetTreeAsync(long parentId, int? depth = null)。")]
        public async Task<List<AreaTreeDto>> GetTreeManyAsync()
        {
            List<Area> areaList = (await _areaRepository.GetQueryableAsync()).ToList();
            List<Area> areaRootList = areaList.Where(p => p.ParentId == null || p.ParentId == 0).OrderBy(p => p.Sort).ToList();
            return LoadAreaTree(areaRootList, areaList);
        }

        /// <summary>
        /// 加载树形区域
        /// </summary>
        /// <param name="areaRoot">(根)区域</param>
        /// <param name="areaList">区域</param>
        /// <returns></returns>
        private List<AreaTreeDto> LoadAreaTree(List<Area> areaRoot, List<Area> areaList)
        {
            List<AreaTreeDto> resultAt = new List<AreaTreeDto>();
            foreach (var aRoot in areaRoot)
            {
                AreaTreeDto areaTree = ObjectMapper.Map<Area, AreaTreeDto>(aRoot);
                if (areaList.Where(p => p.ParentId == aRoot.Id).Any())
                {
                    areaTree.Children = LoadAreaTree(areaList.Where(m => m.ParentId == aRoot.Id).OrderBy(o => o.Sort).ToList(), areaList);
                }
                resultAt.Add(areaTree);
            }
            return resultAt;
        }

        /// <summary>
        /// 获取选中的区域ID
        /// </summary>
        /// <param name="id">区域ID</param>
        /// <returns></returns>
        public async Task<long[]> GetTreeActiveIdsAsync(long id)
        {
            Stack<long> areaTreeIds = new Stack<long>();
            Area area = await _areaRepository.GetAsync(id);
            if (area == null) return areaTreeIds.ToArray();
            areaTreeIds.Push(area.Id);
            while (area.ParentId.HasValue && area.ParentId.Value > 0)
            {
                area = _areaRepository.GetAsync(area.ParentId.Value).Result;
                if (area == null) break;
                areaTreeIds.Push(area.Id);
            }
            return areaTreeIds.ToArray();
        }

        #endregion 查询(树形)区域
    }
}