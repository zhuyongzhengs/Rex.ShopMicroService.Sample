using Microsoft.AspNetCore.Authorization;
using Rex.Service.Core.Configurations;
using Rex.Service.Permission.GoodServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Rex.GoodService.Brands
{
    /// <summary>
    /// 品牌服务
    /// </summary>
    [RemoteService]
    [Authorize(GoodServicePermissions.GoodBrands.Default)]
    public class BrandAppService : CrudAppService<Brand, BrandDto, Guid, PagedAndSortedResultRequestDto, BrandCreateDto, BrandUpdateDto>, IBrandAppService
    {
        private readonly IBrandRepository _brandRepository;

        public BrandAppService(IBrandRepository repository) : base(repository)
        {
            _brandRepository = repository;
        }

        /// <summary>
        /// 获取(分页)品牌列表
        /// </summary>
        /// <param name="input">查询条件</param>
        /// <returns></returns>
        public async Task<PagedResultDto<BrandDto>> GetPageListAsync(GetBrandInput input)
        {
            // 查询数量
            long totalCount = (await _brandRepository.GetQueryableAsync())
                .WhereIf(!input.Name.IsNullOrWhiteSpace(), p => p.Name.Contains(input.Name))
                .WhereIf(input.IsShow.HasValue, p => p.IsShow == input.IsShow.Value).LongCount();

            // 查询数据列表
            if (totalCount < 1)
            {
                return new PagedResultDto<BrandDto>();
            }
            List<Brand> brandList = (await _brandRepository.GetQueryableAsync())
                    .WhereIf(!input.Name.IsNullOrWhiteSpace(), p => p.Name.Contains(input.Name))
                    .WhereIf(input.IsShow.HasValue, p => p.IsShow == input.IsShow.Value)
                    .OrderByIf<Brand, IQueryable<Brand>>(!input.Sorting.IsNullOrWhiteSpace(), input.Sorting)
                    .PageBy(input.SkipCount, input.MaxResultCount)
                    .ToList();

            return new PagedResultDto<BrandDto>(
                totalCount,
                ObjectMapper.Map<List<Brand>, List<BrandDto>>(brandList)
            );
        }

        /// <summary>
        /// 新增品牌
        /// </summary>
        /// <returns></returns>
        [Authorize(GoodServicePermissions.GoodBrands.Create)]
        public override async Task<BrandDto> CreateAsync(BrandCreateDto input)
        {
            Brand brand = await _brandRepository.FindAsync(p => p.Name.Equals(input.Name));
            if (brand != null)
            {
                throw new UserFriendlyException($"品牌名称[{input.Name}]已存在，请重新输入！", SystemStatusCode.Fail.ToGoodServicePrefix(), "品牌名称唯一，不允许重复。").WithData("Name", input.Name);
            }
            return await base.CreateAsync(input);
        }

        /// <summary>
        /// 修改品牌
        /// </summary>
        /// <returns></returns>
        [Authorize(GoodServicePermissions.GoodBrands.Update)]
        public override async Task<BrandDto> UpdateAsync(Guid id, BrandUpdateDto input)
        {
            Brand brand = await _brandRepository.FindAsync(p => p.Name.Equals(input.Name) && p.Id != id);
            if (brand != null)
                throw new UserFriendlyException($"品牌名称[{input.Name}]已存在，请重新输入！", SystemStatusCode.Fail.ToGoodServicePrefix(), "品牌名称唯一，不允许重复。").WithData("Name", input.Name);
            return await base.UpdateAsync(id, input);
        }

        /// <summary>
        /// 修改品牌是否显示
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="isShow">是否显示</param>
        /// <returns></returns>
        public async Task UpdateIsShowAsync(Guid id, bool isShow)
        {
            Brand brand = await _brandRepository.GetAsync(id);
            if (brand != null)
            {
                brand.IsShow = isShow;
            }
        }

        /// <summary>
        /// 查询品牌
        /// </summary>
        /// <param name="isShow">是否显示</param>
        /// <returns></returns>
        public async Task<List<BrandDto>> GetShowAsync(bool? isShow = null)
        {
            List<Brand> brands = (await _brandRepository.GetQueryableAsync()).WhereIf(isShow != null, p => p.IsShow == isShow.Value).ToList();
            return ObjectMapper.Map<List<Brand>, List<BrandDto>>(brands);
        }
    }
}