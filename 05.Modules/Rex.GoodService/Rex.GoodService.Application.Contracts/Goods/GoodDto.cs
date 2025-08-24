using Rex.GoodService.Brands;
using Rex.GoodService.Products;
using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;
using Volo.Abp.MultiTenancy;

namespace Rex.GoodService.Goods
{
    /// <summary>
    /// 商品Dto
    /// </summary>
    public partial class GoodDto : EntityDto<Guid>, IMultiTenant
    {
        /// <summary>
        /// 租户ID
        /// </summary>
        public Guid? TenantId { get; set; }

        /// <summary>
        /// 商品条码
        /// </summary>
        public string BarCode { get; set; }

        /// <summary>
        /// 商品名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 商品简介
        /// </summary>
        public string? Brief { get; set; }

        /// <summary>
        /// 缩略图
        /// </summary>
        public string? Image { get; set; }

        /// <summary>
        /// 图集
        /// </summary>
        public string? Images { get; set; }

        /// <summary>
        /// 视频
        /// </summary>
        public string? Video { get; set; }

        /// <summary>
        /// 佣金分配方式
        /// </summary>
        public int ProductsDistributionType { get; set; }

        /// <summary>
        /// 商品分类
        /// </summary>
        public Guid GoodCategoryId { get; set; }

        /// <summary>
        /// 商品分类数据
        /// </summary>
        public GoodCategoryDto GoodCategory { get; set; }

        /// <summary>
        /// 商品类别
        /// </summary>
        public Guid GoodTypeId { get; set; }

        /// <summary>
        /// sku序列
        /// </summary>
        public string? GoodSkuIds { get; set; }

        /// <summary>
        /// 参数序列
        /// </summary>
        public string? GoodParamsIds { get; set; }

        /// <summary>
        /// 品牌
        /// </summary>
        public Guid BrandId { get; set; }

        /// <summary>
        /// 品牌数据
        /// </summary>
        public BrandDto Brand { get; set; }

        /// <summary>
        /// 是否虚拟商品
        /// </summary>
        public bool IsNomalVirtual { get; set; }

        /// <summary>
        /// 是否上架
        /// </summary>
        public bool IsMarketable { get; set; }

        /// <summary>
        /// 商品单位
        /// </summary>
        public string? Unit { get; set; }

        /// <summary>
        /// 商品详情
        /// </summary>
        public string? Intro { get; set; }

        /// <summary>
        /// 商品规格序列号存储
        /// </summary>
        public string? SpesDesc { get; set; }

        /// <summary>
        /// 参数序列化
        /// </summary>
        public string? Parameters { get; set; }

        /// <summary>
        /// 评论次数
        /// </summary>
        public int CommentsCount { get; set; }

        /// <summary>
        /// 浏览次数
        /// </summary>
        public int ViewCount { get; set; }

        /// <summary>
        /// 购买次数
        /// </summary>
        public int BuyCount { get; set; }

        /// <summary>
        /// 上架时间
        /// </summary>
        public DateTime? Uptime { get; set; }

        /// <summary>
        /// 下架时间
        /// </summary>
        public DateTime? Downtime { get; set; }

        /// <summary>
        /// 商品排序
        /// </summary>
        public int Sort { get; set; }

        /// <summary>
        /// 标签id逗号分隔
        /// </summary>
        public string? LabelIds { get; set; }

        /// <summary>
        /// 自定义规格名称
        /// </summary>
        public string? NewSpec { get; set; }

        /// <summary>
        /// 开启规则
        /// </summary>
        public int OpenSpec { get; set; }

        /// <summary>
        /// 是否推荐
        /// </summary>
        public bool IsRecommend { get; set; }

        /// <summary>
        /// 是否热门
        /// </summary>
        public bool IsHot { get; set; }

        /// <summary>
        /// 货品信息
        /// </summary>
        public List<ProductDto> Products { get; set; }

        /// <summary>
        /// 并发(控制)戳
        /// </summary>
        public string ConcurrencyStamp { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime? LastModificationTime { get; set; }

        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime? CreationTime { get; set; }
    }

    /// <summary>
    /// 商品Dto
    /// </summary>
    public partial class GoodDto
    {
        /// <summary>
        /// 销售价
        /// </summary>
        public decimal Price { get; set; } = 0;

        /// <summary>
        /// 成本价
        /// </summary>
        public decimal CostPrice { get; set; } = 0;

        /// <summary>
        /// 市场价
        /// </summary>
        public decimal MktPrice { get; set; } = 0;

        /// <summary>
        /// 是否收藏
        /// </summary>
        public bool IsFav { get; set; }
    }
}