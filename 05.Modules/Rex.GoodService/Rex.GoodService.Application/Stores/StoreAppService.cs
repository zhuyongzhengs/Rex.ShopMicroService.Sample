using Microsoft.AspNetCore.Authorization;
using Rex.Service.Permission.GoodServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Rex.GoodService.Stores
{
    /// <summary>
    /// 门店服务
    /// </summary>
    [RemoteService]
    [Authorize(GoodServicePermissions.Goods.Default)]
    public class StoreAppService : CrudAppService<Store, StoreDto, Guid, PagedAndSortedResultRequestDto, StoreCreateDto, StoreUpdateDto>, IStoreAppService
    {
        private readonly IStoreRepository _storeRepository;

        public StoreAppService(IStoreRepository repository) : base(repository)
        {
            _storeRepository = repository;
        }

        /// <summary>
        /// 获取(所有)门店信息
        /// </summary>
        /// <param name="storeName">门店名称</param>
        /// <returns></returns>
        public async Task<List<StoreDto>> GetManyAsync(string storeName = "")
        {
            List<Store> storeList = (await _storeRepository.GetQueryableAsync())
                .WhereIf(!storeName.IsNullOrWhiteSpace(), p => p.StoreName.Contains(storeName))
                .ToList();
            return ObjectMapper.Map<List<Store>, List<StoreDto>>(storeList);
        }

        /// <summary>
        /// 获取(分页)门店列表
        /// </summary>
        /// <param name="input">查询条件</param>
        /// <returns></returns>
        public async Task<PagedResultDto<StoreDto>> GetPageListAsync(GetStoreInput input)
        {
            // 查询数量
            long totalCount = (await _storeRepository.GetQueryableAsync())
                .WhereIf(!input.StoreName.IsNullOrWhiteSpace(), p => p.StoreName.Contains(input.StoreName)).LongCount();

            // 查询数据列表
            if (totalCount < 1)
            {
                return new PagedResultDto<StoreDto>();
            }
            List<Store> storeList = (await _storeRepository.GetQueryableAsync())
                    .WhereIf(!input.StoreName.IsNullOrWhiteSpace(), p => p.StoreName.Contains(input.StoreName))
                    .OrderByIf<Store, IQueryable<Store>>(!input.Sorting.IsNullOrWhiteSpace(), input.Sorting)
                    .PageBy(input.SkipCount, input.MaxResultCount)
                    .ToList();

            return new PagedResultDto<StoreDto>(
                totalCount,
                ObjectMapper.Map<List<Store>, List<StoreDto>>(storeList)
            );
        }
    }
}