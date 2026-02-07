using System;
using System.Collections.Generic;
using Rex.BaseService.Systems.Menus;
using Rex.Service.Core.Models;
using Volo.Abp.Application.Dtos;

namespace Rex.BaseService.Systems.RoleMenus
{
    /// <summary>
    /// 菜单树形结构
    /// </summary>
    public class MenuTreeDto : EntityDto<Guid>
    {
        /// <summary>
        /// 路由名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 组件路径
        /// </summary>
        public string Component { get; set; }

        /// <summary>
        /// 组件路径别名
        /// </summary>
        public string ComponentAlias { get; set; }

        /// <summary>
        /// 是否外链
        /// </summary>
        public bool IsLink { get; set; }

        /// <summary>
        /// 路由路径
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// 路由重定向
        /// </summary>
        public string Redirect { get; set; }

        /// <summary>
        /// 元信息
        /// </summary>
        public MenuMeta Meta { get; set; }

        /// <summary>
        /// 子菜单
        /// </summary>
        public List<MenuTreeDto> Children { get; set; } = new();
    }
}