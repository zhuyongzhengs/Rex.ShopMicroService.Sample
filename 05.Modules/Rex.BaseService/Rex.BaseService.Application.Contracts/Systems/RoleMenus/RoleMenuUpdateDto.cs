using System;
using Volo.Abp.Application.Dtos;

namespace Rex.BaseService.Systems.RoleMenus
{
    /// <summary>
    /// 修改角色菜单
    /// </summary>
    public class RoleMenuUpdateDto : EntityDto
    {
        /// <summary>
        /// 角色ID
        /// </summary>
        public Guid RoleId { get; set; }

        /// <summary>
        /// 菜单ID
        /// </summary>
        public Guid MenuId { get; set; }

        /// <summary>
        /// 并发(控制)戳
        /// </summary>
        public string ConcurrencyStamp { get; set; }
    }
}