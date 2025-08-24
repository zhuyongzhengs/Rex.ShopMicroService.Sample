using DotNetCore.CAP;
using Microsoft.AspNetCore.Authorization;
using Rex.Service.Core.Configurations;
using Rex.Service.Core.Events.Goods;
using Rex.Service.Core.Helper;
using Rex.Service.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Rex.OrderService.Bills
{
    /// <summary>
    /// 退货单服务
    /// </summary>
    [RemoteService]
    [Authorize]
    public class BillReshipAppService : ApplicationService, IBillReshipAppService
    {
        private readonly IBillReshipRepository _billReshipRepository;
        private readonly IBillReshipItemRepository _billReshipItemRepository;
        private readonly ICapPublisher _capEventBus;

        public BillReshipAppService(IBillReshipRepository billReshipRepository, IBillReshipItemRepository billReshipItemRepository, ICapPublisher capEventBus)
        {
            _billReshipRepository = billReshipRepository;
            _billReshipItemRepository = billReshipItemRepository;
            _capEventBus = capEventBus;
        }

        /// <summary>
        /// 获取(分页)退货单列表
        /// </summary>
        /// <param name="input">查询条件</param>
        /// <returns></returns>
        public async Task<PagedResultDto<BillReshipDto>> GetListAsync(GetBillReshipInput input)
        {
            // 查询数量
            long totalCount = await _billReshipRepository.GetPageCountAsync(input.No, input.OrderNo, input.AftersalesNo, input.LogiNo, input.Status, input.BeginTime, input.EndTime);
            if (totalCount < 1) return new PagedResultDto<BillReshipDto>();

            // 查询数据列表
            List<BillReship> billReshipList = await _billReshipRepository.GetPageListAsync(input.SkipCount, input.MaxResultCount, input.Sorting, input.No, input.OrderNo, input.AftersalesNo, input.LogiNo, input.Status, input.BeginTime, input.EndTime);
            return new PagedResultDto<BillReshipDto>(
                totalCount,
                ObjectMapper.Map<List<BillReship>, List<BillReshipDto>>(billReshipList)
            );
        }

        /// <summary>
        /// 查询退货单状态
        /// </summary>
        /// <returns></returns>
        public async Task<List<EnumEntity>> GetStatusAsync()
        {
            return await Task.FromResult(EnumHelper.EnumToList<GlobalEnums.BillReshipStatus>());
        }

        /// <summary>
        /// 售后单生成退货单
        /// </summary>
        /// <param name="input">生成退货单参数</param>
        /// <returns></returns>
        public async Task CreateBillAftersalesToReshipAsync(BillAftersalesToReshipCreateDto input)
        {
            // 退货单
            BillReship billReship = new BillReship();
            billReship.No = CommonHelper.GetSerialNumberType((int)GlobalEnums.SerialNumberType.ReturnCode);
            billReship.OrderId = input.OrderId;
            billReship.AftersalesId = input.AftersalesId;
            billReship.UserId = input.UserId;
            billReship.Status = (int)GlobalEnums.BillReshipStatus.PendingReturn;
            await _billReshipRepository.InsertAsync(billReship);

            // 退货单明细
            List<BillReshipItem> billReshipItems = input.BillAftersalesItems.Select(x => new BillReshipItem()
            {
                ReshipId = billReship.Id,
                OrderItemId = x.OrderItemId,
                GoodId = x.GoodId,
                ProductId = x.ProductId,
                Sn = x.Sn,
                Bn = x.Bn,
                Name = x.Name,
                ImageUrl = x.ImageUrl,
                Nums = x.Nums,
                Addon = x.Addon,
            }).ToList();
            await _billReshipItemRepository.InsertManyAsync(billReshipItems);
        }

        /// <summary>
        /// 根据[售后单]获取退货单
        /// </summary>
        /// <param name="aftersalesId">售后ID</param>
        /// <returns></returns>
        public async Task<BillReshipDto> GetBillReshipByAftersalesIdAsync(Guid aftersalesId)
        {
            BillReship? billReship = (await _billReshipRepository.GetQueryableAsync())
                .Where(x => x.AftersalesId == aftersalesId)
                .FirstOrDefault();
            if (billReship == null) return null;
            return ObjectMapper.Map<BillReship, BillReshipDto>(billReship);
        }

        /// <summary>
        /// 根据[订单ID]获取退货单
        /// </summary>
        /// <param name="orderId">订单ID</param>
        /// <returns></returns>
        public async Task<List<BillReshipDto>> GetOrderIdAsync(Guid orderId)
        {
            List<BillReship> billDeliveries = await _billReshipRepository.GetListAsync(x => x.OrderId == orderId);
            return ObjectMapper.Map<List<BillReship>, List<BillReshipDto>>(billDeliveries);
        }

        /// <summary>
        /// 更新退货单物流信息
        /// </summary>
        /// <param name="id">退货单ID</param>
        /// <param name="input">物流信息</param>
        /// <returns></returns>
        public async Task UpdateLogisticsAsync(Guid id, BillReshipLogiUpdateDto input)
        {
            BillReship billReship = await _billReshipRepository.GetAsync(id);
            if (billReship == null)
                throw new UserFriendlyException("退货单不存在，请检查！", SystemStatusCode.Fail.ToOrderServicePrefix());
            billReship.LogiCode = input.LogiCode;
            billReship.LogiNo = input.LogiNo;
            billReship.Status = (int)GlobalEnums.BillReshipStatus.InTransit;
        }

        /// <summary>
        /// 确认收货
        /// </summary>
        /// <param name="id">退货单ID</param>
        /// <returns></returns>
        public async Task UpdateConfirmDeliveryAsync(Guid id)
        {
            BillReship billReship = await _billReshipRepository.GetBillReshipByIdAsync(id);
            if (billReship == null)
                throw new UserFriendlyException("退货单不存在，请检查！", SystemStatusCode.Fail.ToOrderServicePrefix());
            billReship.Status = (int)GlobalEnums.BillReshipStatus.ReceivedReturn;

            StockLogCreateEto stockLogCreate = new StockLogCreateEto();
            stockLogCreate.StockLogDetails = new List<StockLogDetailEto>();
            foreach (var bReshipItem in billReship.BillReshipItems)
            {
                // 调整库存
                await _capEventBus.PublishAsync(ChangeStockEto.EventName, new ChangeStockEto()
                {
                    ProductId = bReshipItem.ProductId,
                    ChangeStockType = (int)GlobalEnums.OrderChangeStockType.Return,
                    Nums = bReshipItem.Nums
                });

                // 库存记录信息
                stockLogCreate.StockLogDetails.Add(new StockLogDetailEto()
                {
                    SourceId = billReship.Id,
                    ProductId = bReshipItem.ProductId,
                    GoodId = bReshipItem.GoodId,
                    GoodName = bReshipItem.Name,
                    Nums = bReshipItem.Nums,
                    Sn = bReshipItem.Sn,
                    Bn = bReshipItem.Bn,
                    SpesDesc = bReshipItem.Addon,
                });
            }

            // [保存]库存操作日志
            StockCreateEto stockCreate = new StockCreateEto();
            stockCreate.StockDetails = new List<StockDetailEto>();
            StockDetailEto stockDetail = new StockDetailEto();
            stockDetail.OperatorId = CurrentUser.Id.Value;
            stockDetail.SourceId = billReship.Id;
            stockDetail.Type = (int)GlobalEnums.StockType.ReturnedGoods;
            stockDetail.Memo = "退货单[退货]";
            stockCreate.StockDetails.Add(stockDetail);
            await _capEventBus.PublishAsync(StockCreateEto.EventName, stockCreate);

            // [保存]库存记录
            if (stockLogCreate.StockLogDetails.Any())
                await _capEventBus.PublishAsync(StockLogCreateEto.EventName, stockLogCreate);
        }
    }
}