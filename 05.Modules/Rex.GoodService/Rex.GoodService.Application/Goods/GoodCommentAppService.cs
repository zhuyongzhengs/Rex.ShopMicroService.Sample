using Rex.Service.Core.Configurations;
using Rex.Service.Core.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.EventBus.Distributed;

namespace Rex.GoodService.Goods
{
    /// <summary>
    /// 商品评价服务
    /// </summary>
    [RemoteService]
    public class GoodCommentAppService : ApplicationService, IGoodCommentAppService
    {
        private readonly IGoodCommentRepository _goodCommentRepository;
        private readonly IGoodRepository _goodRepository;
        private readonly IDistributedEventBus _distributedEventBus;

        public GoodCommentAppService(IGoodCommentRepository goodCommentRepository, IGoodRepository goodRepository, IDistributedEventBus distributedEventBus)
        {
            _goodCommentRepository = goodCommentRepository;
            _goodRepository = goodRepository;
            _distributedEventBus = distributedEventBus;
        }

        /// <summary>
        /// 获取(分页)商品评价列表
        /// </summary>
        /// <param name="input">查询条件</param>
        /// <returns></returns>
        public async Task<PagedResultDto<GoodCommentDto>> GetListAsync(GetGoodCommentInput input)
        {
            // 查询数量
            long totalCount = (await _goodCommentRepository.GetQueryableAsync())
                .WhereIf(input.Score.HasValue && input.Score > 0, p => p.Score == input.Score)
                .WhereIf(input.UserId.HasValue, p => p.UserId == input.UserId)
                .WhereIf(input.GoodId.HasValue, p => p.GoodId == input.GoodId)
                .WhereIf(input.OrderId.HasValue, p => p.OrderId == input.OrderId)
                .WhereIf(input.IsDisplay.HasValue, p => p.IsDisplay == input.IsDisplay)
                .LongCount();

            // 查询数据列表
            if (totalCount < 1)
            {
                return new PagedResultDto<GoodCommentDto>();
            }
            List<GoodComment> goodCommentList = (await _goodCommentRepository.GetQueryableAsync())
                .WhereIf(input.Score.HasValue && input.Score > 0, p => p.Score == input.Score)
                .WhereIf(input.UserId.HasValue, p => p.UserId == input.UserId)
                .WhereIf(input.GoodId.HasValue, p => p.GoodId == input.GoodId)
                .WhereIf(input.OrderId.HasValue, p => p.OrderId == input.OrderId)
                .WhereIf(input.IsDisplay.HasValue, p => p.IsDisplay == input.IsDisplay)
                .OrderByIf<GoodComment, IQueryable<GoodComment>>(!input.Sorting.IsNullOrWhiteSpace(), input.Sorting)
                .PageBy(input.SkipCount, input.MaxResultCount)
                .ToList();

            return new PagedResultDto<GoodCommentDto>(
                totalCount,
                ObjectMapper.Map<List<GoodComment>, List<GoodCommentDto>>(goodCommentList)
            );
        }

        /// <summary>
        /// 更新卖家回复
        /// </summary>
        /// <param name="id">评价ID</param>
        /// <param name="content">回复内容</param>
        /// <returns></returns>
        public async Task UpdateSellerReplyAsync(Guid id, string content)
        {
            GoodComment goodComment = await _goodCommentRepository.GetAsync(id);
            if (goodComment == null)
                throw new UserFriendlyException("该评价信息不存在(或已被删除)，请检查！", SystemStatusCode.Fail.ToGoodServicePrefix());
            goodComment.SellerContent = content;
        }

        /// <summary>
        /// 删除商品评价
        /// </summary>
        /// <param name="ids">商品评价ID</param>
        /// <returns></returns>
        public async Task DeleteGoodCommentByIdsAsync(Guid[] ids)
        {
            if (ids == null || ids.Length == 0)
                throw new UserFriendlyException("未检测到需要删除的评价信息！", SystemStatusCode.Fail.ToGoodServicePrefix());
            await _goodCommentRepository.DeleteManyAsync(ids);
        }

        /// <summary>
        /// 用户评价
        /// </summary>
        /// <param name="orderId">订单ID</param>
        /// <param name="input">评价信息</param>
        /// <returns></returns>
        public async Task UserCommentAsync(Guid orderId, List<UserCommentDto> input)
        {
            List<GoodComment> gComments = new List<GoodComment>();
            foreach (var uComment in input)
            {
                gComments.Add(new GoodComment()
                {
                    Score = uComment.Score,
                    UserId = uComment.UserId,
                    UserName = uComment.UserName,
                    Avatar = uComment.Avatar,
                    GoodId = uComment.GoodId,
                    OrderId = orderId,
                    Images = uComment.Images,
                    ContentBody = uComment.ContentBody,
                    Addon = uComment.Addon
                });
            }
            await _goodCommentRepository.UserCommentAsync(gComments);
        }

        /// <summary>
        /// 更新是否前台显示
        /// </summary>
        /// <param name="id">评价ID</param>
        /// <param name="isDisplay">是否显示</param>
        /// <returns></returns>
        public async Task UpdateIsDisplayAsync(Guid id, bool isDisplay)
        {
            GoodComment goodComment = await _goodCommentRepository.GetAsync(id);
            if (goodComment == null)
                throw new UserFriendlyException("该评价信息不存在(或已被删除)，请检查！", SystemStatusCode.Fail.ToGoodServicePrefix());
            goodComment.IsDisplay = isDisplay;
        }

        /// <summary>
        /// 测试消息
        /// </summary>
        /// <returns></returns>
        public async Task<string> TestOrderToGoodMsgAsync(OrderToGoodEto eventData)
        {
            Thread.Sleep(3 * 1000);
            if (eventData.IsException)
                throw new UserFriendlyException("模拟异常！");
            await Task.FromResult(eventData);
            Console.WriteLine($"订单服务已收到：{eventData.OrderMsg}");
            return JsonSerializer.Serialize(eventData);
        }
    }
}