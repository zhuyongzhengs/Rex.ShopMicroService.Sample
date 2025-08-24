using Microsoft.AspNetCore.Authorization;
using Rex.Service.Core.Configurations;
using Rex.Service.Core.Helper;
using Rex.Service.Core.Models;
using Rex.Service.Permission.GoodServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Rex.GoodService.Goods
{
    /// <summary>
    /// 商品参数[模型]服务
    /// </summary>
    [RemoteService]
    [Authorize(GoodServicePermissions.GoodParams.Default)]
    public class GoodParamAppService : CrudAppService<GoodParam, GoodParamDto, Guid, PagedAndSortedResultRequestDto, GoodParamCreateDto, GoodParamUpdateDto>, IGoodParamAppService
    {
        private readonly IGoodParamRepository _goodParamRepository;

        public GoodParamAppService(IGoodParamRepository repository) : base(repository)
        {
            _goodParamRepository = repository;
        }

        /// <summary>
        /// 获取(分页)商品参数[模型]列表
        /// </summary>
        /// <param name="input">查询条件</param>
        /// <returns></returns>
        public async Task<PagedResultDto<GoodParamDto>> GetPageListAsync(GetGoodParamInput input)
        {
            // 查询数量
            long totalCount = (await _goodParamRepository.GetQueryableAsync())
                .WhereIf(!input.Name.IsNullOrWhiteSpace(), p => p.Name.Contains(input.Name))
                .WhereIf(!input.Type.IsNullOrWhiteSpace(), p => p.Type.Equals(input.Type)).LongCount();

            // 查询数据列表
            if (totalCount < 1)
            {
                return new PagedResultDto<GoodParamDto>();
            }
            List<GoodParam> goodParamList = (await _goodParamRepository.GetQueryableAsync())
                    .WhereIf(!input.Name.IsNullOrWhiteSpace(), p => p.Name.Contains(input.Name))
                    .WhereIf(!input.Type.IsNullOrWhiteSpace(), p => p.Type.Equals(input.Type))
                    .OrderByIf<GoodParam, IQueryable<GoodParam>>(!input.Sorting.IsNullOrWhiteSpace(), input.Sorting)
                    .PageBy(input.SkipCount, input.MaxResultCount)
                    .ToList();

            return new PagedResultDto<GoodParamDto>(
                totalCount,
                ObjectMapper.Map<List<GoodParam>, List<GoodParamDto>>(goodParamList)
            );
        }

        /// <summary>
        /// 新增商品参数[模型]
        /// </summary>
        /// <returns></returns>
        [Authorize(GoodServicePermissions.GoodParams.Create)]
        public override async Task<GoodParamDto> CreateAsync(GoodParamCreateDto input)
        {
            GoodParam gParam = await _goodParamRepository.FindAsync(p => p.Name.Equals(input.Name));
            if (gParam != null)
            {
                throw new UserFriendlyException($"参数名称[{input.Name}]已存在，请重新输入！", SystemStatusCode.Fail.ToGoodServicePrefix(), "参数名称唯一，不允许重复。").WithData("Name", input.Name);
            }
            return await base.CreateAsync(input);
        }

        /// <summary>
        /// 修改商品参数[模型]
        /// </summary>
        /// <returns></returns>
        [Authorize(GoodServicePermissions.GoodParams.Update)]
        public override async Task<GoodParamDto> UpdateAsync(Guid id, GoodParamUpdateDto input)
        {
            GoodParam gParam = await _goodParamRepository.FindAsync(p => p.Name.Equals(input.Name) && p.Id != id);
            if (gParam != null)
            {
                throw new UserFriendlyException($"参数名称[{input.Name}]已存在，请重新输入！", SystemStatusCode.Fail.ToGoodServicePrefix(), "参数名称唯一，不允许重复。").WithData("Name", input.Name);
            }
            return await base.UpdateAsync(id, input);
        }

        /// <summary>
        /// 根据ID获取商品参数[模型]信息
        /// </summary>
        /// <param name="ids">商品参数[模型]ID</param>
        /// <returns></returns>
        public async Task<List<GoodParamDto>> GetManyByIdsAsync(List<Guid> ids)
        {
            List<GoodParam> goodParams = await _goodParamRepository.GetListAsync(p => ids.Contains(p.Id));
            return ObjectMapper.Map<List<GoodParam>, List<GoodParamDto>>(goodParams);
        }

        /// <summary>
        /// 查询商品参数类型
        /// </summary>
        /// <returns></returns>
        public async Task<List<EnumEntity>> GetTypesAsync()
        {
            return await Task.FromResult(EnumHelper.EnumToList<GlobalEnums.GoodParamTypes>());
        }
    }
}