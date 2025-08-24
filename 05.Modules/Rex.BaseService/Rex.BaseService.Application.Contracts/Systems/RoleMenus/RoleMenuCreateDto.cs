using System;
using Volo.Abp.Application.Dtos;

namespace Rex.BaseService.Systems.RoleMenus
{
    /// <summary>
    /// 创建角色菜单
    /// </summary>
    public class RoleMenuCreateDto : EntityDto
    {
        /// <summary>
        /// 角色ID
        /// </summary>
        public Guid RoleId { get; set; }

        /// <summary>
        /// 菜单ID
        /// </summary>
        public Guid MenuId { get; set; }
    }
}