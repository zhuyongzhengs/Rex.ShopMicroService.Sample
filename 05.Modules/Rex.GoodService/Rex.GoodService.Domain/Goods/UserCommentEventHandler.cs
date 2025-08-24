using DotNetCore.CAP;
using Microsoft.Extensions.DependencyInjection;
using Rex.Service.Core.Events.Goods;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using static Rex.Service.Core.Configurations.GlobalEnums;

namespace Rex.GoodService.Goods
{
    /// <summary>
    /// 用户评价事件处理器
    /// </summary>
    /// <remarks>
    /// 订阅事件
    /// </remarks>
    [Dependency(ServiceLifetime.Scoped)]
    public class UserCommentEventHandler : ICapSubscribe
    {
        private readonly IGoodCommentRepository _goodCommentRepository;

        public UserCommentEventHandler(IGoodCommentRepository goodCommentRepository)
        {
            _goodCommentRepository = goodCommentRepository;
        }

        [CapSubscribe(UserCommentEto.EventName)]
        public async Task UserCommentAsync(UserCommentEto eventData)
        {
            List<GoodComment> gComments = new List<GoodComment>();
            foreach (var uComment in eventData.UserCommentDetails)
            {
                gComments.Add(new GoodComment()
                {
                    Score = uComment.Score,
                    UserId = uComment.UserId,
                    GoodId = uComment.GoodId,
                    OrderId = uComment.OrderId,
                    Images = uComment.Images,
                    ContentBody = uComment.ContentBody,
                    Addon = uComment.Addon
                });
            }
            await _goodCommentRepository.UserCommentAsync(gComments, (int)OrderLogType.LogTypeAutoEvaluation);
        }
    }
}