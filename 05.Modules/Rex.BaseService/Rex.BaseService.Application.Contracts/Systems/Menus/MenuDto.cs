using Rex.BaseService.Menus;
using Rex.Service.Core.Models;
using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.MultiTenancy;

namespace Rex.BaseService.Systems.Menus
{
    /// <summary>
    /// 菜单Dto
    /// </summary>
    public class MenuDto : EntityDto<Guid>
    {
        /// <summary>
        /// 上级菜单
        /// </summary>
        public Guid? PId { get; set; }

        /// <summary>
        /// 菜单类型
        /// </summary>
        public MenuType? MenuType { get; set; }

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
        /// 菜单排序
        /// </summary>
        public int MenuSort { get; set; }

        /// <summary>
        /// 路由路径
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// 路由重定向
        /// </summary>
        public string Redirect { get; set; }

        /// <summary>
        /// 权限标识
        /// </summary>
        public string PermissionIdentifying { get; set; }

        /// <summary>
        /// 并发(控制)戳
        /// </summary>
        public string ConcurrencyStamp { get; set; }

        /// <summary>
        /// 元信息
        /// </summary>
        public MenuMeta Meta { get; set; }
    }
}