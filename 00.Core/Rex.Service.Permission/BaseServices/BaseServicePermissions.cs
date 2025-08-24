namespace Rex.Service.Permission.BaseServices
{
    /// <summary>
    /// Base服务权限
    /// </summary>
    public static class BaseServicePermissions
    {
        /// <summary>
        /// 分组(服务)名称
        /// </summary>
        public const string GroupName = "BaseService";

        #region 首页

        public static class Homes
        {
            /// <summary>
            /// 默认权限
            /// </summary>
            public const string Default = GroupName + ".Homes";
        }

        #endregion 首页

        #region 菜单

        /// <summary>
        /// 菜单
        /// </summary>
        public static class Menus
        {
            /// <summary>
            /// 默认权限
            /// </summary>
            public const string Default = GroupName + ".Menus";

            /// <summary>
            /// 创建权限
            /// </summary>
            public const string Create = Default + ".Create";

            /// <summary>
            /// 修改权限
            /// </summary>
            public const string Update = Default + ".Update";

            /// <summary>
            /// 删除权限
            /// </summary>
            public const string Delete = Default + ".Delete";
        }

        #endregion 菜单

        #region 组织单元

        public static class OrganizationUnits
        {
            /// <summary>
            /// 默认权限
            /// </summary>
            public const string Default = GroupName + ".OrganizationUnits";

            /// <summary>
            /// 创建权限
            /// </summary>
            public const string Create = Default + ".Create";

            /// <summary>
            /// 修改权限
            /// </summary>
            public const string Update = Default + ".Update";

            /// <summary>
            /// 删除权限
            /// </summary>
            public const string Delete = Default + ".Delete";

            /// <summary>
            /// 管理组织结构用户
            /// </summary>
            public const string ManagingUser = Default + ".ManagingUser";

            /// <summary>
            /// 管理组织结构角色
            /// </summary>
            public const string ManagingRole = Default + ".ManagingRole";
        }

        #endregion 组织单元

        #region 审计日志

        public static class AuditLoggings
        {
            /// <summary>
            /// 默认权限
            /// </summary>
            public const string Default = GroupName + ".AuditLogging";
        }

        #endregion 审计日志

        #region 安全日志

        public static class SecurityLogs
        {
            /// <summary>
            /// 默认权限
            /// </summary>
            public const string Default = GroupName + ".SecurityLog";
        }

        #endregion 安全日志

        #region 注册用户

        public static class SysUsers
        {
            /// <summary>
            /// 默认权限
            /// </summary>
            public const string Default = GroupName + ".SysUsers";

            /// <summary>
            /// 创建权限
            /// </summary>
            public const string Create = Default + ".Create";

            /// <summary>
            /// 修改权限
            /// </summary>
            public const string Update = Default + ".Update";

            /// <summary>
            /// 删除权限
            /// </summary>
            public const string Delete = Default + ".Delete";
        }

        #endregion 注册用户

        #region 微信用户

        public static class UserWeChats
        {
            /// <summary>
            /// 默认权限
            /// </summary>
            public const string Default = GroupName + ".UserWeChats";

            /// <summary>
            /// 创建权限
            /// </summary>
            public const string Create = Default + ".Create";

            /// <summary>
            /// 修改权限
            /// </summary>
            public const string Update = Default + ".Update";

            /// <summary>
            /// 删除权限
            /// </summary>
            public const string Delete = Default + ".Delete";
        }

        #endregion 微信用户

        #region 用户等级

        public static class UserGrades
        {
            /// <summary>
            /// 默认权限
            /// </summary>
            public const string Default = GroupName + ".UserGrades";

            /// <summary>
            /// 创建权限
            /// </summary>
            public const string Create = Default + ".Create";

            /// <summary>
            /// 修改权限
            /// </summary>
            public const string Update = Default + ".Update";

            /// <summary>
            /// 删除权限
            /// </summary>
            public const string Delete = Default + ".Delete";
        }

        #endregion 用户等级

        #region 平台设置

        public static class PlatformSettings
        {
            /// <summary>
            /// 默认权限
            /// </summary>
            public const string Default = GroupName + ".PlatformSettings";
        }

        #endregion 平台设置
    }
}