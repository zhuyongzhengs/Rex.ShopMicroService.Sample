using Rex.BaseService.Menus;
using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.MultiTenancy;

namespace Rex.BaseService.Systems.Menus
{
    /// <summary>
    /// 菜单Dto
    /// </summary>
    public class MenuDto : EntityDto<Guid>, IMultiTenant
    {
        /// <summary>
        /// 租户ID
        /// </summary>
        public Guid? TenantId { get; set; }

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

    /// <summary>
    /// 元信息
    /// </summary>
    public class MenuMeta
    {
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 图标
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        /// 是否隐藏
        /// </summary>
        public bool IsHide { get; set; }

        /// <summary>
        /// 是否缓存
        /// </summary>
        public bool IsKeepAlive { get; set; }

        /// <summary>
        /// 是否固定
        /// </summary>
        public bool IsAffix { get; set; }

        /// <summary>
        /// 外链地址
        /// </summary>
        public string Link { get; set; }

        /// <summary>
        /// 是否内嵌
        /// </summary>
        public bool IsIframe { get; set; }
    }
}