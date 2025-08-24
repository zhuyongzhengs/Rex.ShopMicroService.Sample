using Microsoft.AspNetCore.Authorization;
using Rex.Service.Core.Configurations;
using Rex.Service.Permission.GoodServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Rex.GoodService.StoreClerks
{
    /// <summary>
    /// 店铺店员关联服务
    /// </summary>
    [RemoteService]
    [Authorize(GoodServicePermissions.Goods.Default)]
    public class StoreClerkAppService : CrudAppService<StoreClerk, StoreClerkDto, Guid, PagedAndSortedResultRequestDto, StoreClerkCreateDto, StoreClerkUpdateDto>, IStoreClerkAppService
    {
        private readonly IStoreClerkRepository _storeClerkRepository;

        public StoreClerkAppService(IStoreClerkRepository repository) : base(repository)
        {
            _storeClerkRepository = repository;
        }

        /// <summary>
        /// 获取(分页)店铺店员关联列表
        /// </summary>
        /// <param name="input">查询条件</param>
        /// <returns></returns>
        public async Task<PagedResultDto<StoreClerkDto>> GetPageListAsync(GetStoreClerkInput input)
        {
            // 查询数量
            long totalCount = await _storeClerkRepository.GetPageCountAsync(input.StoreId, input.UserIds);
            if (totalCount < 1)
            {
                return new PagedResultDto<StoreClerkDto>();
            }
            // 查询数据列表
            List<StoreClerk> storeClerkList = await _storeClerkRepository.GetPageListAsync(input.SkipCount, input.MaxResultCount, input.Sorting, input.StoreId, input.UserIds);
            return new PagedResultDto<StoreClerkDto>(
                totalCount,
                ObjectMapper.Map<List<StoreClerk>, List<StoreClerkDto>>(storeClerkList)
            );
        }

        /// <summary>
        /// 创建店员关联用户
        /// </summary>
        /// <param name="input">店员关联</param>
        /// <returns></returns>
        public override async Task<StoreClerkDto> CreateAsync(StoreClerkCreateDto input)
        {
            StoreClerk storeClerk = await _storeClerkRepository.FindAsync(p => p.StoreId == input.StoreId && p.UserId == input.UserId);
            if (storeClerk != null)
                throw new UserFriendlyException($"该用户已经是当前门店店员！", SystemStatusCode.Fail.ToGoodServicePrefix());
            return await base.CreateAsync(input);
        }

        /// <summary>
        /// 修改店员关联用户
        /// </summary>
        /// <param name="id">关联ID</param>
        /// <param name="input">店员关联</param>
        /// <returns></returns>
        public override async Task<StoreClerkDto> UpdateAsync(Guid id, StoreClerkUpdateDto input)
        {
            StoreClerk storeClerk = await _storeClerkRepository.FindAsync(p => p.Id != id && p.StoreId == input.StoreId && p.UserId == input.UserId);
            if (storeClerk != null)
                throw new UserFriendlyException($"该用户已经是当前门店店员！", SystemStatusCode.Fail.ToGoodServicePrefix());
            return await base.UpdateAsync(id, input);
        }

        /// <summary>
        /// 删除店员信息
        /// </summary>
        /// <param name="ids">ID</param>
        /// <returns></returns>
        public async Task DeleteManyAsync(List<Guid> ids)
        {
            await _storeClerkRepository.DeleteManyAsync(ids);
        }
    }
}