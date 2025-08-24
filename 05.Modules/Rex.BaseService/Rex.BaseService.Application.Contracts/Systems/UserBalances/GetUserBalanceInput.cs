using System;
using Volo.Abp.Application.Dtos;

namespace Rex.BaseService.Systems.UserBalances
{
    /// <summary>
    /// 查询用户余额
    /// </summary>
    public class GetUserBalanceInput : PagedAndSortedResultRequestDto
    {
        /// <summary>
        /// 类型
        /// </summary>
        public int? Type { get; set; }
    }
}