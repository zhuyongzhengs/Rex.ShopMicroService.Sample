using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;

namespace Rex.BaseService.Systems.RoleMenus
{
    /// <summary>
    /// 批量修改角色菜单
    /// </summary>
    public class ManyRoleMenuUpdateDto : EntityDto
    {
        /// <summary>
        /// 角色ID
        /// </summary>
        public Guid RoleId { get; set; }

        /// <summary>
        /// 菜单ID
        /// </summary>
        public List<Guid> MenuIds { get; set; } = new List<Guid>();
    }
}