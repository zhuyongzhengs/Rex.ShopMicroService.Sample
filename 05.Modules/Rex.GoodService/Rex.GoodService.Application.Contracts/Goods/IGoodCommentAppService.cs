using Rex.Service.Core.Events;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Rex.GoodService.Goods
{
    /// <summary>
    /// 商品评价服务接口
    /// </summary>
    public interface IGoodCommentAppService : IApplicationService
    {
        /// <summary>
        /// 获取(分页)商品评价列表
        /// </summary>
        /// <param name="input">查询条件</param>
        /// <returns></returns>
        Task<PagedResultDto<GoodCommentDto>> GetListAsync(GetGoodCommentInput input);

        /// <summary>
        /// 更新卖家回复
        /// </summary>
        /// <param name="id">评价ID</param>
        /// <param name="content">回复内容</param>
        /// <returns></returns>
        Task UpdateSellerReplyAsync(Guid id, string content);

        /// <summary>
        /// 更新是否前台显示
        /// </summary>
        /// <param name="id">评价ID</param>
        /// <param name="isDisplay">是否显示</param>
        /// <returns></returns>
        Task UpdateIsDisplayAsync(Guid id, bool isDisplay);

        /// <summary>
        /// 删除商品评价
        /// </summary>
        /// <param name="ids">评价ID</param>
        /// <returns></returns>
        Task DeleteGoodCommentByIdsAsync(Guid[] ids);

        /// <summary>
        /// 用户评价
        /// </summary>
        /// <param name="orderId">订单ID</param>
        /// <param name="input">评价信息</param>
        /// <returns></returns>
        Task UserCommentAsync(Guid orderId, List<UserCommentDto> input);

        /// <summary>
        /// 测试消息
        /// </summary>
        /// <returns></returns>
        Task<string> TestOrderToGoodMsgAsync(OrderToGoodEto eventData);
    }
}