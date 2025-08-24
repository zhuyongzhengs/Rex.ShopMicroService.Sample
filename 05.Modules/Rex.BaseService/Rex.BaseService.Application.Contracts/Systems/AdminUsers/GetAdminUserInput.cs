using System;
using Volo.Abp.Application.Dtos;

namespace Rex.BaseService.Systems.AdminUsers
{
    /// <summary>
    /// 查询管理员用户
    /// </summary>
    public class GetAdminUserInput : PagedAndSortedResultRequestDto
    {
        /// <summary>
        /// 用户名称
        /// </summary>
        public string? UserName { get; set; }
    }
}