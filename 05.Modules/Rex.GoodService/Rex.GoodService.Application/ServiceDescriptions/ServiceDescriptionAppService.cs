using Microsoft.AspNetCore.Authorization;
using Rex.GoodService.Commons;
using Rex.Service.Core.Configurations;
using Rex.Service.Core.Helper;
using Rex.Service.Core.Models;
using Rex.Service.Permission.GoodServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Caching;

namespace Rex.GoodService.ServiceDescriptions
{
    /// <summary>
    /// 商城服务描述服务
    /// </summary>
    [RemoteService]
    [Authorize(GoodServicePermissions.Goods.Default)]
    public class ServiceDescriptionAppService : CrudAppService<ServiceDescription, ServiceDescriptionDto, Guid, PagedAndSortedResultRequestDto, ServiceDescriptionCreateDto, ServiceDescriptionUpdateDto>, IServiceDescriptionAppService
    {
        private readonly IServiceDescriptionRepository _serviceDescriptionRepository;

        public IDistributedCache<List<ServiceDescriptionDto>> ServiceDescriptionDistributedCache { get; set; }
        public ICommonAppService CommonAppService { get; set; }

        public ServiceDescriptionAppService(IServiceDescriptionRepository repository) : base(repository)
        {
            _serviceDescriptionRepository = repository;
        }

        /// <summary>
        /// 获取(分页)商城服务描述服务列表
        /// </summary>
        /// <param name="input">查询条件</param>
        /// <returns></returns>
        public async Task<PagedResultDto<ServiceDescriptionDto>> GetPageListAsync(GetServiceDescriptionInput input)
        {
            // 查询数量
            long totalCount = (await _serviceDescriptionRepository.GetQueryableAsync())
                .WhereIf(!input.Title.IsNullOrWhiteSpace(), p => p.Title.Contains(input.Title))
                .WhereIf(input.Type.HasValue, p => p.Type == input.Type)
                .WhereIf(input.IsShow.HasValue, p => p.IsShow == input.IsShow)
                .LongCount();
            if (totalCount < 1)
            {
                return new PagedResultDto<ServiceDescriptionDto>();
            }

            // 查询数据列表
            List<ServiceDescription> serviceDescriptionList = (await _serviceDescriptionRepository.GetQueryableAsync())
                    .WhereIf(!input.Title.IsNullOrWhiteSpace(), p => p.Title.Contains(input.Title))
                    .WhereIf(input.Type.HasValue, p => p.Type == input.Type)
                    .WhereIf(input.IsShow.HasValue, p => p.IsShow == input.IsShow)
                    .OrderByIf<ServiceDescription, IQueryable<ServiceDescription>>(!input.Sorting.IsNullOrWhiteSpace(), input.Sorting)
                    .PageBy(input.SkipCount, input.MaxResultCount)
                    .ToList();

            return new PagedResultDto<ServiceDescriptionDto>(
                totalCount,
                ObjectMapper.Map<List<ServiceDescription>, List<ServiceDescriptionDto>>(serviceDescriptionList)
            );
        }

        /// <summary>
        /// 查询商城服务描述类型
        /// </summary>
        /// <returns></returns>
        public async Task<List<EnumEntity>> GetNoteTypesAsync()
        {
            return await Task.FromResult(EnumHelper.EnumToList<GlobalEnums.ShopServiceNoteType>());
        }

        /// <summary>
        /// 创建商城服务描述
        /// </summary>
        /// <param name="input">创建Dto</param>
        /// <returns></returns>
        public override async Task<ServiceDescriptionDto> CreateAsync(ServiceDescriptionCreateDto input)
        {
            ServiceDescriptionDto serviceDescriptionDto = await base.CreateAsync(input);
            await CommonAppService.UpdateServiceDescriptionCacheAsync();
            return serviceDescriptionDto;
        }

        /// <summary>
        /// 修改商城服务描述
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="input">修改Dto</param>
        /// <returns></returns>
        public override async Task<ServiceDescriptionDto> UpdateAsync(Guid id, ServiceDescriptionUpdateDto input)
        {
            ServiceDescriptionDto serviceDescriptionDto = await base.UpdateAsync(id, input);
            await CommonAppService.UpdateServiceDescriptionCacheAsync();
            return serviceDescriptionDto;
        }

        /// <summary>
        /// 修改商城服务描述是否显示
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="isShow">是否显示</param>
        /// <returns></returns>
        public async Task UpdateIsShowAsync(Guid id, bool isShow)
        {
            ServiceDescription serviceDescription = await _serviceDescriptionRepository.GetAsync(id);
            if (serviceDescription != null)
            {
                serviceDescription.IsShow = isShow;
                await CommonAppService.UpdateServiceDescriptionCacheAsync();
            }
        }

        /// <summary>
        /// 删除商城服务描述
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns></returns>
        public override async Task DeleteAsync(Guid id)
        {
            await base.DeleteAsync(id);
            await CommonAppService.UpdateServiceDescriptionCacheAsync();
        }
    }
}