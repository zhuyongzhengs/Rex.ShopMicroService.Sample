using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using static Rex.Service.Core.Configurations.GlobalEnums;

namespace Rex.GoodService.Goods
{
    /// <summary>
    /// 商品评价仓储接口
    /// </summary>
    public interface IGoodCommentRepository : IRepository<GoodComment, Guid>
    {
        /// <summary>
        /// 用户评价
        /// </summary>
        /// <param name="input">评价信息</param>
        /// <returns></returns>
        Task UserCommentAsync(List<GoodComment> input, int type = (int)OrderLogType.LogTypeEvaluation);
    }
}