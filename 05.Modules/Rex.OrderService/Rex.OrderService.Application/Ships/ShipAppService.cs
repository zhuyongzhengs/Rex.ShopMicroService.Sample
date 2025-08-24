using Microsoft.AspNetCore.Authorization;
using Rex.Service.Core.Configurations;
using Rex.Service.Core.Helper;
using Rex.Service.Core.Models;
using Rex.Service.Permission.OrderServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Rex.OrderService.Ships
{
    /// <summary>
    /// 配送方式服务
    /// </summary>
    [RemoteService]
    [Authorize(OrderServicePermissions.Ships.Default)]
    public class ShipAppService : CrudAppService<Ship, ShipDto, Guid, PagedAndSortedResultRequestDto, ShipCreateDto, ShipUpdateDto>, IShipAppService
    {
        private readonly IShipRepository _shipRepository;

        public ShipAppService(IShipRepository repository) : base(repository)
        {
            _shipRepository = repository;
        }

        /// <summary>
        /// 获取(分页)配送方式列表
        /// </summary>
        /// <param name="input">查询条件</param>
        /// <returns></returns>
        public async Task<PagedResultDto<ShipDto>> GetPageListAsync(GetShipInput input)
        {
            // 查询数量
            long totalCount = await _shipRepository.GetPageCountAsync(input.Name, input.LogiCode, input.LogiName);
            if (totalCount < 1)
            {
                return new PagedResultDto<ShipDto>();
            }
            // 查询数据列表
            List<Ship> shipList = await _shipRepository.GetPageListAsync(input.SkipCount, input.MaxResultCount, input.Sorting, input.Name, input.LogiCode, input.LogiName);
            return new PagedResultDto<ShipDto>(
                totalCount,
                ObjectMapper.Map<List<Ship>, List<ShipDto>>(shipList)
            );
        }

        /// <summary>
        /// 创建配送方式
        /// </summary>
        /// <param name="input">配送方式Dto</param>
        /// <returns></returns>
        [Authorize(OrderServicePermissions.Ships.Create)]
        public override async Task<ShipDto> CreateAsync(ShipCreateDto input)
        {
            if (input.IsDefault)
            {
                Ship ship = await _shipRepository.FindAsync(x => x.IsDefault);
                if (ship != null) ship.IsDefault = false;
            }
            return await base.CreateAsync(input);
        }

        /// <summary>
        /// 更新配送方式
        /// </summary>
        /// <param name="input">配送方式Dto</param>
        /// <returns></returns>
        [Authorize(OrderServicePermissions.Ships.Update)]
        public override async Task<ShipDto> UpdateAsync(Guid id, ShipUpdateDto input)
        {
            if (input.IsDefault)
            {
                Ship ship = await _shipRepository.FindAsync(x => x.IsDefault && x.Id != id);
                if (ship != null) ship.IsDefault = false;
            }
            return await base.UpdateAsync(id, input);
        }

        /// <summary>
        /// 获取配送方式重量
        /// </summary>
        /// <returns></returns>
        public async Task<List<EnumEntity>> GetShipUnitsAsync()
        {
            return await Task.FromResult(EnumHelper.EnumToList<GlobalEnums.ShipUnit>());
        }

        /// <summary>
        /// 更新默认配送方式
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="isDefault">是否默认</param>
        /// <returns></returns>
        public async Task UpdateIsDefaultAsync(Guid id, bool isDefault)
        {
            if (isDefault)
            {
                Ship preShip = await _shipRepository.FindAsync(x => x.IsDefault && x.Id != id);
                if (preShip != null) preShip.IsDefault = false;
            }
            Ship ship = await _shipRepository.GetAsync(id);
            if (ship != null) ship.IsDefault = isDefault;
        }

        /// <summary>
        /// 更新是否包邮
        /// </summary>
        /// <param name="id"></param>
        /// <param name="isFreePostage">是否包邮</param>
        /// <returns></returns>
        public async Task UpdateIsfreePostageAsync(Guid id, bool isFreePostage)
        {
            Ship ship = await _shipRepository.GetAsync(id);
            if (ship != null)
            {
                ship.IsfreePostage = isFreePostage;
            }
        }
    }
}