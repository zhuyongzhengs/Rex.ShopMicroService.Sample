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

namespace Rex.GoodService.ServiceGoods
{
    /// <summary>
    /// 服务商品服务
    /// </summary>
    [RemoteService]
    [Authorize(GoodServicePermissions.ServiceGoods.Default)]
    public class ServiceGoodAppService : CrudAppService<ServiceGood, ServiceGoodDto, Guid, PagedAndSortedResultRequestDto, ServiceGoodCreateDto, ServiceGoodUpdateDto>, IServiceGoodAppService
    {
        private readonly IServiceGoodRepository _serviceGoodRepository;

        public ServiceGoodAppService(IServiceGoodRepository repository) : base(repository)
        {
            _serviceGoodRepository = repository;
        }

        /// <summary>
        /// 获取(分页)服务商品列表
        /// </summary>
        /// <param name="input">查询条件</param>
        /// <returns></returns>
        public async Task<PagedResultDto<ServiceGoodDto>> GetPageListAsync(GetServiceGoodInput input)
        {
            // 查询数量
            long totalCount = (await _serviceGoodRepository.GetQueryableAsync())
                .WhereIf(!input.Title.IsNullOrWhiteSpace(), p => p.Title.Contains(input.Title))
                .WhereIf(input.ValidityType.HasValue, p => p.ValidityType == input.ValidityType.Value)
                .WhereIf(input.Status.HasValue, p => p.Status == input.Status.Value).LongCount();

            // 查询数据列表
            if (totalCount < 1)
            {
                return new PagedResultDto<ServiceGoodDto>();
            }
            List<ServiceGood> serviceGoodList = (await _serviceGoodRepository.GetQueryableAsync())
                    .WhereIf(!input.Title.IsNullOrWhiteSpace(), p => p.Title.Contains(input.Title))
                    .WhereIf(input.ValidityType.HasValue, p => p.ValidityType == input.ValidityType.Value)
                    .WhereIf(input.Status.HasValue, p => p.Status == input.Status.Value)
                    .OrderByIf<ServiceGood, IQueryable<ServiceGood>>(!input.Sorting.IsNullOrWhiteSpace(), input.Sorting)
                    .PageBy(input.SkipCount, input.MaxResultCount)
                    .ToList();

            return new PagedResultDto<ServiceGoodDto>(
                totalCount,
                ObjectMapper.Map<List<ServiceGood>, List<ServiceGoodDto>>(serviceGoodList)
            );
        }

        /// <summary>
        /// 获取项目状态
        /// </summary>
        /// <returns></returns>
        public async Task<List<EnumEntity>> GetStatusAsync()
        {
            return await Task.FromResult<List<EnumEntity>>(EnumHelper.EnumToList<GlobalEnums.ServicesStatu>());
        }

        /// <summary>
        /// 核销有效期类型
        /// </summary>
        /// <returns></returns>
        public async Task<List<EnumEntity>> GetValidityTypesAsync()
        {
            return await Task.FromResult<List<EnumEntity>>(EnumHelper.EnumToList<GlobalEnums.ServicesValidityType>());
        }
    }
}