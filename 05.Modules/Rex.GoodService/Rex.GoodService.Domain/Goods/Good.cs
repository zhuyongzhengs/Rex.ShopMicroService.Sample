using Rex.GoodService.Brands;
using Rex.GoodService.Products;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace Rex.GoodService.Goods
{
    /// <summary>
    /// 商品表
    /// </summary>
    public partial class Good : FullAuditedAggregateRoot<Guid>, IMultiTenant
    {
        public Good()
        {
        }

        public Good(Guid id)
        {
            Id = id;
        }

        /// <summary>
        /// 租户ID
        /// </summary>
        public Guid? TenantId { get; set; }

        /// <summary>
        /// 商品条码
        /// </summary>
        [Required]
        [StringLength(30)]
        public string BarCode { get; set; }

        /// <summary>
        /// 商品名称
        /// </summary>
        [Required]
        [StringLength(200)]
        public string Name { get; set; }

        /// <summary>
        /// 商品简介
        /// </summary>
        [StringLength(1000)]
        public string? Brief { get; set; }

        /// <summary>
        /// 缩略图
        /// </summary>
        [StringLength(1000)]
        public string? Image { get; set; }

        /// <summary>
        /// 图集
        /// </summary>
        public string? Images { get; set; }

        /// <summary>
        /// 视频
        /// </summary>
        [StringLength(255)]
        public string? Video { get; set; }

        /// <summary>
        /// 佣金分配方式
        /// </summary>
        [Required]
        public int ProductsDistributionType { get; set; }

        /// <summary>
        /// 商品分类
        /// </summary>
        [Required]
        public Guid GoodCategoryId { get; set; }

        /// <summary>
        /// 商品分类数据
        /// </summary>
        [ForeignKey(nameof(GoodCategoryId))]
        public GoodCategory? GoodCategory { get; set; }

        /// <summary>
        /// 商品类别
        /// </summary>
        [Required]
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
        [Required]
        public Guid BrandId { get; set; }

        /// <summary>
        /// 品牌数据
        /// </summary>
        [ForeignKey(nameof(BrandId))]
        public Brand? Brand { get; set; }

        /// <summary>
        /// 是否虚拟商品
        /// </summary>
        [Required]
        public bool IsNomalVirtual { get; set; }

        /// <summary>
        /// 是否上架
        /// </summary>
        [Required]
        public bool IsMarketable { get; set; }

        /// <summary>
        /// 商品单位
        /// </summary>
        [StringLength(20)]
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
        [Required]
        public int CommentsCount { get; set; }

        /// <summary>
        /// 浏览次数
        /// </summary>
        [Required]
        public int ViewCount { get; set; }

        /// <summary>
        /// 购买次数
        /// </summary>
        [Required]
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
        [Required]
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
        [Required]
        public int OpenSpec { get; set; }

        /// <summary>
        /// 是否推荐
        /// </summary>
        [Required]
        public bool IsRecommend { get; set; }

        /// <summary>
        /// 是否热门
        /// </summary>
        [Required]
        public bool IsHot { get; set; }

        /// <summary>
        /// 货品信息
        /// </summary>
        public List<Product> Products { get; set; } = new();
    }
}