namespace Rex.Service.Permission.GoodServices
{
    /// <summary>
    /// 商品服务权限
    /// </summary>
    public static class GoodServicePermissions
    {
        /// <summary>
        /// 分组(服务)名称
        /// </summary>
        public const string GroupName = "GoodService";

        #region 品牌

        public static class GoodBrands
        {
            /// <summary>
            /// 默认权限
            /// </summary>
            public const string Default = GroupName + ".GoodBrands";

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

        #endregion 品牌

        #region 商品

        public static class Goods
        {
            /// <summary>
            /// 默认权限
            /// </summary>
            public const string Default = GroupName + ".Goods";

            /// <summary>
            /// 创建权限
            /// </summary>
            public const string Create = Default + ".Create";

            /// <summary>
            /// 修改权限
            /// </summary>
            public const string Update = Default + ".Update";

            /// <summary>
            /// 查看权限
            /// </summary>
            public const string Look = Default + ".Look";

            /// <summary>
            /// 删除权限
            /// </summary>
            public const string Delete = Default + ".Delete";
        }

        #endregion 商品

        #region 商品分类

        public static class GoodCategorys
        {
            /// <summary>
            /// 默认权限
            /// </summary>
            public const string Default = GroupName + ".GoodCategorys";

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

        #endregion 商品分类

        #region 商品参数[模型]

        public static class GoodParams
        {
            /// <summary>
            /// 默认权限
            /// </summary>
            public const string Default = GroupName + ".GoodParams";

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

        #endregion 商品参数[模型]

        #region 商品类型规格

        public static class GoodTypeSpecs
        {
            /// <summary>
            /// 默认权限
            /// </summary>
            public const string Default = GroupName + ".GoodTypeSpecs";

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

        #endregion 商品类型规格

        #region 服务商品

        public static class ServiceGoods
        {
            /// <summary>
            /// 默认权限
            /// </summary>
            public const string Default = GroupName + ".ServiceGoods";

            /// <summary>
            /// 创建权限
            /// </summary>
            public const string Create = Default + ".Create";

            /// <summary>
            /// 修改权限
            /// </summary>
            public const string Update = Default + ".Update";

            /// <summary>
            /// 查看权限
            /// </summary>
            public const string Look = Default + ".Look";

            /// <summary>
            /// 删除权限
            /// </summary>
            public const string Delete = Default + ".Delete";
        }

        #endregion 服务商品

        #region 门店

        public static class Stores
        {
            /// <summary>
            /// 默认权限
            /// </summary>
            public const string Default = GroupName + ".Stores";

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

        #endregion 门店

        #region 店铺店员关联

        public static class StoreClerks
        {
            /// <summary>
            /// 默认权限
            /// </summary>
            public const string Default = GroupName + ".StoreClerks";

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

        #endregion 店铺店员关联

        #region 区域

        public static class Areas
        {
            /// <summary>
            /// 默认权限
            /// </summary>
            public const string Default = GroupName + ".Areas";

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

        #endregion 区域

        #region 文章

        public static class Articles
        {
            /// <summary>
            /// 默认权限
            /// </summary>
            public const string Default = GroupName + ".Articles";

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

        #endregion 文章

        #region 文章分类

        public static class ArticleTypes
        {
            /// <summary>
            /// 默认权限
            /// </summary>
            public const string Default = GroupName + ".ArticleTypes";

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

        #endregion 文章分类

        #region 页面设计

        public static class PageDesigns
        {
            /// <summary>
            /// 默认权限
            /// </summary>
            public const string Default = GroupName + ".PageDesigns";

            /// <summary>
            /// 创建权限
            /// </summary>
            public const string Create = Default + ".Create";

            /// <summary>
            /// 复制权限
            /// </summary>
            public const string Copy = Default + ".Copy";

            /// <summary>
            /// 修改权限
            /// </summary>
            public const string Update = Default + ".Update";

            /// <summary>
            /// 删除权限
            /// </summary>
            public const string Delete = Default + ".Delete";

            /// <summary>
            /// 预览权限
            /// </summary>
            public const string Preview = Default + ".Preview";

            /// <summary>
            /// 版面权限
            /// </summary>
            public const string Layout = Default + ".Layout";
        }

        #endregion 页面设计

        #region 公告

        public static class Notices
        {
            /// <summary>
            /// 默认权限
            /// </summary>
            public const string Default = GroupName + ".Notices";

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

        #endregion 公告

        #region 自定义表单

        public static class Forms
        {
            /// <summary>
            /// 默认权限
            /// </summary>
            public const string Default = GroupName + ".Forms";

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

        #endregion 自定义表单

        #region 商城服务描述

        public static class ServiceDescriptions
        {
            /// <summary>
            /// 默认权限
            /// </summary>
            public const string Default = GroupName + ".ServiceDescriptions";

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

        #endregion 商城服务描述

        #region 库存

        public static class Stocks
        {
            /// <summary>
            /// 默认权限
            /// </summary>
            public const string Default = GroupName + ".Stocks";

            /// <summary>
            /// 创建权限
            /// </summary>
            public const string Create = Default + ".Create";
        }

        #endregion 库存

        #region 库存记录

        public static class StockLogs
        {
            /// <summary>
            /// 默认权限
            /// </summary>
            public const string Default = GroupName + ".StockLogs";
        }

        #endregion 库存记录

        #region 商品评价

        public static class GoodComments
        {
            /// <summary>
            /// 默认权限
            /// </summary>
            public const string Default = GroupName + ".GoodComments";

            /// <summary>
            /// 卖家回复权限
            /// </summary>
            public const string SellerReply = Default + ".SellerReply";

            /// <summary>
            /// 删除权限
            /// </summary>
            public const string Delete = Default + ".Delete";
        }

        #endregion 商品评价
    }
}