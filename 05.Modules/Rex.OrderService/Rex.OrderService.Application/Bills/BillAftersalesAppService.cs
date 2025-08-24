using DotNetCore.CAP;
using Microsoft.AspNetCore.Authorization;
using Rex.OrderService.Orders;
using Rex.Service.Core.Configurations;
using Rex.Service.Core.Events.Bases;
using Rex.Service.Core.Events.Goods;
using Rex.Service.Core.Events.Payments;
using Rex.Service.Core.Events.Promotions;
using Rex.Service.Core.Helper;
using Rex.Service.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using static Rex.Service.Core.Configurations.GlobalEnums;

namespace Rex.OrderService.Bills
{
    /// <summary>
    /// 售后单服务
    /// </summary>
    [RemoteService]
    [Authorize]
    public class BillAftersalesAppService : ApplicationService, IBillAftersalesAppService
    {
        public IOrdersAppService OrderAppService { get; set; }
        public IBillReshipAppService BillReshipAppService { get; set; }

        private readonly IBillAftersalesRepository _billAftersalesRepository;
        private readonly IBillAftersalesItemRepository _billAftersalesItemRepository;
        private readonly IBillAftersalesImagesRepository _billAftersalesImagesRepository;
        private readonly IOrderItemRepository _orderItemRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly ICapPublisher _capEventBus;

        public BillAftersalesAppService(IBillAftersalesRepository billAftersalesRepository, IBillAftersalesItemRepository billAftersalesItemRepository, IBillAftersalesImagesRepository billAftersalesImagesRepository,
            IOrderItemRepository orderItemRepository, IOrderRepository orderRepository, ICapPublisher capEventBus)
        {
            _billAftersalesRepository = billAftersalesRepository;
            _billAftersalesItemRepository = billAftersalesItemRepository;
            _billAftersalesImagesRepository = billAftersalesImagesRepository;
            _orderItemRepository = orderItemRepository;
            _orderRepository = orderRepository;
            _capEventBus = capEventBus;
        }

        /// <summary>
        /// 查询售后单收货类型
        /// </summary>
        /// <returns></returns>
        public async Task<List<EnumEntity>> GetReceiveTypesAsync()
        {
            return await Task.FromResult(EnumHelper.EnumToList<GlobalEnums.BillAftersalesIsReceive>());
        }

        /// <summary>
        /// 查询售后单状态
        /// </summary>
        /// <returns></returns>
        public async Task<List<EnumEntity>> GetStatusAsync()
        {
            return await Task.FromResult(EnumHelper.EnumToList<GlobalEnums.BillAftersalesStatus>());
        }

        /// <summary>
        /// 获取(分页)售后单列表
        /// </summary>
        /// <param name="input">查询条件</param>
        /// <returns></returns>
        public async Task<PagedResultDto<BillAftersalesDto>> GetListAsync(GetBillAftersalesInput input)
        {
            // 查询数量
            long totalCount = (await _billAftersalesRepository.GetQueryableAsync())
                .WhereIf(!input.No.IsNullOrWhiteSpace(), p => p.No == input.No)
                .WhereIf(!input.OrderNo.IsNullOrWhiteSpace(), p => p.Order.No.Contains(input.OrderNo))
                .WhereIf(input.Type.HasValue, p => p.Type == input.Type)
                .WhereIf(input.Status.HasValue, p => p.Status == input.Status)
                .WhereIf(input.BeginTime.HasValue, p => p.CreationTime >= input.BeginTime)
                .WhereIf(input.EndTime.HasValue, p => p.CreationTime <= input.EndTime)
                .LongCount();
            if (totalCount < 1) return new PagedResultDto<BillAftersalesDto>();

            // 查询数据列表
            List<BillAftersales> billAftersalesList = (await _billAftersalesRepository.GetQueryableAsync())
                .WhereIf(!input.No.IsNullOrWhiteSpace(), p => p.No == input.No)
                .WhereIf(!input.OrderNo.IsNullOrWhiteSpace(), p => p.Order.No.Contains(input.OrderNo))
                .WhereIf(input.Type.HasValue, p => p.Type == input.Type)
                .WhereIf(input.Status.HasValue, p => p.Status == input.Status)
                .WhereIf(input.BeginTime.HasValue, p => p.CreationTime >= input.BeginTime)
                .WhereIf(input.EndTime.HasValue, p => p.CreationTime <= input.EndTime)
                .OrderByIf<BillAftersales, IQueryable<BillAftersales>>(!input.Sorting.IsNullOrWhiteSpace(), input.Sorting)
                .PageBy(input.SkipCount, input.MaxResultCount)
                .ToList();
            return new PagedResultDto<BillAftersalesDto>(
                totalCount,
                ObjectMapper.Map<List<BillAftersales>, List<BillAftersalesDto>>(billAftersalesList)
            );
        }

        /// <summary>
        /// 查询售后单信息
        /// </summary>
        /// <param name="id">售后单ID</param>
        /// <returns></returns>
        public async Task<BillAftersalesDto> GetAsync(Guid id, bool includeDetails = false)
        {
            BillAftersales billAftersales = await _billAftersalesRepository.GetBillAftersalesByIdAsync(id, includeDetails);
            if (billAftersales == null) return null;
            return ObjectMapper.Map<BillAftersales, BillAftersalesDto>(billAftersales);
        }

        /// <summary>
        /// 查询售后单信息
        /// </summary>
        /// <param name="no">售后单号</param>
        /// <returns></returns>
        public async Task<BillAftersalesDto> GetNoAsync(string no, bool includeDetails = false)
        {
            BillAftersales billAftersales = await _billAftersalesRepository.GetBillAftersalesByNoAsync(no, includeDetails);
            if (billAftersales == null) return null;
            return ObjectMapper.Map<BillAftersales, BillAftersalesDto>(billAftersales);
        }

        /// <summary>
        /// 创建售后单
        /// </summary>
        /// <param name="input">售后单参数</param>
        /// <returns></returns>
        public async Task CreateAsync(BillAftersalesCreateDto input)
        {
            #region 校验数据

            if (input.Type != (int)GlobalEnums.BillAftersalesIsReceive.Refund && input.Type != (int)GlobalEnums.BillAftersalesIsReceive.Reship)
                throw new UserFriendlyException("售后类型不存在，请检查！", SystemStatusCode.Fail.ToOrderServicePrefix());
            if (input.OrderItemIds == null || input.OrderItemIds.Length <= 0)
                throw new UserFriendlyException("未选择订单明细，请检查！", SystemStatusCode.Fail.ToOrderServicePrefix());

            UserOrderDto uOrder = await OrderAppService.GetUserOrderAsync(input.OrderId);
            if (uOrder == null)
                throw new UserFriendlyException("该订单不存在或已被删除，请检查！", SystemStatusCode.Fail.ToOrderServicePrefix());
            if (!uOrder.IsAftersalesStatus)
                throw new UserFriendlyException("订单不是可售后状态！", SystemStatusCode.Fail.ToOrderServicePrefix());
            if (uOrder.Status != (int)GlobalEnums.OrderStatus.Normal)
                throw new UserFriendlyException("订单不是可售后状态！", SystemStatusCode.Fail.ToOrderServicePrefix());
            if (uOrder.PayStatus == (int)GlobalEnums.OrderPayStatus.No || uOrder.PayStatus == (int)GlobalEnums.OrderPayStatus.Refunded)
                throw new UserFriendlyException("订单状态不可退款！", SystemStatusCode.Fail.ToOrderServicePrefix());
            if (input.Type == (int)GlobalEnums.BillAftersalesIsReceive.Reship && uOrder.ShipStatus == (int)GlobalEnums.OrderShipStatus.No)
                throw new UserFriendlyException("该订单还没发货呢，怎么能收到货呢？", SystemStatusCode.Fail.ToOrderServicePrefix());

            decimal oRefunded = uOrder.OrderItems.Where(x => input.OrderItemIds.Contains(x.Id)).Sum(x => x.Amount);
            if (input.RefundAmount > oRefunded)
                throw new UserFriendlyException("总退款金额不能超过已支付的金额！", SystemStatusCode.Fail.ToOrderServicePrefix());

            #endregion 校验数据

            // 售后单
            BillAftersales billAftersales = new BillAftersales();
            billAftersales.No = CommonHelper.GetSerialNumberType((int)SerialNumberType.AfterSaleCode);
            billAftersales.OrderId = input.OrderId;
            billAftersales.UserId = input.UserId;
            billAftersales.Type = input.Type;
            billAftersales.RefundAmount = input.RefundAmount;
            billAftersales.Reason = input.Reason;
            billAftersales.Status = (int)BillAftersalesStatus.WaitAudit;
            await _billAftersalesRepository.InsertAsync(billAftersales);

            // 售后单明细
            List<BillAftersalesItem> billAftersalesItems = uOrder.OrderItems.Where(x => input.OrderItemIds.Contains(x.Id)).Select(x => new BillAftersalesItem()
            {
                AftersalesId = billAftersales.Id,
                OrderItemId = x.Id,
                GoodId = x.GoodId,
                ProductId = x.ProductId,
                Sn = x.Sn,
                Bn = x.Bn,
                Name = x.Name,
                ImageUrl = x.ImageUrl,
                Nums = x.Nums,
                Addon = x.Addon,
            }).ToList();
            if (billAftersalesItems != null && billAftersalesItems.Any())
                await _billAftersalesItemRepository.InsertManyAsync(billAftersalesItems);

            // 图片凭证
            List<BillAftersalesImages> billAftersalesImages = new List<BillAftersalesImages>();
            if (input.Images != null)
            {
                for (int i = 0; i < input.Images.Length; i++)
                {
                    billAftersalesImages.Add(new BillAftersalesImages()
                    {
                        AftersalesId = billAftersales.Id,
                        ImageUrl = input.Images[i],
                        Sort = i + 1
                    });
                }
            }
            if (billAftersalesImages.Any())
                await _billAftersalesImagesRepository.InsertManyAsync(billAftersalesImages);

            // TODO: 推送消息给用户
        }

        /// <summary>
        /// 根据订单ID获取售后单状态
        /// </summary>
        /// <param name="orderId">订单ID</param>
        /// <param name="status">售后单状态</param>
        /// <returns></returns>
        public async Task<BillAftersalesDto> GetOrderStatusAsync(Guid orderId, int status)
        {
            BillAftersales billAftersales = await _billAftersalesRepository.FindAsync(x => x.OrderId == orderId && x.Status == status);
            if (billAftersales == null) return null;
            return ObjectMapper.Map<BillAftersales, BillAftersalesDto>(billAftersales);
        }

        /// <summary>
        /// 审核售后单
        /// </summary>
        /// <param name="id">售后单ID</param>
        /// <param name="input">售后单参数</param>
        /// <returns></returns>
        public async Task UpdateAuditAsync(Guid id, BillAftersalesAuditDto input)
        {
            BillAftersales billAftersales = await _billAftersalesRepository.GetBillAftersalesByIdAsync(id, true);

            #region 0.数据校验

            if (billAftersales == null || billAftersales.Order == null)
                throw new UserFriendlyException("售后单/订单异常，请检查！", SystemStatusCode.Fail.ToOrderServicePrefix());
            if (billAftersales.Order.Status != (int)GlobalEnums.OrderStatus.Normal)
                throw new UserFriendlyException("订单不是可售后状态！", SystemStatusCode.Fail.ToOrderServicePrefix());
            if (
                billAftersales.Order.PayStatus == (int)GlobalEnums.OrderPayStatus.No ||
                billAftersales.Order.PayStatus == (int)GlobalEnums.OrderPayStatus.Refunded
            )
                throw new UserFriendlyException("订单状态不可退款，原因：该订单未付款或已退款！", SystemStatusCode.Fail.ToOrderServicePrefix());
            if (input.Type == (int)GlobalEnums.BillAftersalesIsReceive.Reship && billAftersales.Order.ShipStatus == (int)GlobalEnums.OrderShipStatus.No)
                throw new UserFriendlyException("还没发货呢，怎么能收到货呢？", SystemStatusCode.Fail.ToOrderServicePrefix());

            List<OrderItem> orderItems = await _orderItemRepository.GetListAsync(x => input.OrderItemIds.Contains(x.Id));
            decimal oRefunded = orderItems.Where(x => input.OrderItemIds.Contains(x.Id)).Sum(x => x.Amount);
            if (input.RefundAmount > oRefunded)
                throw new UserFriendlyException("总退款金额不能超过已支付的金额！", SystemStatusCode.Fail.ToOrderServicePrefix());

            #endregion 0.数据校验

            // 用于更新订单信息
            Order uOrder = await _orderRepository.GetAsync(billAftersales.OrderId);
            uOrder.Mark = input.Mark;

            #region 1.更新售后单信息

            billAftersales.RefundAmount = input.RefundAmount;
            billAftersales.Type = input.Type;
            billAftersales.Status = input.Status;
            billAftersales.Mark = input.Mark;

            // 移除未选中的售后明细
            List<BillAftersalesItem> removeBillAftersalesItems = billAftersales.BillAftersalesItems.Where(x => !input.OrderItemIds.Contains(x.OrderItemId)).ToList();
            if (removeBillAftersalesItems != null && removeBillAftersalesItems.Any())
                billAftersales.BillAftersalesItems.RemoveAll(removeBillAftersalesItems);

            // 新增选中的售后明细
            List<Guid> bOrderItemIds = billAftersales.BillAftersalesItems.Select(x => x.OrderItemId).ToList();
            List<BillAftersalesItem> billAftersalesItems = orderItems.Where(x => !bOrderItemIds.Contains(x.Id)).Select(x => new BillAftersalesItem()
            {
                AftersalesId = billAftersales.Id,
                OrderItemId = x.Id,
                GoodId = x.GoodId,
                ProductId = x.ProductId,
                Sn = x.Sn,
                Bn = x.Bn,
                Name = x.Name,
                ImageUrl = x.ImageUrl,
                Nums = x.Nums,
                Addon = x.Addon,
            }).ToList();
            if (billAftersalesItems != null && billAftersalesItems.Any())
                billAftersales.BillAftersalesItems.AddRange(billAftersalesItems);
            //await _billAftersalesItemRepository.InsertManyAsync(billAftersalesItems);

            #endregion 1.更新售后单信息

            #region 2.审核通过

            if (input.Status == (int)GlobalEnums.BillAftersalesStatus.Success)
            {
                // 2.1 生成退款单
                await _capEventBus.PublishAsync(BillRefundCreateEto.EventName, new BillRefundCreateEto()
                {
                    BillAftersalesId = billAftersales.Id,
                    Money = billAftersales.RefundAmount,
                    UserId = billAftersales.UserId,
                    SourceId = billAftersales.OrderId.ToString(),
                    Type = (int)GlobalEnums.BillRefundType.Order,
                    Status = (int)GlobalEnums.BillRefundStatus.Norefund
                });

                // 2.2 如果已经发货了，要退货，则需要生成退货单(让用户把商品寄回来)。
                if (
                    billAftersales.Type == (int)GlobalEnums.BillAftersalesIsReceive.Reship &&
                    billAftersales.BillAftersalesItems != null && billAftersales.BillAftersalesItems.Any()
                )
                {
                    BillAftersalesToReshipCreateDto bAftersalesToReship = new BillAftersalesToReshipCreateDto();
                    bAftersalesToReship.UserId = billAftersales.UserId;
                    bAftersalesToReship.OrderId = billAftersales.OrderId;
                    bAftersalesToReship.AftersalesId = billAftersales.Id;
                    bAftersalesToReship.BillAftersalesItems = billAftersales.BillAftersalesItems.Select(x => new BillAftersalesItemDto()
                    {
                        AftersalesId = x.AftersalesId,
                        OrderItemId = x.OrderItemId,
                        GoodId = x.GoodId,
                        ProductId = x.ProductId,
                        Sn = x.Sn,
                        Bn = x.Bn,
                        Name = x.Name,
                        ImageUrl = x.ImageUrl,
                        Nums = x.Nums,
                        Addon = x.Addon
                    }).ToList();
                    await BillReshipAppService.CreateBillAftersalesToReshipAsync(bAftersalesToReship);
                }

                // 2.3 修改订单状态
                uOrder.PayStatus = (int)GlobalEnums.OrderPayStatus.Refunded;
                uOrder.Status = (int)GlobalEnums.OrderStatus.Complete;

                // 积分返还
                if (uOrder.Point > 0)
                {
                    await _capEventBus.PublishAsync(UserChangePointEto.EventName, new UserChangePointEto()
                    {
                        UserId = uOrder.UserId,
                        PointType = (int)UserPointSourceTypes.PointRefundReturn,
                        Point = uOrder.Point,
                        Remark = $"售后退款，积分返还：+{uOrder.Point}；售后单号：{billAftersales.No}"
                    });
                }

                // 优惠券返还
                if (!uOrder.Coupon.IsNullOrWhiteSpace())
                {
                    string[] couponCodes = JsonSerializer.Deserialize<string[]>(uOrder.Coupon);
                    await _capEventBus.PublishAsync(UsedCouponEto.EventName, new UsedCouponEto()
                    {
                        CouponCode = couponCodes,
                        UsedId = null
                    });
                }

                // 2.4 未发货的商品库存调整，如果订单未发货，并且用户未收到商品的情况下，需要解冻库存
                if (
                    (uOrder.ShipStatus == (int)GlobalEnums.OrderShipStatus.No || uOrder.ShipStatus == (int)GlobalEnums.OrderShipStatus.PartialYes) &&
                    billAftersales.Type == (int)GlobalEnums.BillAftersalesIsReceive.Refund &&
                    billAftersales.BillAftersalesItems != null && billAftersales.BillAftersalesItems.Any()
                )
                {
                    foreach (var bAftersalesItem in billAftersales.BillAftersalesItems)
                    {
                        await _capEventBus.PublishAsync(ChangeStockEto.EventName, new ChangeStockEto()
                        {
                            ProductId = bAftersalesItem.ProductId,
                            ChangeStockType = (int)GlobalEnums.OrderChangeStockType.Return,
                            Nums = bAftersalesItem.Nums
                        });
                    }
                }

                // 2.5 处理售后单审核后的事件处理
                // TODO：处理分销...
            }

            #endregion 2.审核通过
        }

        /// <summary>
        /// 获取用户(分页)售后单列表
        /// </summary>
        /// <param name="input">查询条件</param>
        /// <returns></returns>
        public async Task<PagedResultDto<BillAftersalesDto>> GetUserAsync(GetUserBillAftersalesInput input)
        {
            // 查询数量
            long totalCount = await _billAftersalesRepository.GetUserPageCountAsync(input.UserId, input.No, input.OrderNo, input.Type, input.Status);
            if (totalCount < 1) return new PagedResultDto<BillAftersalesDto>();

            // 查询数据列表
            List<BillAftersales> billAftersalesList = await _billAftersalesRepository.GetUserPageListAsync(input.SkipCount, input.MaxResultCount, input.Sorting, input.UserId, input.No, input.OrderNo, input.Type, input.Status, true);
            return new PagedResultDto<BillAftersalesDto>(
                totalCount,
                ObjectMapper.Map<List<BillAftersales>, List<BillAftersalesDto>>(billAftersalesList)
            );
        }

        /// <summary>
        /// 查询批量售后单信息
        /// </summary>
        /// <param name="ids">售后单ID</param>
        /// <returns></returns>
        public async Task<List<BillAftersalesDto>> GetManyAsync(Guid[] ids, bool includeDetails = false)
        {
            List<BillAftersales> billAftersales = await _billAftersalesRepository.GetListAsync(x => ids.Contains(x.Id), includeDetails);
            return ObjectMapper.Map<List<BillAftersales>, List<BillAftersalesDto>>(billAftersales);
        }
    }
}